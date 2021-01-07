import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ApiService } from '../shared/services/api.service';


@Injectable({
  providedIn: 'root'
})
export class ScraperService {
  private _apiBaseUrl = "api/Scraper";
  constructor(private apiService: ApiService) {}

  start(userId:number): Observable<any> {
    return this.apiService.get<any>(`${this._apiBaseUrl}/${userId}`);
  }


}
