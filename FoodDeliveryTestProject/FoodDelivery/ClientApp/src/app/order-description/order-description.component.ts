import { DatePipe } from '@angular/common';
import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { OrderStatuses, OrderStatuses2LabelMapping } from '../enums/order-statuses.enum';
import { MealForOrder } from '../models/meal-for-order.model';
import { OrderStatus } from '../models/order-status.model';
import { Order } from '../models/order.model';
import { OrderStatusChangeEventArgs } from '../models/signalr-events-args/order-status-change-event-args.interface';
import { Account } from '../models/user.model';
import { AuthenticationService } from '../services/authentication.service';
import { OrdersService } from '../services/orders.service';
import { RestaurantsService } from '../services/restaurants.service';
import { SignalRService } from '../services/signalR.service';

@Component({
  selector: 'app-order-description',
  templateUrl: './order-description.component.html'
})

export class OrderDescriptionComponent implements OnInit, OnDestroy {


  public order: Order;
  public orderStatuses: OrderStatus[];
  public orderMeals: MealForOrder[];
  public orderStatuses2LabelMapping = OrderStatuses2LabelMapping;
  public restaurantName: string;

  private orderStatusChangeSubscription: Subscription;

  public get getAccount(): Account {
    return this.authenticationService.getUser;
  }

  public transformDate(date: Date) {
    let pipe = new DatePipe('en-US');
    return pipe.transform(date, 'short');
  }

  constructor(private route: ActivatedRoute,
    private authenticationService: AuthenticationService,
    private orderService: OrdersService,
    private restaurantService: RestaurantsService,
    signalrService: SignalRService) {


    this.orderStatusChangeSubscription = signalrService.onOrderStatusChangedSubject.subscribe(async (args: OrderStatusChangeEventArgs) => {
      if (this.order && this.order.entityId == args.orderId) {
        this.orderStatuses = await this.orderService.getOrderStatuses(this.order.entityId);
      }
    });
  }
  ngOnDestroy(): void {
    this.orderStatusChangeSubscription && this.orderStatusChangeSubscription.unsubscribe();
  }

  async ngOnInit(): Promise<void> {

    this.route.params.subscribe(async (params) => {
      if (params) {
        const orderId = params['id'];
        if (orderId) {
          this.order = await this.orderService.getOrder(orderId);
          this.orderStatuses = await this.orderService.getOrderStatuses(orderId);
          this.restaurantName = this.order.restaurantName;
          this.orderMeals = await this.orderService.getMealsForOrder(orderId);
          return;
        }
      }
    });

  }

}
