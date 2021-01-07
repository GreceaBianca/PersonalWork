import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HeroService } from '../services/hero.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Hero } from '../models/Hero';
import { Sidekick } from '../models/Sidekick';
import { SidekickService } from '../services/sidekick.service';

@Component({
  selector: 'app-heroes',
  templateUrl: './heroes.component.html',
  styleUrls: ['./heroes.component.css']
})
export class HeroesComponent implements OnInit {
  heros: Hero[] = [];
  sidekicks:Sidekick[]=[];
  modalRef: BsModalRef;
  editMode=false;
  heroClone: Hero=new Hero;

  constructor(private modalService: BsModalService, private heroService: HeroService, private sidekickService:SidekickService) { }

  ngOnInit() {
    this.initialiseHero();
    this.heroService.getHeros().subscribe(result => {
      this.heros = result;
    });
    this.sidekickService.getSidekicks().subscribe(result=>{
      this.sidekicks=result;
    })
  }

  openModal(template: TemplateRef<any>) {
    this.editMode=false;
    this.initialiseHero();
    this.modalRef = this.modalService.show(template);
  }

  openEditModal(template: TemplateRef<any>, hero:Hero) {
    this.editMode=true;
    this.heroClone.id=hero.id;
    this.heroClone.name=hero.name;
    this.heroClone.age=hero.age;
    this.heroClone.power=hero.power;
    this.heroClone.sidekick=hero.sidekick;
    this.heroClone.isEvil=hero.isEvil;
    this.modalRef = this.modalService.show(template);
  }

  initialiseHero() {
    this.heroClone.name = "";
    this.heroClone.age = 0;
    this.heroClone.id = 0;
    this.heroClone.power = 0;
    this.heroClone.isEvil=false;
    this.heroClone.sidekick=new Sidekick();
    this.heroClone.sidekick.name='';
  }

  save() {
    if(this.editMode){
      this.editMode=false;
      this.heroClone.sidekickId=this.heroClone.sidekick.id;
      this.heroService.editHero(this.heroClone).subscribe(result=>{
        this.initialiseHero();
        let index=this.heros.findIndex(s=>s.id==result.id);
        this.heros[index]=result;
        this.modalRef.hide();
      }, err=>{
        alert("Another hero has this name");
        this.modalRef.hide();
      });
    }
    else{
      this.heroClone.sidekickId=this.heroClone.sidekick.id;
      //this.heroClone.sidekick=null;
      let newHero=new Hero();
      newHero.name=this.heroClone.name;
      newHero.age=this.heroClone.age;
      newHero.power=this.heroClone.power;
      newHero.isEvil=this.heroClone.isEvil;
      newHero.id=0;
      newHero.sidekickId=this.heroClone.sidekick.id;
      this.heroService.addHero(newHero).subscribe(result=>{
        this.initialiseHero();
        let returnedHero=result;
        let i=this.sidekicks.findIndex(s=>s.id==result.sidekickId);
        returnedHero.sidekick=this.sidekicks[i];
        this.heros.push(result);
        this.modalRef.hide();
      }, err=>{
        alert("Another hero has this name");
        this.modalRef.hide();
      });
    }
  }

  delete(id:number){
    this.heroService.deleteHero(id).subscribe(result=>{
      let index=this.heros.findIndex(s=>s.id==id);
      this.heros.splice(index,1);
    })
  }
  close(){
    this.modalRef.hide();
  }
}
