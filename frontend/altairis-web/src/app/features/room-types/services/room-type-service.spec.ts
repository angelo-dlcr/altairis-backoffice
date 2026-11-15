import { TestBed } from '@angular/core/testing';
import { RoomTypeApi } from './room-type-service';

describe('RoomTypeApi', () => {
  let service: RoomTypeApi;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RoomTypeApi);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
