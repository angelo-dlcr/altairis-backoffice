import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../../environments/environment';

import {
  RoomType,
  CreateRoomTypeRequest,
  UpdateRoomTypeRequest
} from '../models/room-type.model';

@Injectable({
  providedIn: 'root'
})
export class RoomTypeApiService {

  private readonly http = inject(HttpClient);

  private readonly apiUrl =
    `${environment.apiUrl}/room-types`;

  getByHotel(hotelId: number): Observable<RoomType[]> {
    return this.http.get<RoomType[]>(
      `${environment.apiUrl}/hotels/${hotelId}/room-types`
    );
  }

  getById(id: number): Observable<RoomType> {
    return this.http.get<RoomType>(
      `${this.apiUrl}/${id}`
    );
  }

  create(request: CreateRoomTypeRequest): Observable<RoomType> {
    return this.http.post<RoomType>(
      this.apiUrl,
      request
    );
  }

  update(
    id: number,
    request: UpdateRoomTypeRequest
  ): Observable<RoomType> {
    return this.http.put<RoomType>(
      `${this.apiUrl}/${id}`,
      request
    );
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(
      `${this.apiUrl}/${id}`
    );
  }
}