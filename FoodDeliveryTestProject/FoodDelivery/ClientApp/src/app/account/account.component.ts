import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { AlertService } from '../alert-system/alert.service';
import { AccountRoles, AccountRoles2LabelMapping } from '../enums/account-role.enum';
import { OrderStatuses } from '../enums/order-statuses.enum';
import { Order } from '../models/order.model';
import { OrderStatusChangeEventArgs } from '../models/signalr-events-args/order-status-change-event-args.interface';
import { Account } from '../models/user.model';
import { AccountService } from '../services/account-service';
import { AuthenticationService } from '../services/authentication.service';
import { OrdersService } from '../services/orders.service';
import { SignalRService } from '../services/signalR.service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html'
})

export class AccountComponent implements OnInit, OnDestroy {

  public currentOrders: Order[];
  public finishedOrders: Order[];
  public accountRoles2LabelMapping = AccountRoles2LabelMapping;
  public userName: string;

  private orderStatusChangeSubscription: Subscription;

  options = {
    autoClose: true,
    keepAfterRouteChange: false
  };

  public get getAccount(): Account {
    return this.authenticationService.getUser;
  }

  public get ifBanAllowed(): boolean {
    return this.authenticationService.isUserOwner;
  }

  constructor(protected authenticationService: AuthenticationService,
    protected orderService: OrdersService,
    signalrService: SignalRService,
    private accountService: AccountService,
    alertService: AlertService) {
    this.orderStatusChangeSubscription = signalrService.onOrderStatusChangedSubject.subscribe(async (args: OrderStatusChangeEventArgs) => {
      if (args.orderStatus == OrderStatuses.Canceled || args.orderStatus == OrderStatuses.Received) {
        this.SetOrders();
      }
      alertService.info("Order updated", this.options);
    });
  }
  ngOnDestroy(): void {
    this.orderStatusChangeSubscription && this.orderStatusChangeSubscription.unsubscribe();
  }

  async ngOnInit(): Promise<void> {
    await this.SetOrders();
  }

  public async banUser() {
    if (this.userName) {
      this.accountService.banUser(this.authenticationService.getUserId, this.userName);
      await this.onUserBanned();
    }
  }

  public async onUserBanned() {
    await this.SetOrders();
  }

  private async SetOrders() {
    let account = this.authenticationService.getUser;
    let orders = [];
    if (this.authenticationService.isUserOwner) {
      orders = await this.orderService.getAllOrdersForOwner(account.entityId);
    }
    else {
      orders = await this.orderService.getAllOrdersForRegularUser(account.entityId);
    }
    this.finishedOrders = orders
      .filter(o => o.latestOrderStatus == OrderStatuses.Canceled || o.latestOrderStatus == OrderStatuses.Received)
      .sort((o1, o2) => o1.creationDate < o2.creationDate ? 1 : -1);

    this.currentOrders = orders
      .filter(o => o.latestOrderStatus != OrderStatuses.Canceled && o.latestOrderStatus != OrderStatuses.Received)
      .sort((o1, o2) => o1.creationDate < o2.creationDate ? 1 : -1);;
  }
}
