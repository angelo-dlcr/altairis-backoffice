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
  id: number;
  isActive: boolean;
}

export class RoomTypeFilters {
  nameOrCode: string | null = null;
  hotelCodeOrName: number | null = null;
  isActive: boolean | null = null;
  startDate: string | null = null;
  endDate: string | null = null;
}

export type RoomTypeCode =
    | 'SGL'
    | 'DBL'
    | 'TWN'
    | 'TPL'
    | 'QUAD'
    | 'STD'
    | 'SUP'
    | 'DLX'
    | 'FAM'
    | 'STU'
    | 'JSU'
    | 'STE'
    | 'EXEC'
    | 'APT'
    | 'VIL'
    | 'ACC'
;

export const ROOM_TYPE_CODES: readonly RoomTypeCode[] = 
[
    'SGL',
    'DBL',
    'TWN',
    'TPL',
    'QUAD',
    'STD',
    'SUP',
    'DLX',
    'FAM',
    'STU',
    'JSU',
    'STE',
    'EXEC',
    'APT',
    'VIL',
    'ACC',
]

export const RoomTypeCodeLabels: Record<RoomTypeCode, string> = {
    SGL: 'Single',
    DBL: 'Double',
    TWN: 'Twin',
    TPL: 'Triple',
    QUAD: 'Quadruple',
    STD: 'Standard',
    SUP: 'Superior',
    DLX: 'Deluxe',
    FAM: 'Family',
    JSU: 'Junior Suite',
    STE: 'Suite',
    EXEC: 'Executive',
    STU: 'Studio',
    APT: 'Apartment',
    VIL: 'Villa',
    ACC: 'Accessible'
};
