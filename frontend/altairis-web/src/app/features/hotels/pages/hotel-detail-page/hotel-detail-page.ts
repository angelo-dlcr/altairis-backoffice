
import { Component, effect, inject, input, signal } from '@angular/core';
import { ToastService } from '../../../../shared/components/toast-component/service/toast-service';
import { RoomTypeApiService } from '../../../room-types/services/room-type-service';
import { RoomType } from '../../../room-types/models/room-type.model';

@Component({
  imports: [ ],
  selector: 'app-hotel-detail-page',
  styleUrl: './hotel-detail-page.scss',
  templateUrl: './hotel-detail-page.html',
})
export class HotelDetailPageComponent {
  private toastService = inject(ToastService);
  private roomTypeApiService = inject(RoomTypeApiService);
  
  currentHotelId = input.required<number>();

  roomTypes = signal<RoomType[]>([]);

  constructor() {
    effect(() => {
      const hotelId = this.currentHotelId();

      if (hotelId != undefined && hotelId != null)
        this.fetchRoomTypes(hotelId);
    })

  }
  
  fetchRoomTypes(hotelId: number) {
    this.roomTypeApiService.getAllByHotelId(hotelId).subscribe({
      next: (response) => {
        if (response.success) {
          this.roomTypes.set(response.items ?? []);
        }
      },
      error: (err) => {
        this.toastService.show(
          `Error en el servicio: ${err.error?.message ?? ''}`,
          'danger',
        )
      }
    });
  }
}
