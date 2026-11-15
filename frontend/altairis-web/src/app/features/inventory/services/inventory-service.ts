import { Injectable, inject } from '@angular/core';
import {
  HttpClient,
  HttpParams
} from '@angular/common/http';

import { Observable } from 'rxjs';

import { environment } from '../../../../environments/environment';

import {
  InventoryDay
} from '../models/inventory.model';

@Injectable({
  providedIn: 'root'
})
export class InventoryApiService {

  private readonly http = inject(HttpClient);

  private readonly apiUrl = environment.apiUrl;

  getByHotelAndDateRange(
    hotelId: number,
    fromDate: string,
    toDate: string
  ): Observable<InventoryDay[]> {

    const params = new HttpParams()
      .set('fromDate', fromDate)
      .set('toDate', toDate);

    return this.http.get<InventoryDay[]>(
      `${this.apiUrl}/hotels/${hotelId}/inventory`,
      { params }
    );
  }
}