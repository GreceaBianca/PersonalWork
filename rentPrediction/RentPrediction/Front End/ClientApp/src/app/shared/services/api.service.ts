import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { SharedModule } from '../shared.module';

@Injectable({
  providedIn: SharedModule
})
export class ApiService {

  constructor(
    private http: HttpClient
  ) { }

  get<T>(url: string): Observable<T> {
    return this.http.get<T>(url)
      .pipe(
        catchError(err => {
          throw err.error;
        })
      );
  }

  post<T>(url: string, param: any): Observable<T> {
    if (!param) param = {};
    return this.http.post<T>(url, param)
      .pipe(catchError(err => {
          throw err.error;
        })
      );
  }

  put<T>(url: string, param: any): Observable<T> {
    if (!param) param = {};
    return this.http.put<T>(url, param)
      .pipe(catchError(err => {
          throw err.error;
        })
      );
  }

  delete<T>(url: string): Observable<T> {
    return this.http.delete<T>(url)
      .pipe(catchError(err => {
          throw err.error;
        })
      );
  }
}

