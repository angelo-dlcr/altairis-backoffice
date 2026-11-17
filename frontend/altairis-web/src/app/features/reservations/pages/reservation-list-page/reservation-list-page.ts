import { Component, inject, signal } from '@angular/core';
import { rxResource } from '@angular/core/rxjs-interop';
import { ReservationApiService } from '../../services/reservation-service';
import { PaginationParams } from '../../../../shared/models/pagination-params-model';
import { ReservationsFiltersComponent } from '../../components/reservations-filters-component/reservations-filters-component';
import { ReservationFilters, ReservationStatusLabel } from '../../models/reservation.model';
import { PaginationComponent } from '../../../../shared/components/pagination-component/pagination-component';
import { DatePipe } from '@angular/common';

@Component({
  imports: [
    ReservationsFiltersComponent, 
    PaginationComponent,
    DatePipe
  ],
  selector: 'app-reservation-list-page',
  styleUrl: './reservation-list-page.scss',
  templateUrl: './reservation-list-page.html',
})
export class ReservationListPageComponent {
  private apiService = inject(ReservationApiService);

  pagination = signal<PaginationParams>({pageNumber: 1, pageSize: 10});
  filters = signal<ReservationFilters | undefined>(undefined);

  readonly statusLabels = ReservationStatusLabel;

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
  
  onPageChange(page: number): void {
    this.pagination.update(current => ({
      ...current,
      pageNumber: page
    }));
  }

  filterReservations(filters: ReservationFilters) {
    this.filters.set(filters); 
  }

  removeFilters() {
    this.filters.set(undefined); 
  }
}
