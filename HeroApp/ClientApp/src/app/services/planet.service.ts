import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {  HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PlanetService {

  private _apiBaseUrl='api/Planet';

  constructor(private httpClient: HttpClient) {}

  getPlanets(): Observable<any> {
    return this.httpClient.get(`${this._apiBaseUrl}`) 
  }

  getPlanet(id): Observable<any> {
    return this.httpClient.get(`${this._apiBaseUrl}/${id}`) 
  }
  addPlanet(planet:any): Observable<any> {
    return this.httpClient.post(`${this._apiBaseUrl}`,planet) 
  }
  editPlanet(planet): Observable<any> {
    return this.httpClient.put(`${this._apiBaseUrl}`, planet) 
  }
  deletePlanet(id): Observable<any> {
    return this.httpClient.delete(`${this._apiBaseUrl}/${id}`) 
  }
}