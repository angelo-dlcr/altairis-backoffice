import { Component, inject, signal } from '@angular/core';
import { PaginationComponent } from '../../../../shared/components/pagination-component/pagination-component';
import { rxResource } from '@angular/core/rxjs-interop';
import { PaginationParams } from '../../../../shared/models/pagination-params-model';
import { RoomTypeFilters } from '../../models/room-type.model';
import { RoomTypeApiService } from '../../services/room-type-service';
import { RoomTypesFiltersComponent } from '../../components/room-types-filters-component/room-types-filters-component';

@Component({
  imports: [PaginationComponent, RoomTypesFiltersComponent],
  selector: 'app-room-type-list-page',
  styleUrl: './room-type-list-page.scss',
  templateUrl: './room-type-list-page.html',
})
export class RoomTypeListPageComponent {
  private apiService = inject(RoomTypeApiService);

  pagination = signal<PaginationParams>({pageNumber: 1, pageSize: 10});
  filters = signal<RoomTypeFilters | undefined>(undefined);

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

  filterRoomTypes(filters: RoomTypeFilters) {
    this.filters.set(filters); 
  }

  removeFilters() {
    this.filters.set(undefined); 
  }
}
