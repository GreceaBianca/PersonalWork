import { Component, OnInit } from '@angular/core';
import { Property } from 'src/app/models/properties/Property';
import { PropertyService } from 'src/app/services/property.service';
import { LoadableComponentBase } from 'src/app/shared/utils/LoadableComponentBase';
import { ActivatedRoute } from '@angular/router';
import { DialogService } from 'src/app/shared/services/dialog.service';
import { Gallery } from 'src/app/models/galery/Gallery';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { UserService } from 'src/app/services/user.service';
import { SessionStorageService } from 'src/app/services/session-storage.service';
import { forkJoin } from 'rxjs';
import { User } from 'src/app/models/users/User';
import { FavoriteService } from 'src/app/services/favorite.service';
import { Favorite } from 'src/app/models/favorite/Favorite';
import { reduce } from 'rxjs/operators';
@Component({
  selector: 'app-property',
  templateUrl: './property.component.html',
  styleUrls: ['./property.component.scss']
})
export class PropertyComponent extends LoadableComponentBase implements OnInit {
  featuresExpanded = false;
  addressExpanded = false;
  isFavorite = false;
  user: User;
  predictedPrice:string;
  sourceExpanded = false;
  property: Property = new Property;
  srcData: SafeResourceUrl[] = [];
  constructor(private sanitizer: DomSanitizer,
    private favoriteService: FavoriteService,
    private propertyService: PropertyService,
    private route: ActivatedRoute,
    private dialogService: DialogService,
    private sessionStorage: SessionStorageService) { super(); }

  ngOnInit() {
    this.startLoader();
    let userId = this.sessionStorage.getUserID();
    let propertyId = this.route.snapshot.params.id;
    forkJoin(
      this.propertyService.getById(propertyId),
       this.favoriteService.getFavoritesByUserId(userId)
    )
      .subscribe(result => {
        if (result[0]) {
          this.property = result[0] as Property;
          if (this.property.images.length > 0) {
            this.property.images.forEach(image => {
              let imageModel = image;
              let index = imageModel.imageURL.split("/propertyImages");
              imageModel.imageURL = " ../../../assets/img/propertyImages" + index[1];
              image = imageModel;
              this.srcData.push(this.sanitizer.bypassSecurityTrustResourceUrl(imageModel.imageURL));
            })
          }
          else {
            this.property.images[0] = new Gallery();
            this.property.images[0].imageURL = '../../../../..assets/img/noimage.png';
            this.srcData.push(this.sanitizer.bypassSecurityTrustResourceUrl(this.property.images[0].imageURL));
          }
          this.predictedPrice=this.property.predictedPrice.split('.')[0];
        }
        let favorites=result[1] as Favorite[];
        favorites.forEach(favorite=>{
          if(favorite.propertyId==propertyId){
            this.isFavorite=true;
          }
        });
        if(this.property.address.streetName.includes("Cartier:")){
          this.property.address.streetName=this.property.address.streetName.split("Cartier:")[1];
        }

        this.stopLoader();
      }, err => {
        this.dialogService.showSimpleDialog("Eroare", "A intervenit o eroare", 'error');
        this.stopLoader();
      })
  }
  markAsFavorite(){
    let favorite=new Favorite();
    
    let userId = this.sessionStorage.getUserID();
    let propertyId = this.route.snapshot.params.id;
    favorite.userId=userId;
    favorite.propertyId=propertyId;
    this.favoriteService.addFavorite(favorite).subscribe(result=>{
      if(result){
        this.isFavorite=true;
      }
    })
  }

}
