export interface InventoryDay {
  id: number;
  roomTypeId: number;
  stayDate: string;
  totalUnits: number;
  reservedUnits: number;
  availableUnits: number;
}