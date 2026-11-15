import { Component, OnInit, inject, signal } from '@angular/core';
import { HotelFiltersComponent } from '../../components/hotel-filters-component/hotel-filters-component';
import { HotelApiService } from '../../services/hotel-service';
import { PaginationParams } from '../../../../shared/models/pagination-params-model';
import { Hotel, HotelFilters } from '../../models/hotel.model';
import { PagedListApiResponse } from '../../../../shared/models/api-response.model';
import { HttpStatusCode } from '@angular/common/http';
import { DatePipe } from '@angular/common';
import { rxResource } from '@angular/core/rxjs-interop';

@Component({
  imports: [
    HotelFiltersComponent,
    DatePipe
  ],
  selector: 'app-hotel-list-page',
  styleUrl: './hotel-list-page.scss',
  templateUrl: './hotel-list-page.html',
})

export class HotelListPageComponent {
  private apiService = inject(HotelApiService);

  pagination = signal<PaginationParams>({pageNumber: 1, pageSize: 10});
  filters = signal< HotelFilters | undefined>(undefined);

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

  filterHotels(filters: HotelFilters) {
    this.filters.set(filters); 
  }

  removeFilters() {
    this.filters.set(undefined); 
  }
}
