import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';
import { OrdersService } from '../services/orders.service';
import { RestaurantsService } from '../services/restaurants.service';
import { SignalRService } from '../services/signalR.service';
import { OrderStatuses2LabelMapping } from '../enums/order-statuses.enum';
import { OrderDescriptionComponent } from './order-description.component';

describe('OrderDescriptionComponent', () => {
  let component: OrderDescriptionComponent;
  let fixture: ComponentFixture<OrderDescriptionComponent>;

  beforeEach(() => {
    const activatedRouteStub = () => ({ params: { subscribe: f => f({}) } });
    const authenticationServiceStub = () => ({});
    const ordersServiceStub = () => ({
      getOrderStatuses: entityId => ({}),
      getOrder: orderId => ({}),
      getMealsForOrder: orderId => ({})
    });
    const restaurantsServiceStub = () => ({
      getRestaurantName: restaurantId => ({})
    });
    const signalRServiceStub = () => ({
      onOrderStatusChangedSubject: { subscribe: f => f({}) }
    });
    TestBed.configureTestingModule({
      schemas: [NO_ERRORS_SCHEMA],
      declarations: [OrderDescriptionComponent],
      providers: [
        { provide: ActivatedRoute, useFactory: activatedRouteStub },
        {
          provide: AuthenticationService,
          useFactory: authenticationServiceStub
        },
        { provide: OrdersService, useFactory: ordersServiceStub },
        { provide: RestaurantsService, useFactory: restaurantsServiceStub },
        { provide: SignalRService, useFactory: signalRServiceStub }
      ]
    });
    fixture = TestBed.createComponent(OrderDescriptionComponent);
    component = fixture.componentInstance;
  });

  it('can load instance', () => {
    expect(component).toBeTruthy();
  });

  it(`orderStatuses2LabelMapping has default value`, () => {
    expect(component.orderStatuses2LabelMapping).toEqual(
      OrderStatuses2LabelMapping
    );
  });

});
