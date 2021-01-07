
import { Component, OnInit } from '@angular/core';
import { UserBrief } from 'src/app/models/users/UserBrief';
import { UserService } from 'src/app/services/user.service';
import { DialogService } from 'src/app/shared/services/dialog.service';
import { Router } from '@angular/router';
import { SessionStorageService } from 'src/app/services/session-storage.service';
import { LoadableComponentBase } from 'src/app/shared/utils/LoadableComponentBase';
import { User } from 'src/app/models/users/User';
import { HeaderNotificationService } from 'src/app/services/communication/header-notification.service';
import { UserResetPassword } from 'src/app/models/users/UserResetPassword';
@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent extends LoadableComponentBase implements OnInit {

  user: UserResetPassword = new UserResetPassword;

  oldUserPassword: string = "";
  newUserPassword: string = "";
  newUserPasswordConfirmed = "";

  seePassword: boolean = false;
  seePassword2: boolean = false;
  seePassword3: boolean = false;
  passwordConfirmation = true;
  passwordValidation = true;
  constructor(private userService: UserService, private dialogService: DialogService, private router: Router, private sessionStorageService: SessionStorageService, private headerServicenotification: HeaderNotificationService) { super(); }

  ngOnInit() {
    this.user.id=this.sessionStorageService.getUserID();
  }

  save() {
    if (this.newUserPassword !== this.newUserPasswordConfirmed) {
      this.passwordConfirmation = false;
    }
    else {
      this.user.oldPassword=this.oldUserPassword;
      this.user.newPassword=this.newUserPassword;
        this.startLoader();
        this.userService.resetUserPassword(this.user).subscribe(result => {
          this.stopLoader();
          this.dialogService.showSimpleDialog("Succes", "Datele au fost salvate cu succes", 'success');
        }, err => {
          this.stopLoader();
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

  hidePassword3() {
    document.getElementById('eye-close3').style.display = 'block';
    document.getElementById('eye-open3').style.display = 'none';
    this.seePassword3 = false;
  }

  showPassword3() {
    document.getElementById('eye-close3').style.display = 'none';
    document.getElementById('eye-open3').style.display = 'block';
    this.seePassword3 = true;
  }

}
