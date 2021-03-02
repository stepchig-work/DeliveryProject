import { Component, Injectable, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountRoles } from '../enums/account-role.enum';
import { Meal } from '../models/meal.model';
import { Restaurant } from '../models/restaurant.model';
import { AuthenticationService } from '../services/authentication.service';
import { MealsService } from '../services/meals.service';
import { RestaurantsService } from '../services/restaurants.service';

@Component({
  selector: 'app-restaurant',
  templateUrl: './restaurant.component.html'
})


export class RestaurantComponent implements OnInit {

  public restaurant: Restaurant;
  public meals: Meal[];
  public image;

  public get ifAddingAllowed(): boolean {
    return this.authenticationService.isUserOwner;
  }

  constructor(private authenticationService: AuthenticationService,
    private route: ActivatedRoute,
    private restaurantService: RestaurantsService,
    private mealsService: MealsService,
    private router: Router) {
  }

  public ngOnInit(): void {
    this.route.params.subscribe(async (params) => {
      if (params) {
        const restaurantId = params['id'];
        if (restaurantId) {
          this.restaurant = await this.restaurantService.getRestaurant(restaurantId, this.authenticationService.getUserId);
          this.image = await this.restaurantService.getImage(restaurantId);
          this.meals = await this.mealsService.getAllForRestaurant(restaurantId);
          return;
        }
      }
    });
  }

  public addMeal() {
    this.router.navigate(['/restaurant-meal-update-creation/' + this.restaurant.entityId]);
  }

  public async onMealRemoved() {
    this.meals = await this.mealsService.getAllForRestaurant(this.restaurant.entityId);
  }

  formatImage(img: any): any {
    return 'data:image/jpeg;base64,' + img;
  }


}
