import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ApiService } from '../shared/services/api.service';
import { Property } from '../models/properties/Property';


@Injectable({
  providedIn: 'root'
})
export class PropertyService {
  private _apiBaseUrl = "api/Property";
  constructor(private apiService: ApiService) {}

  getAll(): Observable<Property[]> {
    return this.apiService.get<Property[]>(`${this._apiBaseUrl}`);
  }
  getAllUserProperties(userId:number): Observable<Property[]> {
    return this.apiService.get<Property[]>(`${this._apiBaseUrl}/User/${userId}`);
  }
  getById(id:number): Observable<Property> {
    return this.apiService.get<Property>(`${this._apiBaseUrl}/${id}`);
  }
  update(property:Property): Observable<Property> {
    return this.apiService.put<Property>(`${this._apiBaseUrl}`, property);
  }
  delete(id:number): Observable<Property> {
    return this.apiService.delete<Property>(`${this._apiBaseUrl}/${id}`);
  }
}
