import { Component, OnInit, Input } from '@angular/core';
import { SelfUnsubscriberBase } from 'src/app/shared/utils/SelfUnsubscriberBase';
import { HeaderNotificationService } from 'src/app/services/communication/header-notification.service';
import { User } from 'src/app/models/users/User';
import { NavMenuItem } from '../utils/NavMenuItem';
import { Router } from '@angular/router';
import { appMenuItems } from '../utils/MenuConfig';

@Component({
  selector: 'rent-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent extends SelfUnsubscriberBase implements OnInit {

  @Input() user: User;

  constructor(
    private headerNotificationService: HeaderNotificationService, private router: Router
  ) {
    super();
    headerNotificationService.currentUserUpdated$.subscribe(user => {
      this.user = user;
      if (this.user) {
        const userRoleId = this.user.userRole.id;
        this.menuItems = appMenuItems.filter(mi => mi.route).filter(mi => !mi.route || !mi.route.data || !mi.route.data.minUserRole || userRoleId >= mi.route.data.minUserRole || mi.name !== 'Login');
        let index=this.menuItems.findIndex(mi=>mi.name==="Login");
        this.menuItems.splice(index,1);
      }
      else {
        this.menuItems = appMenuItems.filter(mi => mi.route).filter(mi => !mi.route || !mi.route.data || !mi.route.data.minUserRole);
      }
    });
  }

  menuItems: NavMenuItem[] = [];

  ngOnInit() {
    if (this.user) {
      const userRoleId = this.user.userRole.id;
      this.menuItems = appMenuItems.filter(mi => mi.route).filter(mi => !mi.route || !mi.route.data || !mi.route.data.minUserRole || userRoleId >= mi.route.data.minUserRole );
      let index=this.menuItems.findIndex(mi=>mi.name==="Login");
      this.menuItems.splice(index,1);
    }
    else {
      this.menuItems = appMenuItems.filter(mi => mi.route).filter(mi => !mi.route || !mi.route.data || !mi.route.data.minUserRole);
    }
  }

  onMenuItemClicked(path: string, menuItem: NavMenuItem) {

    if (path !== undefined)
      this.router.navigate([path]);
  }

  expandMenu(menuItem: NavMenuItem) {

    if (menuItem.displayedChildren === true) {
      menuItem.displayedChildren = false;
    }
    else {
      menuItem.displayedChildren = true;
    }

  }

  isMenuItemActive(menuItem: NavMenuItem) {

    const routeElements = this.router.routerState.snapshot.url.split('/');
    const currentUrl = routeElements[routeElements.length - 1];
    return currentUrl == menuItem.route.path;
  }
}
