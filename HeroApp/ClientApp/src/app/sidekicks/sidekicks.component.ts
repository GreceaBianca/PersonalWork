import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SidekickService } from '../services/sidekick.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Sidekick } from '../models/Sidekick';
@Component({
  selector: 'app-sidekicks',
  templateUrl: './sidekicks.component.html',
  styleUrls: ['./sidekicks.component.css']
})
export class SidekicksComponent implements OnInit {
  sidekicks: any[] = [];
  modalRef: BsModalRef;
  editMode=false;
  sidekickClone: Sidekick=new Sidekick;

  constructor(private modalService: BsModalService, private sidekickService: SidekickService) { }

  ngOnInit() {
    this.initialiseSidekick();
    this.sidekickService.getSidekicks().subscribe(result => {
      this.sidekicks = result;
    });
  }

  openModal(template: TemplateRef<any>) {
    this.editMode=false;
    this.initialiseSidekick();
    this.modalRef = this.modalService.show(template);
  }

  openEditModal(template: TemplateRef<any>, sidekick:Sidekick) {
    this.editMode=true;
    this.sidekickClone.id=sidekick.id;
    this.sidekickClone.name=sidekick.name;
    this.sidekickClone.age=sidekick.age;
    this.sidekickClone.power=sidekick.power;
    this.modalRef = this.modalService.show(template);
  }

  initialiseSidekick() {
    this.sidekickClone.name = "";
    this.sidekickClone.age = 0;
    this.sidekickClone.id = 0;
    this.sidekickClone.power = 0;
  }

  save() {
    if(this.editMode){
      this.editMode=false;
      this.sidekickService.editSidekick(this.sidekickClone).subscribe(result=>{
        this.initialiseSidekick();
        let index=this.sidekicks.findIndex(s=>s.id==result.id);
        this.sidekicks[index]=result;
        this.modalRef.hide();
      }, err=>{
        alert("Another sidekick has this name");
        this.modalRef.hide();
      });
    }
    else{
      this.sidekickService.addSidekick(this.sidekickClone).subscribe(result=>{
        this.initialiseSidekick();
        this.sidekicks.push(result);
        this.modalRef.hide();
      }, err=>{
        alert("Another sidekick has this name");
        this.modalRef.hide();
      });
    }
  }

  delete(id:number){
    this.sidekickService.deleteSidekick(id).subscribe(result=>{
      let index=this.sidekicks.findIndex(s=>s.id==id);
      this.sidekicks.splice(index,1);
    })
  }
  close(){
    this.modalRef.hide();
  }
}
