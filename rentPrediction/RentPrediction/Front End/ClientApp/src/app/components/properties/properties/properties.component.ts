import { Component, OnInit, EventEmitter, ViewEncapsulation, ViewChild } from '@angular/core';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { Property } from 'src/app/models/properties/Property';
import { LoadableComponentBase } from 'src/app/shared/utils/LoadableComponentBase';
import { PropertyService } from 'src/app/services/property.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Feature } from 'src/app/models/feature/Feature';
import { clone } from 'src/app/shared/utils/Utils';
import { PropertyFilter } from 'src/app/models/properties/PropertyFilter';
import { BsDropdownDirective } from 'ngx-bootstrap/dropdown';


@Component({
  selector: 'app-properties',
  templateUrl: './properties.component.html',
  styleUrls: ['./properties.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class PropertiesComponent extends LoadableComponentBase implements OnInit {

  properties:Property[]=[];
  propertiesCloned:Property[]=[];
  propertiesReturnedArray:Property[]=[];
  propertyFilter:PropertyFilter=new PropertyFilter();

  maxDate:Date=new Date();
  myDateValue: Date=new Date();

  minPriceValue:number=0;
  maxPriceValue:number=1000;

  itemsPerPage=9;
  pagesDisplayed=5;
  totalItems=0;
  currentPage:number=1;

  showReset=false;

  propertySearchString:string='';

  @ViewChild("dropdown", null) dropdown: BsDropdownDirective;

  constructor(private propertyService:PropertyService, private router:Router, private route:ActivatedRoute) { super(); }
       
  ngOnInit() {
    this.startLoader();
    this.propertyService.getAll().subscribe(result=>{
      this.properties=result;
      this.propertiesCloned=clone(this.properties);
      this.totalItems=this.propertiesCloned.length;
      this.propertiesReturnedArray=this.propertiesCloned.slice(0,this.itemsPerPage);
      const page=this.route.snapshot.queryParams.page;
      if(!page){
        this.router.navigate(['/properties'], { queryParams: { page: `1` } });
      }
      else{
        if(page!=='1'){
          this.currentPage=parseInt(page);
        }
      }
      this.stopLoader();
    }, err=>{
      this.stopLoader();
    })
  }
  pageChanged(event: any): void {
      const startItem = (event.page - 1) * event.itemsPerPage;
      const endItem = event.page * event.itemsPerPage;
      this.router.navigate(['/properties'], { queryParams: { page: `${event.page}` } });
      this.propertiesReturnedArray = this.propertiesCloned.slice(startItem, endItem);
  }

  goToProperty(id:number){
    this.router.navigateByUrl('/property/'+id);
  }

  onDateChange(newDate: Date) {
    this.propertyFilter.creationDate=newDate;
    this.applyFilters();
  }

  applyFilters() {
    this.showReset=true;
    this.propertiesCloned = this.properties.filter(item => {
      if (
        this.propertyFilter.feature.hasBalcony != null &&
        item.feature.hasBalcony !=  this.propertyFilter.feature.hasBalcony
      ) {
        return false;
      }
      if (
        this.propertyFilter.usableSurface != null &&
        item.usableSurface !=  this.propertyFilter.usableSurface
      ) {
        return false;
      }
      if (
        this.propertyFilter.creationDate != null &&
        new Date(item.creationDate) < this.propertyFilter.creationDate
      ) {
        return false;
      }
      if (
        this.propertyFilter.feature.hasGarden != null &&
        item.feature.hasGarden !=  this.propertyFilter.feature.hasGarden
      ) {
        return false;
      }
      if (
        this.propertyFilter.feature.hasHeating != null &&
        item.feature.hasHeating !=  this.propertyFilter.feature.hasHeating
      ) {
        return false;
      }
      if (
        this.propertyFilter.feature.hasParking != null &&
        item.feature.hasParking !=  this.propertyFilter.feature.hasParking
      ) {
        return false;
      }
      if (
        this.propertyFilter.feature.hasBalcony != null &&
        item.feature.hasBalcony !=  this.propertyFilter.feature.hasBalcony
      ) {
        return false;
      }
      if (
        this.propertyFilter.feature.numberOfBaths != null &&
        item.feature.numberOfBaths !=  this.propertyFilter.feature.numberOfBaths
      ) {
        return false;
      }
      if (
        this.propertyFilter.feature.numberOfParkingSpots != null &&
        item.feature.numberOfParkingSpots !=  this.propertyFilter.feature.numberOfParkingSpots
      ) {
        return false;
      }
      if (
        this.propertyFilter.feature.numberOfRooms != null &&
        item.feature.numberOfRooms !=  this.propertyFilter.feature.numberOfRooms
      ) {
        return false;
      }
      if (
        this.propertyFilter.feature.numberOfRooms != null &&
        item.feature.numberOfRooms !=  this.propertyFilter.feature.numberOfRooms
      ) {
        return false;
      }
      if (
        this.propertyFilter.price != null &&
        item.price >  this.propertyFilter.price
      ) {
        return false;
      }
      return true;
    });
    this.totalItems=this.propertiesCloned.length;
    this.propertiesReturnedArray=this.propertiesCloned.slice(0,this.itemsPerPage);
    this.currentPage=1;
    this.dropdown.hide();
  }

  resetFilters() {
    this.propertyFilter = new PropertyFilter();
    this.propertiesCloned = clone(this.properties);
    this.totalItems=this.propertiesCloned.length;
    this.propertiesReturnedArray=this.propertiesCloned.slice(0,this.itemsPerPage);
    this.currentPage=1;
    this.showReset=false;
    this.dropdown.hide();
  }

  filterName(){
   this.propertiesCloned=clone(this.properties);
    this.propertiesCloned = this.propertiesCloned.filter(item => {
      if (
        item.name.search(this.propertySearchString)>-1
      ) {
        return true;
      }
    });
    this.totalItems=this.propertiesCloned.length;
    this.propertiesReturnedArray=this.propertiesCloned.slice(0,this.itemsPerPage);
    this.currentPage=1;
  }
}
