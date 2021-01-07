import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SelfUnsubscriberBase } from '../../shared/utils/SelfUnsubscriberBase';

import { User } from '../../models/users/User';
import { UserRole } from 'src/app/models/users/UserRole';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent extends SelfUnsubscriberBase implements OnInit {

  constructor(private route: ActivatedRoute) {
    super();
  }

  user: User;

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data.user;
    });
  }
}
