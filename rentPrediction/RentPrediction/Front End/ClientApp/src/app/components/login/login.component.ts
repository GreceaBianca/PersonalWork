import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { UserBrief } from 'src/app/models/users/UserBrief';
import { UserService } from 'src/app/services/user.service';
import { DialogService } from 'src/app/shared/services/dialog.service';
import { Router } from '@angular/router';
import { LoadableComponentBase } from 'src/app/shared/utils/LoadableComponentBase';
import { SessionStorageService } from 'src/app/services/session-storage.service';
import { HeaderNotificationService } from 'src/app/services/communication/header-notification.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent extends LoadableComponentBase  {

  user: UserBrief = new UserBrief;

  seePassword: boolean = false;
  email: string = "";

  modalRef: BsModalRef;

  constructor(private headerNotificationService: HeaderNotificationService,private modalService: BsModalService, private userService: UserService, private dialogService: DialogService, private router: Router, private sessionStorageService: SessionStorageService) { super(); }


  save() {
    this.userService.authenticate(this.user).subscribe(result => {
      this.sessionStorageService.storeUser(result.token, true);
      this.sessionStorageService.storeUserID(result.id);
      this.headerNotificationService.notifyCurrentUserUpdated(result.user);
      this.router.navigate(['/properties']);
    }, err => {
      this.dialogService.showSimpleDialog("Eroare", err.message, 'error');
      this.user = new UserBrief;
    })
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

  sendEmail() {
    this.userService.sendEmail(this.email).subscribe(result => {
      this.dialogService.showSimpleDialog("Succes", "Va puteti verifica adresa", 'success');
      this.modalRef.hide();
    }, err => {
      this.dialogService.showSimpleDialog("Eroare", err.message, 'error');
      this.modalRef.hide();
    });
  }


  openModal(template: TemplateRef<any>) {
    this.email = "";
    this.modalRef = this.modalService.show(template, { backdrop: "static" });
  }

}
