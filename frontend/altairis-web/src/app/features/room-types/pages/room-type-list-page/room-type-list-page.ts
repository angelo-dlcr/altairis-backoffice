import { Component, inject, signal, viewChild } from '@angular/core';
import { PaginationComponent } from '../../../../shared/components/pagination-component/pagination-component';
import { rxResource } from '@angular/core/rxjs-interop';
import { PaginationParams } from '../../../../shared/models/pagination-params-model';
import { RoomType, RoomTypeFilters } from '../../models/room-type.model';
import { RoomTypeApiService } from '../../services/room-type-service';
import { RoomTypesFiltersComponent } from '../../components/room-types-filters-component/room-types-filters-component';
import { ModalComponent } from '../../../../shared/components/modal-component/modal-component';
import { HttpStatusCode } from '@angular/common/http';
import { ToastService } from '../../../../shared/components/toast-component/service/toast-service';
import { RoomTypeFormPageComponent } from '../room-type-form-page/room-type-form-page';

@Component({
  imports: [PaginationComponent, RoomTypesFiltersComponent, ModalComponent, RoomTypeFormPageComponent],
  selector: 'app-room-type-list-page',
  styleUrl: './room-type-list-page.scss',
  templateUrl: './room-type-list-page.html',
})
export class RoomTypeListPageComponent {
  private apiService = inject(RoomTypeApiService);
  private toastService = inject(ToastService);

  pagination = signal<PaginationParams>({pageNumber: 1, pageSize: 10});
  filters = signal<RoomTypeFilters | undefined>(undefined);
  isEdition = signal<boolean>(false);
  isView = signal<boolean>(false);
  selectedRoomType = signal<RoomType | undefined | null>(undefined);

  isModalOpen = signal<boolean>(false);

  isFormLoading = signal<boolean>(false)
  
  creationModal = viewChild.required(ModalComponent);
  isFormValid: boolean = false;  
  
  edit(roomType: RoomType) {
    this.isEdition.set(true);
    this.isView.set(false);
    
    this.openModal(roomType);
  }
  
  view(roomType: RoomType) {
    this.isView.set(true);
    this.isEdition.set(false);

    this.openModal(roomType);
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
          this.roomTypeResource.reload();
        }
      });
  }

  checkFormValid = (isValid: boolean = false) => this.isFormValid = isValid;
  
  openModal(roomType: RoomType | undefined | null = undefined): void {
    if (this.isFormLoading()) return;
    if (roomType != undefined && roomType != null) this.selectedRoomType.set(roomType);
    
    this.isModalOpen.set(true);
    this.creationModal().open();
  } 

  onSaved(isSaved: boolean): void {
    if (isSaved) {
      this.creationModal().close();
    }
  }
  
  onModalClosed(): void {
    this.isModalOpen.set(false);
    this.roomTypeResource.reload();
    this.isEdition.set(false);
    this.isView.set(false);
    this.selectedRoomType.set(null);
  }

  filterHotels(filters: RoomTypeFilters) {
    this.filters.set(filters); 
  }

  roomTypeResource = rxResource({
    params: () =>({
      pagination: this.pagination(),
      filters: this.filters()
    }),
    stream: (ctx) => this.apiService.getAllPaged(
      ctx.params.pagination, 
      ctx.params.filters
    )
  });

  data = this.roomTypeResource.value;        
  isLoading = this.roomTypeResource.isLoading;
  error = this.roomTypeResource.error;
  
  onPageChange(page: number): void {
    this.pagination.update(current => ({
      ...current,
      pageNumber: page
    }));
  }

  filterRoomTypes(filters: RoomTypeFilters) {
    this.filters.set(filters); 
  }

  removeFilters() {
    this.filters.set(undefined); 
  }
}
