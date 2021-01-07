import { Component, OnInit } from '@angular/core';
import { LoadableComponentBase } from 'src/app/shared/utils/LoadableComponentBase';
import { ScraperService } from 'src/app/services/scraper.service';
import { User } from 'src/app/models/users/User';
import { UserService } from 'src/app/services/user.service';
import { DialogService } from 'src/app/shared/services/dialog.service';
import { clone } from 'src/app/shared/utils/Utils';
import { SessionStorageService } from 'src/app/services/session-storage.service';

@Component({
  selector: 'app-admin-management',
  templateUrl: './admin-management.component.html',
  styleUrls: ['./admin-management.component.scss']
})
export class AdminManagementComponent extends LoadableComponentBase implements OnInit {
  users:User[]=[];
  editUser:User=new User();
  constructor(private userService:UserService,
     private dialogService:DialogService, 
     private sessionStorageService:SessionStorageService) { super(); }

  ngOnInit() {
    this.startLoader();{
      this.userService.getUsers().subscribe(result=>{
        this.users=result;
        let index=this.users.findIndex(u=>u.id==this.sessionStorageService.getUserID());
        this.users.splice(index,1);
        this.stopLoader();
      },err=>{
        this.stopLoader();
      })
    }
  }
  makeAdmin(user){
    this.editUser=clone(user);
    this.editUser.roleId=3;
    this.editUser.userRole=null;
    this.update(user);
  }
  makeBasicUser(user){
    this.editUser=clone(user);
    this.editUser.roleId=1;
    this.editUser.userRole=null;
    this.update(user);
  }
  update(user){
    this.userService.editUser(this.editUser).subscribe(result=>{
      if(result){
        let index=this.users.findIndex(e=>e.id==result.id);
        this.users[index].roleId=result.roleId;
        this.dialogService.showSimpleDialog("Succes", "Datele au fost salvate cu succes", 'success');
      }
    }, err => {
      this.dialogService.showSimpleDialog("Eroare", "Am intampinat o eroare", 'error');
    })
  }


}
