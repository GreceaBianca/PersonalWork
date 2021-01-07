import { Component, OnInit, ViewChild } from '@angular/core';
import { LoadableComponentBase } from 'src/app/shared/utils/LoadableComponentBase';
import { PropertyService } from 'src/app/services/property.service';
import { SessionStorageService } from 'src/app/services/session-storage.service';
import { Property } from 'src/app/models/properties/Property';
import { clone } from 'src/app/shared/utils/Utils';
import { Router, ActivatedRoute } from '@angular/router';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { DialogService } from 'src/app/shared/services/dialog.service';

@Component({
  selector: 'app-user-properties',
  templateUrl: './user-properties.component.html',
  styleUrls: ['./user-properties.component.scss']
})
export class UserPropertiesComponent extends LoadableComponentBase implements OnInit {

  properties: Property[] = [];
  propertyEdited: Property = new Property();
  surfaceError:boolean=false;
  priceError:boolean=false;
  price:number=0;
  @ViewChild("editModal", null) modal: ModalDirective;

  constructor(private propertyService: PropertyService,
     private sessionStorageService: SessionStorageService, 
     private router: Router, private route: ActivatedRoute,
     private dialogService: DialogService) { super(); }

  ngOnInit() {
    const id = this.sessionStorageService.getUserID();
    this.startLoader();
    this.propertyService.getAllUserProperties(id).subscribe(result => {
      this.properties = result;
      this.properties.forEach(property=>{
        if(property.predictedPrice){
          property.predictedPrice=property.predictedPrice.split('.')[0]+"€";
        }
      })
      this.stopLoader();
    }, err => {
      this.stopLoader();
    })
  }
  updateError(){
    if(this.propertyEdited.usableSurface<=0){
      this.surfaceError=true;
    }
    else{
      this.surfaceError=false;
    }
    if(this.price<=0){
      this.priceError=true;
    }
    else{
      this.priceError=false;
    }
  }
 

  openModal(property: Property) {
    this.propertyEdited = clone(property);
    if(property.predictedPrice){
      this.propertyEdited.predictedPrice=this.propertyEdited.predictedPrice.split('€')[0];
      this.price=+this.propertyEdited.predictedPrice;
    }
    else{
      this.price=1;
    }
    
    this.modal.show()
  }
  edit() {
    if(this.propertyEdited.usableSurface<=0){
      this.surfaceError=true;
      return;
    }
    if(this.price<=0){
      this.priceError=true;
      return;
    }
    this.propertyEdited.predictedPrice=this.price.toString();
    this.propertyEdited.userId=this.sessionStorageService.getUserID();
    this.startLoader();
    this.propertyService.update(this.propertyEdited).subscribe(result => {
      if(result){
        let index = this.properties.findIndex(p => p.id == result.id);
        if(result.predictedPrice){
          result.predictedPrice=result.predictedPrice.split('.')[0]+"€";
        }
        this.properties[index] = result;
        this.stopLoader();
        this.modal.hide();
        this.dialogService.showSimpleDialog("Succes", "Datele au fost salvate cu succes", 'success');
      }
    }, err => {
      this.stopLoader();
      this.modal.hide();
      this.dialogService.showSimpleDialog("Eroare", "Am intampinat o eroare", 'error');
    })
  }

  deleteProperty(property: Property) {
    this.propertyService.delete(property.id).subscribe(result => {
      let index = this.properties.findIndex(p => p.id == property.id);
      this.properties.splice(index, 1);
    });
  }


}
