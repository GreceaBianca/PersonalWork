import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { Sidekick } from '../models/Sidekick';

@Injectable({
  providedIn: 'root'
})
export class SidekickService {

  private _apiBaseUrl='api/Sidekick';

  constructor(private apiService: ApiService) {}

  getSidekicks(): Observable<Sidekick[]> {
    return this.apiService.get<Sidekick[]>(`${this._apiBaseUrl}`) ;
  }

  getSidekick(id): Observable<Sidekick> {
    return this.apiService.get(`${this._apiBaseUrl}/${id}`) ;
  }
  addSidekick(sidekick:Sidekick): Observable<Sidekick> {
    return this.apiService.post(`${this._apiBaseUrl}`,sidekick) ;
  }
  editSidekick(sidekick:Sidekick): Observable<Sidekick> {
    return this.apiService.put(`${this._apiBaseUrl}`, sidekick) ;
  }
  deleteSidekick(id:number): Observable<any> {
    return this.apiService.delete<Sidekick>(`${this._apiBaseUrl}/${id}`) ;
  }
}
