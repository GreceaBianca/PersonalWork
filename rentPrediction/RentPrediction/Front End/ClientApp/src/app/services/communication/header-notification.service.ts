
import { Injectable } from '@angular/core';
import { User } from '../../models/users/User';
import { Subject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class HeaderNotificationService {

    private currentUserUpdatedSource = new Subject<User>();

    currentUserUpdated$ = this.currentUserUpdatedSource.asObservable();

    notifyCurrentUserUpdated(user: User){
        this.currentUserUpdatedSource.next(user);
    }
}
