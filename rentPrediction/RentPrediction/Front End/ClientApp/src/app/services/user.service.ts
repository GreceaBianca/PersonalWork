import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ApiService } from '../shared/services/api.service';
import { User } from '../models/users/User';
import { UserRole } from '../models/users/UserRole';
import { UserBrief } from '../models/users/UserBrief';
import { HttpHeaders } from '@angular/common/http';
import { SessionStorageService } from './session-storage.service';
import { UserResetPassword } from '../models/users/UserResetPassword';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private _apiBaseUrl = "api/User";
  constructor(private apiService: ApiService, private storageService:SessionStorageService) {}

  getUser(): Observable<User> {
    let id=this.storageService.getUserID();
    return this.apiService.get<User>(`${this._apiBaseUrl}/${id}`);
  }  

  getUserById(id:number): Observable<User> {
    return this.apiService.get<User>(`${this._apiBaseUrl}/${id}`);
  }  

  sendEmail(email:string): Observable<any> {
    return this.apiService.get<any>(`${this._apiBaseUrl}/Email/${email}`);
  }

  authenticate(user:UserBrief): Observable<any> {
    return this.apiService.post<any>(`${this._apiBaseUrl}/authenticate`, user);
  }

  getUsers(): Observable<User[]> {
    return this.apiService.get<User[]>(`${this._apiBaseUrl}`);
  }
  
  addUser(user: User): Observable<User> {
    return this.apiService.post(`${this._apiBaseUrl}`, user);
  }

  editUser(user: User): Observable<User> {
    return this.apiService.put(`${this._apiBaseUrl}`, user);
  }

  resetUserPassword(user: UserResetPassword): Observable<User> {
    return this.apiService.put(`${this._apiBaseUrl}/Reset`, user);
  }

  deleteUser(id: number): Observable<any> {
    return this.apiService.delete(`${this._apiBaseUrl}/${id}`);
  }

  getUserRoles(): Observable<UserRole[]> {
    return this.apiService.get<UserRole[]>(`${this._apiBaseUrl}/UserRoles`);
  }
}
