import { Component, output, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HotelFilters } from '../../models/hotel.model';

@Component({
  imports: [FormsModule],
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
