export type ReservationStatus =
  | 'Pending'
  | 'Confirmed'
  | 'Cancelled';

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
}

export interface CreateReservationRequest {
  roomTypeId: number;
  guestName: string;
  guestEmail?: string | null;
  checkInDate: string;
  checkOutDate: string;
  roomsCount: number;
}