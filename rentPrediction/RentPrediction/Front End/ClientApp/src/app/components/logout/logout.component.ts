import { Component, OnInit } from '@angular/core';
import { SessionStorageService } from 'src/app/services/session-storage.service';
import { Router } from '@angular/router';
import { HeaderNotificationService } from 'src/app/services/communication/header-notification.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent implements OnInit {

  constructor(private sessionStorageService:SessionStorageService, private router:Router, private headerNotificationService: HeaderNotificationService) { }

  ngOnInit() {
    this.sessionStorageService.deleteStorage();
    this.headerNotificationService.notifyCurrentUserUpdated(null);
    this.router.navigate(['login']);
  }

}
