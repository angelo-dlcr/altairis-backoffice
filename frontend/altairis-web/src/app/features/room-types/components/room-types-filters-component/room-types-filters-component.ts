import { Component, output, signal } from '@angular/core';
import { RoomTypeFilters } from '../../models/room-type.model';
import { FormsModule } from '@angular/forms';
import { DropdownSelectComponent, DropdownSelectOption } from '../../../../shared/components/dropdown-select-component/dropdown-select-component';

@Component({
  imports: [FormsModule, DropdownSelectComponent],
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
  
  statusOptions: DropdownSelectOption<boolean | null>[] = [
    { value: null, label: 'Todos' },
    { value: true, label: 'Activo' },
    { value: false, label: 'Inactivo' },
  ];

  updateFilterField<K extends keyof RoomTypeFilters>(
    field: K, 
    value: RoomTypeFilters[K]
  ) {
    this.filters.update(currentFilters => ({
      ...currentFilters,
      [field]: value === '' ? null as RoomTypeFilters[K] : value
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
