import { Component, OnInit } from '@angular/core';
import { UserBrief } from 'src/app/models/users/UserBrief';
import { UserService } from 'src/app/services/user.service';
import { DialogService } from 'src/app/shared/services/dialog.service';
import { Router } from '@angular/router';
import { SessionStorageService } from 'src/app/services/session-storage.service';
import { LoadableComponentBase } from 'src/app/shared/utils/LoadableComponentBase';
import { User } from 'src/app/models/users/User';
import { HeaderNotificationService } from 'src/app/services/communication/header-notification.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent extends LoadableComponentBase implements OnInit {

  user: User = new User;
  userPassword: string = "";
  seePassword: boolean = false;
  seePassword2: boolean = false;
  passwordConfirmation = true;
  constructor(private userService: UserService, private dialogService: DialogService, private router: Router, private sessionStorageService: SessionStorageService, private headerServicenotification:HeaderNotificationService) { super(); }

  ngOnInit() {
  }

  save() {
    if (this.user.password !== this.userPassword) {
      this.passwordConfirmation = false;
    }
    else {
      this.userService.addUser(this.user).subscribe(result => {
        this.sessionStorageService.storeUser(result.token, true);
        this.sessionStorageService.storeUserID(result.id);
        this.user=result;
       this.headerServicenotification.notifyCurrentUserUpdated(this.user);
        this.router.navigate(['/properties']);
      }, err => {
        this.dialogService.showSimpleDialog("Eroare", err.message, 'error');
      })
    }
  }

  hidePassword() {
    document.getElementById('eye-close').style.display = 'block';
    document.getElementById('eye-open').style.display = 'none';
    this.seePassword = false;
  }

  showPassword() {
    document.getElementById('eye-close').style.display = 'none';
    document.getElementById('eye-open').style.display = 'block';
    this.seePassword = true;
  }
  hidePassword2() {
    document.getElementById('eye-close2').style.display = 'block';
    document.getElementById('eye-open2').style.display = 'none';
    this.seePassword2 = false;
  }

  showPassword2() {
    document.getElementById('eye-close2').style.display = 'none';
    document.getElementById('eye-open2').style.display = 'block';
    this.seePassword2 = true;
  }

}
