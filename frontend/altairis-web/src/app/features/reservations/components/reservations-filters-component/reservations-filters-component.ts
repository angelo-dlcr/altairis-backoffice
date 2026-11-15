import { Component, output, signal } from '@angular/core';
import { RESERVATION_STATUSES, ReservationFilters, ReservationStatus, ReservationStatusLabel } from '../../models/reservation.model';
import { FormsModule } from '@angular/forms';

@Component({
  imports: [FormsModule],
  selector: 'app-reservations-filters-component',
  styleUrl: './reservations-filters-component.scss',
  templateUrl: './reservations-filters-component.html',
})
export class ReservationsFiltersComponent {
  onFilter = output<ReservationFilters>();
  onClear = output<void>();

  readonly statuses = RESERVATION_STATUSES;
  readonly statusLabels = ReservationStatusLabel;

  filters = signal<ReservationFilters>({
    reference: null,
    roomTypeCode: null,
    guestName: null,
    status: null,
    checkInDate: null,
    checkOutDate: null,
  });

  updateFilterField(field: keyof ReservationFilters, value: any) {
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
      reference: null,
      roomTypeCode: null,
      guestName: null,
      status: null,
      checkInDate: null,
      checkOutDate: null,
    });

    this.onClear.emit();
  }

}
