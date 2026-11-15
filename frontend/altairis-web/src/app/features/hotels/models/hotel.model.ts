export interface Hotel {
  id: number;
  code: string;
  name: string;
  country: string;
  city: string;
  address?: string | null;
  isActive: boolean;
  creationDate: string;
}

export interface CreateHotelRequest {
  code: string;
  name: string;
  country: string;
  city: string;
  address?: string | null;
}

export interface UpdateHotelRequest extends CreateHotelRequest {
  isActive: boolean;
}

export class HotelFilters {
  nameOrCode: string | null = null;
  country: string | null = null;
  city: string | null = null;
  isActive: boolean | null = null;
  startDate: string | null = null;
  endDate: string | null = null;
}