import { Component, inject, signal } from '@angular/core';
import { InventoryApiService } from '../../services/inventory-service';
import { rxResource } from '@angular/core/rxjs-interop';
import { InventoryFilters } from '../../models/inventory.model';
import { PaginationParams } from '../../../../shared/models/pagination-params-model';
import { InventoryFiltersComponent } from '../../components/inventory-filters-component/inventory-filters-component';
import { DatePipe } from '@angular/common';
import { PaginationComponent } from '../../../../shared/components/pagination-component/pagination-component';

@Component({
  imports: [
    InventoryFiltersComponent,
    DatePipe,
    PaginationComponent
],
  selector: 'app-inventory-page',
  styleUrl: './inventory-page.scss',
  templateUrl: './inventory-page.html',
})
export class InventoryPageComponent {
  private apiService = inject(InventoryApiService);
  pagination = signal<PaginationParams>({pageNumber: 1, pageSize: 10});
  filters = signal< InventoryFilters | undefined>(undefined);

  inventoryResource = rxResource({
    params: () =>({
      pagination: this.pagination(),
      filters: this.filters()
    }),
    stream: (ctx) => this.apiService.getAllPaged(
      ctx.params.pagination, 
      ctx.params.filters
    )
  });
  
  data = this.inventoryResource.value;        
  isLoading = this.inventoryResource.isLoading;
  error = this.inventoryResource.error;

  onPageChange(page: number): void {
    this.pagination.update(current => ({
      ...current,
      pageNumber: page
    }));
  }

  filterInventory(filters: InventoryFilters) {
    this.filters.set(filters); 

    this.pagination.update(current => ({
      ...current,
      pageNumber: 1
    }));
  }

  removeFilters() {
    this.filters.set(undefined); 

    this.pagination.update(current => ({
      ...current,
      pageNumber: 1
    }));
  }
}
