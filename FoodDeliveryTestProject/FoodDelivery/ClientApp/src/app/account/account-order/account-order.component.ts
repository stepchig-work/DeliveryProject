import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Injectable, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AccountRoles } from '../../enums/account-role.enum';
import { OrderStatuses, OrderStatuses2LabelMapping } from '../../enums/order-statuses.enum';
import { Order } from '../../models/order.model';
import { OrderStatusChangeEventArgs } from '../../models/signalr-events-args/order-status-change-event-args.interface';
import { Account } from '../../models/user.model';
import { AccountService } from '../../services/account-service';
import { AuthenticationService } from '../../services/authentication.service';
import { OrdersService } from '../../services/orders.service';
import { RestaurantsService } from '../../services/restaurants.service';
import { SignalRService } from '../../services/signalR.service';

@Component({
  selector: 'app-account-order',
  templateUrl: './account-order.component.html'
})
@Injectable()
export class AccountOrderComponent implements OnInit, OnDestroy {

  @Input() order: Order;
  @Output() onUserBanned: EventEmitter<any> = new EventEmitter();

  public orderStatus: OrderStatuses;
  public restaurantName: string;
  public userName: string;

  private orderStatusChangeSubscription: Subscription;

  public get getAccount(): Account {
    return this.authenticationService.getUser;
  }

  public get isActionAllowed(): boolean {
    if (this.getAccount.role == AccountRoles.RegularUser) {
      return this.orderStatus == OrderStatuses.Placed
        || this.orderStatus == OrderStatuses.Delivered;
    }
    else {
      return this.orderStatus == OrderStatuses.Placed
        || this.orderStatus == OrderStatuses.Processed
        || this.orderStatus == OrderStatuses.InRoute
    }
  }
  public transformDate(date: Date) {
    let pipe = new DatePipe('en-US');
    return pipe.transform(date, 'short');
  }

  public get getCurrentBtnName(): string {
    switch (this.orderStatus) {
      case OrderStatuses.Placed:
        return this.getAccount.role == AccountRoles.RegularUser ? "Cancel" : "Start Order";
      case OrderStatuses.Processed:
        return "Send to customer";
      case OrderStatuses.InRoute:
        return "Courer delivered";
      case OrderStatuses.Delivered:
        return "Confirm delivery";
    }
  }

  public orderStatuses2LabelMapping = OrderStatuses2LabelMapping;

  constructor(protected orderService: OrdersService,
    protected authenticationService: AuthenticationService,
    protected restaurantsService: RestaurantsService,
    protected signalrService: SignalRService,
    private accountSetvice: AccountService,
    private router: Router,) {
    this.orderStatusChangeSubscription = signalrService.onOrderStatusChangedSubject.subscribe(async (args: OrderStatusChangeEventArgs) => {
      if (this.order.entityId == args.orderId) {
        this.orderStatus = args.orderStatus;
      }
    });
    
  }

  async ngOnInit(): Promise<void> {
    this.setOrderStatus();
    this.restaurantName = this.order.restaurantName;
    this.userName = await this.accountSetvice.getUserName(this.order.customerId);
  }

  ngOnDestroy(): void {
    this.orderStatusChangeSubscription && this.orderStatusChangeSubscription.unsubscribe();
  }

  public showOrder() {
    this.router.navigate(['/order-description/' + this.order.entityId]);
  }

  public changeStatus() {
    let newStatus: OrderStatuses;
    if (this.orderStatus == OrderStatuses.Placed) {
      newStatus = this.getAccount.role == AccountRoles.RegularUser ? OrderStatuses.Canceled : OrderStatuses.Processed;
    }
    else {
      newStatus = this.orderStatus + 1;
    }
    this.orderService.changeStatus(newStatus, this.order.entityId, this.authenticationService.getUserId);
  }

  public async banUser() {
    await this.accountSetvice.banUser(this.authenticationService.getUserId, this.userName);
    this.onUserBanned.next();
  }
  

  public async setOrderStatus() {

    let orderStatuses = await this.orderService.getOrderStatuses(this.order.entityId);
    if (orderStatuses.some(o => o.status == OrderStatuses.Received)) {
      this.orderStatus = OrderStatuses.Received;
      return;
    }
    if (orderStatuses.some(o => o.status == OrderStatuses.Canceled)) {
      this.orderStatus = OrderStatuses.Canceled;
      return;
    }
    if (orderStatuses.some(o => o.status == OrderStatuses.Delivered)) {
      this.orderStatus = OrderStatuses.Delivered;
      return;
    }
    if (orderStatuses.some(o => o.status == OrderStatuses.InRoute)) {
      this.orderStatus = OrderStatuses.InRoute;
      return;
    }
    if (orderStatuses.some(o => o.status == OrderStatuses.Processed)) {
      this.orderStatus = OrderStatuses.Processed;
      return;
    }
    if (orderStatuses.some(o => o.status == OrderStatuses.Placed)) {
      this.orderStatus = OrderStatuses.Placed;
      return;
    }
  }


}
