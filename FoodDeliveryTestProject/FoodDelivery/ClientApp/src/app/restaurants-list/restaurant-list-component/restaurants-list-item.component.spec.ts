import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../services/authentication.service';
import { RestaurantsService } from '../../services/restaurants.service';
import { RestaurantsListItemComponent } from './restaurants-list-item.component';

describe('RestaurantsListItemComponent', () => {
  let component: RestaurantsListItemComponent;
  let fixture: ComponentFixture<RestaurantsListItemComponent>;

  beforeEach(() => {
    const activatedRouteStub = () => ({});
    const routerStub = () => ({ navigate: array => ({}) });
    const authenticationServiceStub = () => ({});
    const restaurantsServiceStub = () => ({
      getImage: restaurantId => ({}),
      removeRestaurant: restaurantId => ({})
    });
    TestBed.configureTestingModule({
      schemas: [NO_ERRORS_SCHEMA],
      declarations: [RestaurantsListItemComponent],
      providers: [
        { provide: ActivatedRoute, useFactory: activatedRouteStub },
        { provide: Router, useFactory: routerStub },
        {
          provide: AuthenticationService,
          useFactory: authenticationServiceStub
        },
        { provide: RestaurantsService, useFactory: restaurantsServiceStub }
      ]
    });
    fixture = TestBed.createComponent(RestaurantsListItemComponent);
    component = fixture.componentInstance;
  });

  it('can load instance', () => {
    expect(component).toBeTruthy();
  });

  it('show restaurant moves to specified rote', () => {
    const restaurantId = 1;
    const routerStub: Router = fixture.debugElement.injector.get(Router);
    component['restaurantId'] = restaurantId;
    spyOn(routerStub, 'navigate').and.callThrough();
    component.showRestaurant();
    expect(routerStub.navigate).toHaveBeenCalledWith(['/restaurant/'+ restaurantId]);
  });

  it('edit restaurant moves to specified rote', () => {
    const restaurantId = 1;
    const routerStub: Router = fixture.debugElement.injector.get(Router);
    component['restaurantId'] = restaurantId;
    spyOn(routerStub, 'navigate').and.callThrough();
    component.editRestaurant();
    expect(routerStub.navigate).toHaveBeenCalledWith(['/restaurant-update-creation/' + restaurantId]);
  });
});
