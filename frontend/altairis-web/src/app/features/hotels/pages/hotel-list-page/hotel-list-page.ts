import { Component, OnInit, inject, input, signal, viewChild } from '@angular/core';
import { HotelFiltersComponent } from '../../components/hotel-filters-component/hotel-filters-component';
import { HotelApiService } from '../../services/hotel-service';
import { PaginationParams } from '../../../../shared/models/pagination-params-model';
import { Hotel, HotelFilters } from '../../models/hotel.model';
import { PagedListApiResponse } from '../../../../shared/models/api-response.model';
import { HttpStatusCode } from '@angular/common/http';
import { DatePipe } from '@angular/common';
import { rxResource } from '@angular/core/rxjs-interop';
import { PaginationComponent } from '../../../../shared/components/pagination-component/pagination-component';
import { ModalComponent } from '../../../../shared/components/modal-component/modal-component';
import { HotelFormPageComponent } from '../hotel-form-page/hotel-form-page';
import { Dropdown } from 'bootstrap';
import { ToastService } from '../../../../shared/components/toast-component/service/toast-service';

@Component({
  imports: [
    HotelFiltersComponent,
    PaginationComponent,
    ModalComponent,
    HotelFormPageComponent
],
  selector: 'app-hotel-list-page',
  styleUrl: './hotel-list-page.scss',
  templateUrl: './hotel-list-page.html',
})

export class HotelListPageComponent {
  private apiService = inject(HotelApiService);
  private toastService = inject(ToastService);
  
  pagination = signal<PaginationParams>({pageNumber: 1, pageSize: 10});
  filters = signal<HotelFilters | undefined>(undefined);
  isEdition = signal<boolean>(false);
  isView = signal<boolean>(false);
  selectedHotel = signal<Hotel | undefined | null>(undefined);

  isFormLoading = signal<boolean>(false)
  
  creationModal = viewChild.required(ModalComponent);
  isFormValid: boolean = false;
  

  edit(hotel: Hotel) {
    this.isEdition.set(true);
    this.isView.set(false);
    
    this.openModal(hotel);
  }
  
  view(hotel: Hotel) {
    this.isView.set(true);
    this.isEdition.set(false);

    this.openModal(hotel);
  }

  activate = (id: number) => this.processStatusChange(id, true);
  deactivate = (id: number) => this.processStatusChange(id, false);

  private processStatusChange(id: number, activate: boolean) {
      this.apiService.changeStatus(id, activate).subscribe({
        next: (response) => {
          console.log(response.statusCode === HttpStatusCode.Ok)

          this.toastService.show(
            response.message,

            (response.statusCode === HttpStatusCode.Ok) ? 'success' :
            (response.statusCode === HttpStatusCode.NotFound) ? 'info' :
            (response.statusCode === HttpStatusCode.BadRequest) ? 'danger' :
            'success'
          )
        },
        error: (err) => {
          this.toastService.show(
            'Error en el servicio: ' + err?.error?.message,
            'danger',
          )
        },
        complete: () => {
          this.hotelResource.reload();
        }
      });
  }
  
  checkFormValid = (isValid: boolean = false) => this.isFormValid = isValid;
  
  openModal(hotel: Hotel | undefined | null = undefined): void {
    if (this.isFormLoading()) return;

    if (hotel != undefined && hotel != null) this.selectedHotel.set(hotel);

    this.creationModal().open();
  } 

  onHotelSaved(isSaved: boolean): void {
    if (isSaved) {
      this.creationModal().close();
    }
  }
  
  onModalClosed(): void {
    this.hotelResource.reload();
    this.isEdition.set(false);
    this.isView.set(false);
    this.selectedHotel.set(null);
  }

  onPageChange(page: number): void {
    this.pagination.update(current => ({
      ...current,
      pageNumber: page
    }));
  }

  filterHotels(filters: HotelFilters) {
    this.filters.set(filters); 
  }

  removeFilters() {
    this.filters.set(undefined); 
  }

  hotelResource = rxResource({
    params: () =>({
      pagination: this.pagination(),
      filters: this.filters()
    }),
    stream: (ctx) => this.apiService.getAllPaged(
      ctx.params.pagination, 
      ctx.params.filters
    )
  });

  data = this.hotelResource.value;        
  isLoading = this.hotelResource.isLoading;
  error = this.hotelResource.error;
}
