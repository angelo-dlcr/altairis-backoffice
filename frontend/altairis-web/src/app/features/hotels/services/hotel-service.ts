import { Injectable, inject } from '@angular/core';

import {
  HttpClient,
  HttpParams
} from '@angular/common/http';

import {
  Observable
} from 'rxjs';

import { environment } from '../../../../environments/environment';

import {
  Hotel,
  CreateHotelRequest,
  UpdateHotelRequest,
  HotelFilters
} from '../models/hotel.model';
import { PaginationParams } from '../../../shared/models/pagination-params-model';
import { ListApiResponse, PagedListApiResponse } from '../../../shared/models/api-response.model';

@Injectable({
  providedIn: 'root'
})
export class HotelApiService {

  private readonly http = inject(HttpClient);

  private readonly apiUrl = `${environment.apiUrl}/Hotel`;

  getAll(): Observable<ListApiResponse<Hotel>> {
    return this.http.get<ListApiResponse<Hotel>>(this.apiUrl);
  }

  getById(id: number): Observable<Hotel> {
    return this.http.get<Hotel>(`${this.apiUrl}/${id}`);
  }

  getAllPaged(
    pagination: PaginationParams, 
    filters: HotelFilters | undefined
  ) : Observable<PagedListApiResponse<Hotel>> 
  {
    const unifiedParams = {
      ...pagination,
      ...filters
    }

    var httpParams = this.flatParams(unifiedParams);

    return this.http.get<PagedListApiResponse<Hotel>>(
      `${this.apiUrl}/GetAllPaged`,
      {params: httpParams}
    );
  }

  create(request: CreateHotelRequest): Observable<Hotel> {
    return this.http.post<Hotel>(
      this.apiUrl,
      request
    );
  }

  update(
    id: number,
    request: UpdateHotelRequest
  ): Observable<Hotel> {
    return this.http.put<Hotel>(
      `${this.apiUrl}/${id}`,
      request
    );
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(
      `${this.apiUrl}/${id}`
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