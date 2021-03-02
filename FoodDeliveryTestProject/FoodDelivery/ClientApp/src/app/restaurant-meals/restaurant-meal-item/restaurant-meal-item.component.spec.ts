import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../services/authentication.service';
import { MealsService } from '../../services/meals.service';
import { OrderCreationService } from '../../services/order-creation.service';
import { RestaurantsMealItemComponent } from './restaurant-meal-item.component';
import { Meal } from 'src/app/models/meal.model';
import { mergeAll } from 'rxjs/operators';

describe('RestaurantsMealItemComponent', () => {
  let component: RestaurantsMealItemComponent;
  let fixture: ComponentFixture<RestaurantsMealItemComponent>;

  beforeEach(() => {
    const activatedRouteStub = () => ({});
    const routerStub = () => ({ navigate: array => ({}) });
    const authenticationServiceStub = () => ({});
    const mealsServiceStub = () => ({
      getImage: entityId => ({}),
      removeMeal: entityId => ({})
    });
    const orderCreationServiceStub = () => ({
      addMeal: meal => ({}),
      removeMeal: meal => ({})
    });
    TestBed.configureTestingModule({
      schemas: [NO_ERRORS_SCHEMA],
      declarations: [RestaurantsMealItemComponent],
      providers: [
        { provide: ActivatedRoute, useFactory: activatedRouteStub },
        { provide: Router, useFactory: routerStub },
        {
          provide: AuthenticationService,
          useFactory: authenticationServiceStub
        },
        { provide: MealsService, useFactory: mealsServiceStub },
        { provide: OrderCreationService, useFactory: orderCreationServiceStub }
      ]
    });
    fixture = TestBed.createComponent(RestaurantsMealItemComponent);
    component = fixture.componentInstance;
  });

  it('can load instance', () => {
    expect(component).toBeTruthy();
  });

    it('add meals sends to specified location', () => {
      const orderCreationServiceStub: OrderCreationService = fixture.debugElement.injector.get(
        OrderCreationService
      );
      spyOn(orderCreationServiceStub, 'addMeal').and.callThrough();
      component.addMeal();
      expect(orderCreationServiceStub.addMeal).toHaveBeenCalled();
    });

    it('remove meal called for order creation service', () => {
      const orderCreationServiceStub: OrderCreationService = fixture.debugElement.injector.get(
        OrderCreationService
      );
      spyOn(orderCreationServiceStub, 'removeMeal').and.callThrough();
      component.removeMeal();
      expect(orderCreationServiceStub.removeMeal).toHaveBeenCalled();
    });

    it('navigates to edit meal location', () => {
      let meal: Meal = <Meal>{};
      meal.entityId = 1;
      meal.restaurantId = 1;
      component['meal'] = meal;
      const routerStub: Router = fixture.debugElement.injector.get(Router);
      spyOn(routerStub, 'navigate').and.callThrough();
      component.editMeal();
      expect(routerStub.navigate).toHaveBeenCalledWith(['/restaurant-meal-update-creation/'+ meal.restaurantId + '/' + meal.entityId]);
    });

    it('remove meal calls remove meal for meal service', () => {
      let meal: Meal = <Meal>{};
      meal.entityId = 1;
      component['meal'] = meal;
      const mealsServiceStub: MealsService = fixture.debugElement.injector.get(
        MealsService
      );
      spyOn(mealsServiceStub, 'removeMeal').and.callThrough();
      component.removeFromRestaurantMeal();
      expect(mealsServiceStub.removeMeal).toHaveBeenCalled();
    });
});
