import { AfterViewChecked, AfterViewInit, Component, EventEmitter, Injectable, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountRoles } from '../../enums/account-role.enum';
import { AuthenticationService } from '../../services/authentication.service';
import { RestaurantsService } from '../../services/restaurants.service';

@Component({
  selector: 'app-restaurants-list-item',
  templateUrl: './restaurants-list-item.component.html'
})

@Injectable()
export class RestaurantsListItemComponent implements OnInit  {

  @Input() restaurantName: string;
  @Input() restaurantDescription: string;
  @Input() restaurantId: number;
  @Output() onRemoveRestaurant: EventEmitter<any> = new EventEmitter();

  public image: any;

  public get ifEditingRestaurantAllowed(): boolean {
    return this.authenticationService.isUserOwner;
  }
  constructor(private authenticationService: AuthenticationService,
    private route: ActivatedRoute,
    private router: Router,
    private restaurantService: RestaurantsService) {
  }

  async ngOnInit(): Promise<void> {
    this.image = await this.restaurantService.getImage(this.restaurantId);
  }

  public showRestaurant() {
    this.router.navigate(['/restaurant/'+ this.restaurantId ]);
  }

  public editRestaurant() {
    this.router.navigate(["/restaurant-update-creation/" + this.restaurantId]);
  }

  public async removeRestaurant(event: any) {
    event.stopPropagation();
    await this.restaurantService.removeRestaurant(this.restaurantId, this.authenticationService.getUserId);
    this.onRemoveRestaurant.emit();
  }

  formatImage(img: any): any {
    return 'data:image/jpeg;base64,' + img;
  }
  
  
}
