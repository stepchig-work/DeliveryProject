import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountRoles } from '../enums/account-role.enum';
import { Restaurant } from '../models/restaurant.model';
import { AuthenticationService } from '../services/authentication.service';
import { RestaurantsService } from '../services/restaurants.service';

@Component({
  selector: 'app-restaurants-list',
  templateUrl: './restaurants-list.component.html'
})
export class RestaurantsListComponent implements OnInit {

  public restaurants: Restaurant[] = [];

  public get ifAddingRestaurantAllowed(): boolean {
    return this.authenticationService.isUserOwner;
  }

  constructor(private authenticationService: AuthenticationService,
    private restaurantService: RestaurantsService,
    private router: Router) {
  }

  async ngOnInit(): Promise<void> {
    await this.getRestaurants();
  }

  public onRestaurantRemoved() {
    this.getRestaurants();
  }


  public addRestaurant() {
    this.router.navigate(['/restaurant-update-creation']);
  }

  private async getRestaurants() {

    if (!this.authenticationService.isLoggedIn) {
      this.restaurants = await this.restaurantService.getAllElements();
      return;
    }
    if (this.authenticationService.isUserOwner) {
      this.restaurants = await this.restaurantService.getAllRestaurantsForOwner(this.authenticationService.getUserId);
      return;
    }
    this.restaurants = await this.restaurantService.getAllRestaurantsForUser(this.authenticationService.getUserId);
  }
}
