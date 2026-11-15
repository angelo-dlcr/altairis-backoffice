import { Injectable, inject } from '@angular/core';

import {
  HttpClient,
  HttpParams
} from '@angular/common/http';

import { Observable } from 'rxjs';

import { environment } from '../../../../environments/environment';

import {
  Reservation,
  CreateReservationRequest,
  ReservationStatus
} from '../models/reservation.model';

export interface ReservationFilter {
  hotelId?: number;
  fromDate?: string;
  toDate?: string;
  status?: ReservationStatus;
}

@Injectable({
  providedIn: 'root'
})
export class ReservationApiService {

  private readonly http = inject(HttpClient);

  private readonly apiUrl =
    `${environment.apiUrl}/reservations`;

  getAll(
    filters?: ReservationFilter
  ): Observable<Reservation[]> {

    let params = new HttpParams();

    if (filters?.hotelId !== undefined) {
      params = params.set('hotelId', filters.hotelId);
    }

    if (filters?.fromDate) {
      params = params.set('fromDate', filters.fromDate);
    }

    if (filters?.toDate) {
      params = params.set('toDate', filters.toDate);
    }

    if (filters?.status) {
      params = params.set('status', filters.status);
    }

    return this.http.get<Reservation[]>(
      this.apiUrl,
      { params }
    );
  }

  getById(id: number): Observable<Reservation> {
    return this.http.get<Reservation>(
      `${this.apiUrl}/${id}`
    );
  }

  create(
    request: CreateReservationRequest
  ): Observable<Reservation> {
    return this.http.post<Reservation>(
      this.apiUrl,
      request
    );
  }

  updateStatus(
    id: number,
    status: ReservationStatus
  ): Observable<Reservation> {
    return this.http.patch<Reservation>(
      `${this.apiUrl}/${id}/status`,
      { status }
    );
  }
}