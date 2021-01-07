import { Route } from '@angular/router';

export class NavMenuItem {
  name?: string;
  route?: Route;
  children?: NavMenuItem[];
  displayedChildren?: boolean;
  class?: string ='';
}
