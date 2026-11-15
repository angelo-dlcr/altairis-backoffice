import { Component, inject, output, signal } from '@angular/core';
import { InventoryFilters } from '../../models/inventory.model';
import { FormsModule } from '@angular/forms';

@Component({
  imports: [FormsModule],
  selector: 'app-inventory-filters-component',
  styleUrl: './inventory-filters-component.scss',
  templateUrl: './inventory-filters-component.html',
})
export class InventoryFiltersComponent {
  roomTypeCodes: string[] = [];

  onFilter = output<InventoryFilters>();
  onClear = output<void>();

  filters = signal<InventoryFilters>({
    roomTypeCode: null,
    stayDate: null,
    startDate: null,
    endDate: null,
  });

  updateFilterField(field: keyof InventoryFilters, value: any) {
    this.filters.update(currentFilters => ({
      ...currentFilters,
      [field]: value === '' ? null : value
    }));
  }

  sendFilters() {
    this.onFilter.emit(this.filters());
  }


  clearFilters() {
    this.filters.set({
    roomTypeCode: null,
    stayDate: null,
    startDate: null,
    endDate: null,
    });

    this.onClear.emit();
  }
}
