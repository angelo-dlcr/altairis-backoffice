import { Injectable, inject } from '@angular/core';
import {
  HttpClient,
  HttpParams
} from '@angular/common/http';

import { Observable } from 'rxjs';

import { environment } from '../../../../environments/environment';

import {
  Inventory,
  InventoryFilters
} from '../models/inventory.model';
import { PaginationParams } from '../../../shared/models/pagination-params-model';
import { PagedListApiResponse } from '../../../shared/models/api-response.model';

@Injectable({
  providedIn: 'root'
})
export class InventoryApiService {

  private readonly http = inject(HttpClient);

  private readonly apiUrl = `${environment.apiUrl}/DailyInventory`;

  getAllPaged(
    pagination: PaginationParams, 
    filters: InventoryFilters | undefined)
  : Observable<PagedListApiResponse<Inventory>> 
  {
    const unifiedParams = {
      ...pagination,
      ...filters
    }

    var httpParams = this.flatParams(unifiedParams);

    return this.http.get<PagedListApiResponse<Inventory>>(
      `${this.apiUrl}/GetAllPaged`,
      {params: httpParams}
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