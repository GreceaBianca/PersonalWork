import { Component, OnInit } from '@angular/core';
import { LoadableComponentBase } from 'src/app/shared/utils/LoadableComponentBase';
import { Router } from '@angular/router';
import { SessionStorageService } from 'src/app/services/session-storage.service';
import { HeaderNotificationService } from 'src/app/services/communication/header-notification.service';
import { UserService } from 'src/app/services/user.service';
import { ScraperService } from 'src/app/services/scraper.service';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.scss']
})
export class UserManagementComponent extends LoadableComponentBase implements OnInit {
  isAdmin:boolean;
  constructor(private scraperService:ScraperService,private router:Router,private userService:UserService, private sessionStorageService:SessionStorageService, private headerNotificationService:HeaderNotificationService) {super(); }

  ngOnInit() {
    this.userService.getUserById(this.sessionStorageService.getUserID()).subscribe(result=>{
      this.isAdmin=result.userRole.name==="Admin";
    })
  }

  goToEdit(){
    this.router.navigate(['account/edit']);
  }
  goToResetPassword(){
    this.router.navigate(['account/change-password']);
  }
  logout(){
    this.sessionStorageService.deleteStorage();
    this.headerNotificationService.notifyCurrentUserUpdated(null);
    this.router.navigate(['login']);
  }
  goToUserProperties(){
    this.router.navigate(['account/my-properties']);
  }
  goToFavoriteProperties(){
    this.router.navigate(['account/favorites']);
  }
  startScraping(){
    this.startLoader();
    this.scraperService.start(this.sessionStorageService.getUserID()).subscribe(result=>{
      this.stopLoader();
    }, err=>{
      this.stopLoader();
    })
  }
  goToAdmin(){
    this.router.navigate(['account/admin']);
  }
}
