import { Component, Injectable, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MealForOrder } from '../../models/meal-for-order.model';
import { MealsService } from '../../services/meals.service';
import { OrderCreationService } from '../../services/order-creation.service';

@Component({
  selector: 'app-meal-for-order',
  templateUrl: './meal-for-order.component.html'
})

@Injectable()
export class OrderMealComponent implements OnInit {

  @Input() mealForOrder: MealForOrder;


  constructor(private route: ActivatedRoute,
    private router: Router,
    private mealsService: MealsService,
    private orderCreationService: OrderCreationService) {
  }

  async ngOnInit(): Promise<void> {
  }

  public addMeal() {
    this.mealForOrder.ammountOfMeals += 1;
  }
  public removeMeal() {
    this.mealForOrder.ammountOfMeals -= 1;
  }

}
