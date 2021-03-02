import { EventEmitter, Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { Subject } from 'rxjs';
import { OrderStatuses } from '../enums/order-statuses.enum';
import { OrderStatusChangeEventArgs } from '../models/signalr-events-args/order-status-change-event-args.interface';
import { AuthenticationService } from './authentication.service';

@Injectable()
export class SignalRService {

  private readonly hubConnection: HubConnection;
  private readonly serverTimeoutInMilliseconds = 200000;


  private readonly onHubConnectionErrorSubject = new Subject<void>();
  private readonly onHubConnectionError = this.onHubConnectionErrorSubject.asObservable();

  public readonly onOrderStatusChangedSubject = new Subject<OrderStatusChangeEventArgs>();
  private readonly onOrderStatusChanged = this.onOrderStatusChangedSubject.asObservable();

  public initialized: boolean = false;
  public initFailed: boolean = false;

  constructor(private authenticationService: AuthenticationService) {
    const hubConnection = new HubConnectionBuilder()
      .withUrl('/foodDeliveryHub')
      .build();

    this.startHubConnection(hubConnection);
    this.setOnHubConnectionClose(hubConnection);
    this.setUpOrderStatusMonitoring(hubConnection)

    this.hubConnection = hubConnection;
  }
  private setUpOrderStatusMonitoring(hubConnection: HubConnection) {
    hubConnection.on("onOrderStatusChanged", (args: any) => {
      if (this.authenticationService.getUserId != args.regularUserId
        && this.authenticationService.getUserId != args.ownerId) {
        return;
      }

      var argsn: OrderStatusChangeEventArgs = {
        regularUserId: args.regularUserId,
        ownerId: args.ownerId,
        orderId: args.orderId,
        orderStatus: args.orderStatus
      };
      this.onOrderStatusChangedSubject.next(argsn);
    })
  }

  private startHubConnection(hubConnection: HubConnection) {
    hubConnection.serverTimeoutInMilliseconds = this.serverTimeoutInMilliseconds;

    hubConnection
      .start()
      .then(() => console.log("Connection started!"))
      .catch(_ => {
        this.initFailed = true;
        this.onHubConnectionErrorSubject.next();
      })
      .finally(() => this.initialized = true);
  }

  private setOnHubConnectionClose(hubConnection: HubConnection) {
    hubConnection
      .onclose(error => {
        console.log(error);
        this.onHubConnectionErrorSubject.next();
      });
  }
}
