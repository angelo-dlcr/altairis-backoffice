import { readonly } from "@angular/forms/signals";
import { RoomType } from "../../room-types/models/room-type.model";
import { Hotel } from "../../hotels/models/hotel.model";

export type ReservationStatus =
  | 'Pending'
  | 'Confirmed'
  | 'Cancelled';

export const RESERVATION_STATUSES: readonly ReservationStatus[] = 
  [ 'Pending', 'Confirmed', 'Cancelled' ]

export const ReservationStatusLabel: Record<ReservationStatus, string> = {
  Pending: 'Pendiente',
  Confirmed: 'Confirmada',
  Cancelled: 'Cancelada'
};

export interface Reservation {
  id: number;
  reference: string;
  roomTypeId: number;
  guestName: string;
  guestEmail?: string | null;
  checkInDate: string;
  checkOutDate: string;
  roomsCount: number;
  status: ReservationStatus;
  creationDate: string;
  roomType: RoomType | null,
  hotel: Hotel | null
}

export interface CreateReservationRequest {
  roomTypeId: number;
  guestName: string;
  guestEmail?: string | null;
  checkInDate: string;
  checkOutDate: string;
  roomsCount: number;
}

export class ReservationFilters {
  reference: string | null = null;
  roomTypeCode: number | null = null;
  guestName: string | null = null;
  status: ReservationStatus | null = null;
  checkInDate: string | null = null;
  checkOutDate: string | null = null;
}