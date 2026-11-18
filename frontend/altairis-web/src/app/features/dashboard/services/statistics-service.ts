import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { SingleApiResponse } from '../../../shared/models/api-response.model';
import { Statistics } from '../models/statistics';

@Injectable({
    providedIn: 'root'
})
export class StatisticsApiService {
    private readonly http = inject(HttpClient);
    private readonly apiUrl = `${environment.apiUrl}/Statistics`;

    getStatistics(): Observable<SingleApiResponse<Statistics>> {
        return this.http.get<SingleApiResponse<Statistics>>(`${this.apiUrl}/GetStatistics`);
    }
}
