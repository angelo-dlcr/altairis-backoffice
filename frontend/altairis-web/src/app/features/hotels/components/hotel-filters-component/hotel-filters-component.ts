import { Component, output, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HotelFilters } from '../../models/hotel.model';
import { DropdownSelectComponent, DropdownSelectOption } from '../../../../shared/components/dropdown-select-component/dropdown-select-component';

@Component({
  imports: [FormsModule, DropdownSelectComponent],
  selector: 'app-hotel-filters-component',
  styleUrl: './hotel-filters-component.scss',
  templateUrl: './hotel-filters-component.html',
})
export class HotelFiltersComponent {
  onFilter = output<HotelFilters>();
  onClear = output<void>();

  filters = signal<HotelFilters>({
    nameOrCode: null,
    country: null,
    city: null,
    isActive: null,
    startDate: null,
    endDate: null,
  });

  statusOptions: DropdownSelectOption<boolean | null>[] = [
    { value: null, label: 'Todos' },
    { value: true, label: 'Activo' },
    { value: false, label: 'Inactivo' },
  ];
  
  updateFilterField(field: keyof HotelFilters, value: any) {
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
      nameOrCode: null,
      country: null,
      city: null,
      isActive: null,
      startDate: null,
      endDate: null,
    });

    this.onClear.emit();
  }
}
