import { ComponentFixture, fakeAsync, TestBed, tick } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { ActivatedRoute, convertToParamMap } from '@angular/router';
import { Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';
import { MealsService } from '../services/meals.service';
import { RestaurantsService } from '../services/restaurants.service';
import { RestaurantComponent } from './restaurant.component';
import { Restaurant } from '../models/restaurant.model';
import { of } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';

describe('RestaurantComponent', () => {
  let component: RestaurantComponent;
  let fixture: ComponentFixture<RestaurantComponent>;

  const mockActivatedRoute = {
    snapshot: {
      paramMap: {
        get() { return '1'; }
      }
    }
  };

  beforeEach(() => {
    const authenticationServiceStub = () => ({});
    const mealsServiceStub = () => ({
      getAllForRestaurant: restaurantId => ({})
    });
    const restaurantsServiceStub = () => ({
      getRestaurant: restaurantId => ({}),
      getImage: restaurantId => ({})
    });
    TestBed.configureTestingModule({
      schemas: [NO_ERRORS_SCHEMA],
      declarations: [RestaurantComponent],
      providers: [
        {
          provide: AuthenticationService,
          useFactory: authenticationServiceStub
        },
        { provide: MealsService, useFactory: mealsServiceStub },
        { provide: ActivatedRoute, useValue: {
            paramMap: of(convertToParamMap({ id: 1 }))
          }
        },
        { provide: RestaurantsService, useFactory: restaurantsServiceStub }
      ],
      imports:[RouterTestingModule]
    });
    fixture = TestBed.createComponent(RestaurantComponent);
    component = fixture.componentInstance;
  });

  it('can load instance', () => {
    expect(component).toBeTruthy();
  });


  it('add meal rotes to specified location', () => {
    let restaurant:Restaurant = <Restaurant>{};
    restaurant.entityId = 1;
    component['restaurant'] = restaurant;
    const router: Router = fixture.debugElement.injector.get(Router);
    spyOn(router, 'navigate').and.callThrough();
    component.addMeal();
    expect(router.navigate).toHaveBeenCalledWith(['/restaurant-meal-update-creation/' + restaurant.entityId]);
  });
});
