import { RoomType } from "../../room-types/models/room-type.model";

export interface Inventory {
  id: number;
  roomTypeId: number;
  hotelId: number | null;
  hotelCode: string | null;
  hotelName: string | null;
  roomTypeCode: string | null;
  roomTypeName: string | null;
  stayDate: string;
  totalUnits: number;
  creationDate: string;
  roomType: RoomType;
  reservedUnits: number;
  availableUnits: number;
}

export class InventoryFilters {
  roomTypeCode: string | null = null;
  stayDate: string | null = null;
  startDate: string | null = null;
  endDate: string | null = null;
}