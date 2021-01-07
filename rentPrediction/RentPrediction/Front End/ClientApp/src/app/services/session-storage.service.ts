import { Injectable } from '@angular/core';
import { Inject } from '@angular/core';
import { StorageService, SESSION_STORAGE } from 'ngx-webstorage-service';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class SessionStorageService {
  remember: boolean = true;
  constructor(@Inject(SESSION_STORAGE) private storage: StorageService, private cookieService: CookieService) { }

 
  public storeUser(token: string, rememberMe: boolean): void {
    this.remember = rememberMe;
    if (rememberMe) {
      this.cookieService.set('UserLogged', token);
    }
    else {
      localStorage.setItem('UserLogged', token);
    }
  }

  public isLogged(): boolean {
    if (this.remember)
      return this.cookieService.check('UserLogged');
    else
      return localStorage.getItem('UserLogged') != undefined;
  }
  public getUser() {
    if (this.remember)
      return this.cookieService.get('UserLogged');
    else
      return localStorage.getItem('UserLogged');
  }
  public storeUserID(id: number): void {
    if (this.remember)
      this.cookieService.set('UserID', id.toString());
    else {
      localStorage.setItem('UserID', id.toString());
    }
  }
  public getUserID(): number {
    if (this.remember)
      return +this.cookieService.get('UserID');
    return +localStorage.getItem('UserID');
  }
 
  public deleteStorage(): void {
    this.cookieService.delete('UserID');
    this.cookieService.delete('UserLogged');
  }
}
