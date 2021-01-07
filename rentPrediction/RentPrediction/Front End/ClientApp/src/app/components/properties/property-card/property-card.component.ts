import { Component, OnInit, Input } from '@angular/core';
import { Property } from 'src/app/models/properties/Property';
import { Router } from '@angular/router';

@Component({
  selector: 'property-card',
  templateUrl: './property-card.component.html',
  styleUrls: ['./property-card.component.scss']
})
export class PropertyCardComponent implements OnInit {

  @Input() property:Property;
  imageSrc:string='';
  constructor(private router:Router) { }

  ngOnInit() {
    let imageModel = this.property.images[0];
              let index = imageModel.imageURL.split("/propertyImages");
              imageModel.imageURL = " ../../../assets/img/propertyImages" + index[1];
    this.imageSrc=imageModel .imageURL;
  }
  goToProperty(id:number){
    this.router.navigateByUrl('/property/'+this.property.id);
  }

}
