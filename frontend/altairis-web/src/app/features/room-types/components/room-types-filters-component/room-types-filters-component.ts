import { Component, output, signal } from '@angular/core';
import { RoomTypeFilters } from '../../models/room-type.model';
import { FormsModule } from '@angular/forms';

@Component({
  imports: [FormsModule],
  selector: 'app-room-types-filters-component',
  styleUrl: './room-types-filters-component.scss',
  templateUrl: './room-types-filters-component.html',
})
export class RoomTypesFiltersComponent {
  onFilter = output<RoomTypeFilters>();
  onClear = output<void>();

  filters = signal<RoomTypeFilters>({
    nameOrCode: null,
    hotelCodeOrName: null,
    isActive: null,
    startDate: null,
    endDate: null,
  });

  updateFilterField(field: keyof RoomTypeFilters, value: any) {
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
      hotelCodeOrName: null,
      isActive: null,
      startDate: null,
      endDate: null,
    });

    this.onClear.emit();
  }
}
