import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { LoadableComponentBase } from 'src/app/shared/utils/LoadableComponentBase';
import { FavoriteService } from 'src/app/services/favorite.service';
import { Favorite } from 'src/app/models/favorite/Favorite';
import { SessionStorageService } from 'src/app/services/session-storage.service';
import { PropertyService } from 'src/app/services/property.service';
import { Router, ActivatedRoute } from '@angular/router';
import { clone } from 'src/app/shared/utils/Utils';
import { Property } from 'src/app/models/properties/Property';

@Component({
  selector: 'app-user-favorite-properties',
  templateUrl: './user-favorite-properties.component.html',
  styleUrls: ['./user-favorite-properties.component.scss']
})
export class UserFavoritePropertiesComponent extends LoadableComponentBase implements OnInit {
  properties: Property[] = [];
  propertiesCloned: Property[] = [];
  propertiesReturnedArray: Property[] = [];
  itemsPerPage = 2;
  pagesDisplayed = 5;
  totalItems = 0;
  currentPage: number = 1;

  favorites: Favorite[] = [];

  constructor(private favoriteService: FavoriteService, private propertyService: PropertyService,
    private sessionStorageService: SessionStorageService, private router: Router, private route: ActivatedRoute) { super(); }

  ngOnInit() {
    this.startLoader();
    const id = this.sessionStorageService.getUserID();
    this.favoriteService.getFavoritesByUserId(id).subscribe(result => {
      this.favorites=result;
      this.favorites.forEach(favorite=>{
        this.properties.push(favorite.property);
      }
        )
      this.propertiesCloned = clone(this.properties);
      this.totalItems = this.propertiesCloned.length;
      this.propertiesReturnedArray = this.propertiesCloned.slice(0, this.itemsPerPage);
      const page = this.route.snapshot.queryParams.page;
      if (!page) {
        this.router.navigate(['/account/favorites'], { queryParams: { page: `1` } });
      }
      else {
        if (page !== '1') {
          this.currentPage = parseInt(page);
        }
      }
      this.stopLoader();
    }, err => {
      this.stopLoader();
    })
  }
  pageChanged(event: any): void {
    const startItem = (event.page - 1) * event.itemsPerPage;
    const endItem = event.page * event.itemsPerPage;
    this.router.navigate(['/account/favorites'], { queryParams: { page: `${event.page}` } });
    this.propertiesReturnedArray = this.propertiesCloned.slice(startItem, endItem);
  }
}