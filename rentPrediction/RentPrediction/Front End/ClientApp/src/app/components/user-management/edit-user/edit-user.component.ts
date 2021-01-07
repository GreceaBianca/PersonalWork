import { Component, OnInit } from '@angular/core';
import { UserBrief } from 'src/app/models/users/UserBrief';
import { UserService } from 'src/app/services/user.service';
import { DialogService } from 'src/app/shared/services/dialog.service';
import { Router } from '@angular/router';
import { SessionStorageService } from 'src/app/services/session-storage.service';
import { LoadableComponentBase } from 'src/app/shared/utils/LoadableComponentBase';
import { User } from 'src/app/models/users/User';
import { removeSummaryDuplicates } from '@angular/compiler';
@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.scss']
})
export class EditUserComponent extends LoadableComponentBase implements OnInit {

  user: User = new User;
  constructor(private userService: UserService, 
    private dialogService: DialogService, 
    private router: Router, 
    private sessionStorageService: SessionStorageService) { super(); }

  ngOnInit() {
    this.startLoader();
    this.userService.getUser().subscribe(result=>{
      this.user=result;
      this.stopLoader();
    }, err=>{
      this.stopLoader();
    });
  }

  save() {
    this.startLoader();
    this.userService.editUser(this.user).subscribe(result => {
      this.stopLoader();
      this.dialogService.showSimpleDialog("Succes", "Datele au fost salvate cu succes", 'success');
    }, err => {
      this.stopLoader();
      this.dialogService.showSimpleDialog("Eroare", err.message, 'error');
    })
  }
}
