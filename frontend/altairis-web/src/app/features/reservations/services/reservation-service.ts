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
  ReservationStatus,
  ReservationFilters
} from '../models/reservation.model';
import { PaginationParams } from '../../../shared/models/pagination-params-model';
import { PagedListApiResponse } from '../../../shared/models/api-response.model';

@Injectable({
  providedIn: 'root'
})
export class ReservationApiService {

  private readonly http = inject(HttpClient);

  private readonly apiUrl =
    `${environment.apiUrl}/Reservation`;

  getAllPaged(pagination: PaginationParams, filters: ReservationFilters | undefined)
  : Observable<PagedListApiResponse<Reservation>> 
  {
    const unifiedParams = {
      ...pagination,
      ...filters
    }

    var httpParams = this.flatParams(unifiedParams);

    return this.http.get<PagedListApiResponse<Reservation>>(
      `${this.apiUrl}/GetAllPaged`,
      {params: httpParams}
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
  
  private flatParams(params: any) : HttpParams {
    let httpParams = new HttpParams();

    if (params) {
      Object.keys(params).forEach(key => {
        const value = (params as any)[key];
        
        if (value !== undefined && value !== null && value !== '') {
          httpParams = httpParams.set(key, value.toString());
        }
      });
    }

    return httpParams;
  }
}