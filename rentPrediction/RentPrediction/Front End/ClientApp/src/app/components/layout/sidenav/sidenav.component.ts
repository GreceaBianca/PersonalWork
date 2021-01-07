import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { SelfUnsubscriberBase } from '../../../shared/utils/SelfUnsubscriberBase';

import { NavMenuItem } from "../utils/NavMenuItem";
import { appMenuItems } from '../utils/MenuConfig';
import { User } from '../../../models/users/User';

@Component({
  selector: 'rent-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.scss']
})
export class SidenavComponent extends SelfUnsubscriberBase implements OnInit {
  
  constructor(private router: Router) {
    super();
  }
  @Input() user: User;
  menuItems: NavMenuItem[] = [];

  ngOnInit() {
    const userRoleId = this.user.userRole.id;
    this.menuItems = appMenuItems.filter(mi => mi.route).filter(mi => !mi.route || !mi.route.data || !mi.route.data.minUserRole || userRoleId >= mi.route.data.minUserRole);
    
  }

  onMenuItemClicked(path: string, menuItem: NavMenuItem) {
    
    if(path!==undefined)
    this.router.navigate([path]);
  }

  expandMenu(menuItem: NavMenuItem){

    if(menuItem.displayedChildren === true)
    {
      menuItem.displayedChildren = false;
    }
    else
    {
      menuItem.displayedChildren = true;
    }
    
  }

  isMenuItemActive(menuItem: NavMenuItem) {

    const routeElements = this.router.routerState.snapshot.url.split('/');
    const currentUrl = routeElements[routeElements.length - 1];
    return currentUrl == menuItem.route.path;
  }
}
