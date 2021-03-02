import { ComponentFixture, fakeAsync, TestBed, tick } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';
import { RestaurantsService } from '../services/restaurants.service';
import { RestaurantsListComponent } from './restaurants-list.component';

describe('RestaurantsListComponent', () => {
  let component: RestaurantsListComponent;
  let fixture: ComponentFixture<RestaurantsListComponent>;

  beforeEach(() => {
    const routerStub = () => ({ navigate: () => ({}) });
    const authenticationServiceStub = () => ({
      isLoggedIn: {},
      isUserOwner: {},
      getUser: { entityId: {} }
    });
    const restaurantsServiceStub = () => ({
      getAllElements: () => ({}),
      getAllRestaurantsForOwner: () => ({}),
      getAllRestaurantsForUser: () => ({})
    });
    TestBed.configureTestingModule({
      schemas: [NO_ERRORS_SCHEMA],
      declarations: [RestaurantsListComponent],
      providers: [
        { provide: Router, useFactory: routerStub },
        {
          provide: AuthenticationService,
          useFactory: authenticationServiceStub
        },
        { provide: RestaurantsService, useFactory: restaurantsServiceStub }
      ]
    });
    fixture = TestBed.createComponent(RestaurantsListComponent);
    component = fixture.componentInstance;
  });

  it('can load instance', () => {
    expect(component).toBeTruthy();
  });

  it(`restaurants has default value`, () => {
    expect(component.restaurants).toEqual([]);
  });

  it('add restaurant makes navigate call', () => {
    const routerStub: Router = fixture.debugElement.injector.get(Router);
    spyOn(routerStub, 'navigate').and.callThrough();
    component.addRestaurant();
    expect(routerStub.navigate).toHaveBeenCalledWith(['/restaurant-update-creation']);
  });


});
