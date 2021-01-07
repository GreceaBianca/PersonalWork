import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PlanetService } from '../services/planet.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Planet } from '../models/Planet';
import { Sidekick } from '../models/Sidekick';
import { SidekickService } from '../services/sidekick.service';

@Component({
  selector: 'app-planets',
  templateUrl: './planets.component.html',
  styleUrls: ['./planets.component.css']
})
export class PlanetsComponent implements OnInit {

  planets: Planet[] = [];
  modalRef: BsModalRef;
  editMode=false;
  planetClone: Planet=new Planet;

  constructor(private modalService: BsModalService, private planetService: PlanetService, private sidekickService:SidekickService) { }

  ngOnInit() {
    //this.initialisePlanet();
    this.planetService.getPlanets().subscribe(result => {
      this.planets = result;
    });
  }

  // openModal(template: TemplateRef<any>) {
  //   this.editMode=false;
  //   this.initialisePlanet();
  //   this.modalRef = this.modalService.show(template);
  // }

  // openEditModal(template: TemplateRef<any>, planet:Planet) {
  //   this.editMode=true;
  //   this.planetClone.id=planet.id;
  //   this.planetClone.name=planet.name;
  //   this.planetClone.age=planet.age;
  //   this.planetClone.power=planet.power;
  //   this.planetClone.sidekick=planet.sidekick;
  //   this.planetClone.isEvil=planet.isEvil;
  //   this.modalRef = this.modalService.show(template);
  // }

  // initialisePlanet() {
  //   this.planetClone.name = "";
  //   this.planetClone.age = 0;
  //   this.planetClone.id = 0;
  //   this.planetClone.power = 0;
  //   this.planetClone.isEvil=false;
  //   this.planetClone.sidekick=new Sidekick();
  // }

  // save() {
  //   if(this.editMode){
  //     this.editMode=false;
  //     this.planetClone.sidekickId=this.planetClone.sidekick.id;
  //     this.planetService.editPlanet(this.planetClone).subscribe(result=>{
  //       this.initialisePlanet();
  //       let index=this.planets.findIndex(s=>s.id==result.id);
  //       this.planets[index]=result;
  //       this.modalRef.hide();
  //     }, err=>{
  //       alert("Another planet has this name");
  //       this.modalRef.hide();
  //     });
  //   }
  //   else{
  //     this.planetService.addPlanet(this.planetClone).subscribe(result=>{
  //       this.initialisePlanet();
  //       this.planets.push(result);
  //       this.modalRef.hide();
  //     }, err=>{
  //       alert("Another planet has this name");
  //       this.modalRef.hide();
  //     });
  //   }
  // }

  // delete(id:number){
  //   this.planetService.deletePlanet(id).subscribe(result=>{
  //     let index=this.planets.findIndex(s=>s.id==id);
  //     this.planets.splice(index,1);
  //   })
  // }
  // close(){
  //   this.modalRef.hide();
  // }

}
