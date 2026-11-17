import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../../environments/environment';

import {
  RoomType,
  CreateRoomTypeRequest,
  UpdateRoomTypeRequest,
  RoomTypeFilters
} from '../models/room-type.model';
import { ListApiResponse, PagedListApiResponse, SingleApiResponse } from '../../../shared/models/api-response.model';
import { PaginationParams } from '../../../shared/models/pagination-params-model';

@Injectable({
  providedIn: 'root'
})
export class RoomTypeApiService {

  private readonly http = inject(HttpClient);

  private readonly apiUrl = `${environment.apiUrl}/RoomType`;

  getAllPaged(
    pagination: PaginationParams, 
    filters: RoomTypeFilters | undefined
  ) : Observable<PagedListApiResponse<RoomType>> 
  {
    const unifiedParams = {
      ...pagination,
      ...filters
    }

    var httpParams = this.flatParams(unifiedParams);

    return this.http.get<PagedListApiResponse<RoomType>>(
      `${this.apiUrl}/GetAllPaged`,
      {params: httpParams}
    );
  }

  getAllByHotelId(id: number) : Observable<ListApiResponse<RoomType>> 
  {
    return this.http.get<ListApiResponse<RoomType>>(
      `${this.apiUrl}/GetAllByHotelId`,
      {params: { hotelId: id} }
    );
  }

  getById(id: number): Observable<RoomType> {
    return this.http.get<RoomType>(
      `${this.apiUrl}/${id}`
    );
  }

  create(request: CreateRoomTypeRequest): Observable<SingleApiResponse<boolean>> {
    return this.http.post<SingleApiResponse<boolean>>(
      `${this.apiUrl}/Create`,
      request
    );
  }
  
  changeStatus(id: number, activate: boolean): Observable<SingleApiResponse<boolean>> {
    return this.http.put<SingleApiResponse<boolean>>(
      `${this.apiUrl}/ChangeStatus/${id}`,
      activate
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