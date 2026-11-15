import { Hotel } from "../../hotels/models/hotel.model";

export interface RoomType {
  id: number;
  hotelId: number;
  hotelName: string;
  hotelCode: string;
  code: string;
  name: string;
  description?: string | null;
  maxOccupancy: number;
  isActive: boolean;
  creationDate: string;
  hotel: Hotel | null;
}

export interface CreateRoomTypeRequest {
  hotelId: number;
  code: string;
  name: string;
  description?: string | null;
  maxOccupancy: number;
}

export interface UpdateRoomTypeRequest extends CreateRoomTypeRequest {
  isActive: boolean;
}

export class RoomTypeFilters {
  nameOrCode: string | null = null;
  hotelCodeOrName: number | null = null;
  isActive: boolean | null = null;
  startDate: string | null = null;
  endDate: string | null = null;
}