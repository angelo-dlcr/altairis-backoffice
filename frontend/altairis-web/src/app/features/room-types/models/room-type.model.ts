export interface RoomType {
  id: number;
  hotelId: number;
  code: string;
  name: string;
  description?: string | null;
  maxOccupancy: number;
  isActive: boolean;
  creationDate: string;
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