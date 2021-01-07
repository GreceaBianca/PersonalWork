import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ApiService } from '../shared/services/api.service';

import { SessionStorageService } from './session-storage.service';

import { Favorite } from '../models/favorite/Favorite';
import { Property } from '../models/properties/Property';

@Injectable({
  providedIn: 'root'
})
export class FavoriteService {
  private _apiBaseUrl = "api/Favorite";
  constructor(private apiService: ApiService, private storageService:SessionStorageService) {}

  getFavoritesByUserId(userId:number): Observable<Favorite[]> {
    return this.apiService.get<Favorite[]>(`${this._apiBaseUrl}/User/${userId}`);
  }  

  getFavoriteById(id:number): Observable<Favorite> {
    return this.apiService.get<Favorite>(`${this._apiBaseUrl}/${id}`);
  }  
  getFavoritesPropertiesByUserId(userId:number):Observable<Property[]>{
    return this.apiService.get<Property[]>(`${this._apiBaseUrl}/Property/${userId}`);
  }  

  getFavorites(): Observable<Favorite[]> {
    return this.apiService.get<Favorite[]>(`${this._apiBaseUrl}`);
  }
  
  addFavorite(Favorite: Favorite): Observable<Favorite> {
    return this.apiService.post(`${this._apiBaseUrl}`, Favorite);
  }

  editFavorite(Favorite: Favorite): Observable<Favorite> {
    return this.apiService.put(`${this._apiBaseUrl}`, Favorite);
  }

  deleteFavorite(id: number): Observable<any> {
    return this.apiService.delete(`${this._apiBaseUrl}/${id}`);
  }

}
