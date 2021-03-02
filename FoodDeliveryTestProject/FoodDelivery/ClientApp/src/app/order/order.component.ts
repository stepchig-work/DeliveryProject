import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NgbAlert } from '@ng-bootstrap/ng-bootstrap';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { Order } from '../models/order.model';
import { AuthenticationService } from '../services/authentication.service';
import { OrderCreationService } from '../services/order-creation.service';
import { OrdersService } from '../services/orders.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html'
})
export class OrderComponent implements OnInit {

  public get getOrder(): Order {
    return this.order ? this.order : this.orderCreationService.getCurrentOrder;
  }

  private order: Order;

  @ViewChild('selfClosingAlert', { static: false }) alertMessage: NgbAlert;

  constructor(private orderService: OrdersService,
    private orderCreationService: OrderCreationService,
    private authenticationService: AuthenticationService,
    private route: ActivatedRoute,) {

  }

  ngOnInit(): void {
    this.route.params.subscribe(async (params) => {
      if (params) {
        const orderId = params['id'];
        if (orderId) {
          this.order = await this.orderService.getOrder(orderId);
          return;
        }
      }
    });
  }

  public sendOrder() {
    this.orderCreationService.sendOrder();
  }
}
