import { Component, EventEmitter, Injectable, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Meal } from '../../models/meal.model';
import { AuthenticationService } from '../../services/authentication.service';
import { MealsService } from '../../services/meals.service';
import { OrderCreationService } from '../../services/order-creation.service';

@Component({
  selector: 'app-restaurant-meal-item',
  templateUrl: './restaurant-meal-item.component.html'
})

@Injectable()
export class RestaurantsMealItemComponent implements OnInit {

  @Input() meal: Meal;
  @Output() onRemoveMeal: EventEmitter<any> = new EventEmitter();

  public image: any;

  public get ifEditingAllowed(): boolean {
    return this.authenticationService.isUserOwner;
  }

  public get ifAddingAndRemovingAllowed(): boolean {
    return this.authenticationService.isLoggedIn && !this.authenticationService.isUserOwner;
    
  }

  constructor(private route: ActivatedRoute,
    private router: Router,
    private mealsService: MealsService,
    private orderCreationService: OrderCreationService,
    private authenticationService: AuthenticationService) {
  }

  async ngOnInit(): Promise<void> {
    this.image = await this.mealsService.getImage(this.meal.entityId);
  }

  public addMeal() {
    this.orderCreationService.addMeal(this.meal);
  }

  public removeMeal() {
    this.orderCreationService.removeMeal(this.meal);
  }

  public editMeal() {
    this.router.navigate(['/restaurant-meal-update-creation/' + this.meal.restaurantId + '/' + this.meal.entityId]);
  }

  public async removeFromRestaurantMeal() {
    await this.mealsService.removeMeal(this.meal.entityId, this.authenticationService.getUserId);
    this.onRemoveMeal.emit();
  }

  formatImage(img: any): any {
    return 'data:image/jpeg;base64,' + img;
  }
  
}
