import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, CanActivate, CanActivateChild, Router } from '@angular/router';
import { Observable, of } from "rxjs";
import { catchError, map } from 'rxjs/operators';

import { UserService } from '../services/user.service';
import { User } from '../models/users/User';

@Injectable({
  providedIn: 'root'
})
export class AccessGuard implements CanActivate, CanActivateChild {

  path: ActivatedRouteSnapshot[];
  route: ActivatedRouteSnapshot;

  constructor(
    private router: Router,
    private userService: UserService) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    const minUserRole = route.data.minUserRole;
    if (!minUserRole) {
      return of(true);
    }
    return this.userService 
      .getUser()
      .pipe(
        catchError(e => of(e)),
        map(data => {
          const user = data as User;
          if (!user || !user.userRole) {
            this.router.navigateByUrl('/login');
            return false;
          }
          const result = user.userRole.id >= minUserRole;
          if (!result) {
            this.router.navigateByUrl('/unauthorized');
          }
          return result;
        })
      );
  }

  canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    return this.canActivate(route, state);
  }
}
