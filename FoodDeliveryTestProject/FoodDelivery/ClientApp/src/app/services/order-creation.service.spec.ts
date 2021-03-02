import { TestBed } from '@angular/core/testing';
import { Meal } from '../models/meal.model';
import { AuthenticationService } from './authentication.service';
import { AlertService } from '../alert-system/alert.service';
import { OrdersService } from './orders.service';
import { Router } from '@angular/router';
import { OrderCreationService } from './order-creation.service';
import { RestaurantsService } from './restaurants.service';

describe('OrderCreationService', () => {
  let service: OrderCreationService;

  beforeEach(() => {
    const authenticationServiceStub = () => ({
      isLoggedIn: {},
      getUser: { entityId: {} }
    });
    const alertServiceStub = () => ({
      error: (string, options) => ({}),
      warn: (string, options) => ({})
    });
    const ordersServiceStub = () => ({ addOrder: currentOrder => ({}) });
    const routerStub = () => ({ navigate: array => ({}) });
    const restaurantsServiceStub = () => ({
      getRestaurantName: restaurantId => ({})
    });
    TestBed.configureTestingModule({
      providers: [
        OrderCreationService,
        {
          provide: AuthenticationService,
          useFactory: authenticationServiceStub
        },
        { provide: AlertService, useFactory: alertServiceStub },
        { provide: OrdersService, useFactory: ordersServiceStub },
        { provide: Router, useFactory: routerStub },
        { provide: RestaurantsService, useFactory: restaurantsServiceStub }
      ]
    });
    service = TestBed.inject(OrderCreationService);
  });

  it('can load instance', () => {
    expect(service).toBeTruthy();
  });

    it('adds new meal', () => {
      const meal: Meal = <Meal>{};
      meal.entityId = 1;
      meal.restaurantId = 1;
      meal.price = 4;

      service.addMeal(meal);
      expect(service.getCurrentOrder.price).toEqual(meal.price);
      expect(service.getCurrentOrder.restaurantId).toEqual(meal.restaurantId);
    });

    it('removes meal from order', () => {
      const meal: Meal = <Meal>{};
      meal.entityId = 1;
      meal.restaurantId = 1;
      meal.price = 4;

      service.addMeal(meal);
      service.removeMeal(meal);

      expect(service.getCurrentOrder.price).toEqual(0);
      expect(service.getCurrentOrder.restaurantId).toEqual(meal.restaurantId);
    });

    it('add 2 meal from different restaurants', () => {
      const meal: Meal = <Meal>{};
      meal.entityId = 1;
      meal.restaurantId = 1;
      meal.price = 4;

      service.addMeal(meal);

      const meal2: Meal = <Meal>{};
      meal2.entityId = 2;
      meal2.restaurantId = 2;
      meal2.price = 4;

      service.addMeal(meal2);

      expect(service.getCurrentOrder.price).toEqual(meal2.price);
      expect(service.getCurrentOrder.restaurantId).toEqual(meal2.restaurantId);
    });

    it('add 2 meal from one restaurant', () => {
      const meal: Meal = <Meal>{};
      meal.entityId = 1;
      meal.restaurantId = 1;
      meal.price = 4;

      service.addMeal(meal);

      const meal2: Meal = <Meal>{};
      meal2.entityId = 2;
      meal2.restaurantId = 1;
      meal2.price = 4;

      service.addMeal(meal2);

      expect(service.getCurrentOrder.price).toEqual(meal2.price + meal.price);
      expect(service.getCurrentOrder.restaurantId).toEqual(meal2.restaurantId);
    });

});
