import { Injectable } from "@angular/core";
import { Meal } from "../models/meal.model";
import { Order } from "../models/order.model";
import { OrderStatus } from "../models/order-status.model";
import { OrderStatuses } from "../enums/order-statuses.enum";
import { DatePipe, formatDate } from "@angular/common";
import { AuthenticationService } from "./authentication.service";
import { AlertService } from "../alert-system/alert.service";
import { Alert } from "../alert-system/alert.model";
import { MealForOrder } from "../models/meal-for-order.model";
import { OrdersService } from "./orders.service";
import { Router } from "@angular/router";
import { RestaurantsService } from "./restaurants.service";

@Injectable()
export class OrderCreationService {

  private currentOrder: Order;

  public get getCurrentOrder(): Order {
    return this.currentOrder;
  }

  options = {
    autoClose: true,
    keepAfterRouteChange: false
  };


  constructor(protected authenticationService: AuthenticationService,
    protected alertService: AlertService,
    protected ordersService: OrdersService,
    private router: Router,
    private restaurantsService: RestaurantsService) { }

  public async sendOrder() {
    if (!this.currentOrder || this.currentOrder.mealsForOrder.length == 0) {
      this.alertService.error('No meals in order', this.options);
    }
    this.cleanOrder();
    await this.ordersService.addOrder(this.currentOrder);
    this.currentOrder = <Order>{};
    this.router.navigate([""]);
  }

  cleanOrder() {
    this.currentOrder.mealsForOrder = this.currentOrder.mealsForOrder.map(mfo => {
      if (mfo.ammountOfMeals > 0) {
        return mfo;
      }
    });
  }


  public addMeal(meal: Meal): void {

    if (!this.authenticationService.isLoggedIn) {
      this.alertService.error('Need to log in before making order', this.options);
      return;
    }


    if (this.currentOrder
      && this.currentOrder.restaurantId == meal.restaurantId) {

      this.addMealToOrder(meal);
      return;
    }

    this.alertService.warn('New order created', this.options);
    this.createNewOrder();
    this.addFirstMeal(meal);
  }

  public removeMeal(meal: Meal) {
    let mealForOrderIndex = this.currentOrder.mealsForOrder.findIndex(mfo => mfo.mealId == meal.entityId);
    if (mealForOrderIndex == -1) {
      return;
    }
    this.currentOrder.price -= meal.price;
    this.currentOrder.mealsForOrder[mealForOrderIndex].ammountOfMeals -= 1;

    if (this.currentOrder.mealsForOrder[mealForOrderIndex].ammountOfMeals == 0) {
      this.currentOrder.mealsForOrder.splice(mealForOrderIndex, 1);
    }
  }

  private addMealToOrder(meal: Meal) {
    this.currentOrder.price += meal.price;
    var mealForOrder = this.currentOrder.mealsForOrder.find(mfo => mfo.mealId == meal.entityId);
    if (mealForOrder) {
      mealForOrder.ammountOfMeals += 1;
      return;
    }
    this.addNewMealToOrder(meal);
  }

  private addNewMealToOrder(meal: Meal) {
    let newMealForOrder = <MealForOrder>{};
    newMealForOrder.ammountOfMeals = 1;
    newMealForOrder.mealName = meal.name;
    newMealForOrder.mealPrice = meal.price;
    newMealForOrder.mealId = meal.entityId;
    this.currentOrder.mealsForOrder.push(newMealForOrder);
  }

  private createNewOrder() {
    this.currentOrder = <Order>{};
    this.currentOrder.mealsForOrder = [];
    this.currentOrder.orderStatuses = [];
    this.addStatus(OrderStatuses.Created);
    this.currentOrder.customerId = this.authenticationService.getUserId;
  }

  private async addFirstMeal(meal: Meal) {
    this.addNewMealToOrder(meal);
    this.currentOrder.price = meal.price;
    this.currentOrder.restaurantId = meal.restaurantId;
    this.currentOrder.restaurantName = await this.restaurantsService.getRestaurantName(meal.restaurantId);
  }

  private addStatus(status: OrderStatuses) {
    let newStatus = <OrderStatus>{};
    newStatus.statusChangeTime = new Date();
    newStatus.orderId = this.currentOrder.entityId;
    newStatus.status = status
    this.currentOrder.orderStatuses.push(newStatus);
  }
}
