import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit {

  phoneNo:string="0744850728";
  email:string="grecea.bianca@gmail.com";

  @ViewChild("contactModal", null) modal: ModalDirective;
  constructor(private router:Router) { }

  ngOnInit() {
  }
  goToQuestions(){
    this.router.navigate(["/questions"]);
  }

}
