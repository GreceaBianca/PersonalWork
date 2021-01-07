import { Component, OnInit } from '@angular/core';
import { LoadableComponentBase } from 'src/app/shared/utils/LoadableComponentBase';

@Component({
  selector: 'rent-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent extends LoadableComponentBase implements OnInit {
  constructor(){super();}

  ngOnInit(){
  }
}
