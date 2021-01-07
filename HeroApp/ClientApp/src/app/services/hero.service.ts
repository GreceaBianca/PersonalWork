import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {  HttpClient } from '@angular/common/http';
import { Hero } from '../models/Hero';

@Injectable({
  providedIn: 'root'
})
export class HeroService {

  private _apiBaseUrl='api/Hero';

  constructor(private httpClient: HttpClient) {}

  getHeros(): Observable<Hero[]> {
    return this.httpClient.get<Hero[]>(`${this._apiBaseUrl}`) 
  }

  getHero(id): Observable<Hero> {
    return this.httpClient.get<Hero>(`${this._apiBaseUrl}/${id}`) 
  }
  addHero(hero:Hero): Observable<Hero> {
    return this.httpClient.post<Hero>(`${this._apiBaseUrl}`,hero) 
  }
  editHero(hero): Observable<Hero> {
    return this.httpClient.put<Hero>(`${this._apiBaseUrl}`, hero) 
  }
  deleteHero(id): Observable<Hero> {
    return this.httpClient.delete<Hero>(`${this._apiBaseUrl}/${id}`) 
  }
}