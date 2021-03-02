import { TestBed } from '@angular/core/testing';
import {
  HttpClientTestingModule,
  HttpTestingController
} from '@angular/common/http/testing';
import { OrderStatuses } from '../enums/order-statuses.enum';
import { Order } from '../models/order.model';
import { OrdersService } from './orders.service';

describe('OrdersService', () => {
  let service: OrdersService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [OrdersService]
    });
    service = TestBed.inject(OrdersService);
  });

  it('can load instance', () => {
    expect(service).toBeTruthy();
  });

    it('add order api call', () => {
      const httpTestingController = TestBed.inject(HttpTestingController);
      const orderStub: any = <any>{};
      service.addOrder(orderStub).then(res => {
        expect(res).toEqual(orderStub);
      });
      const req = httpTestingController.expectOne('api/orders/addOrder');
      expect(req.request.method).toEqual('POST');
      req.flush(orderStub);
      httpTestingController.verify();
    });

    it('update status api call', () => {
      const httpTestingController = TestBed.inject(HttpTestingController);
      const orderStub: any = <any>{};
      service.changeStatus(OrderStatuses.Created, 1, 2).then(res => {
        expect(res).toEqual(undefined);
      });
      const req = httpTestingController.expectOne('api/orders/updateStatus');
      expect(req.request.method).toEqual('PUT');
      req.flush(orderStub);
      httpTestingController.verify();
    });

    it('get order api call', () => {
      const httpTestingController = TestBed.inject(HttpTestingController);
      const orderStub: any = <any>{};
      service.getOrder(1).then(res => {
        expect(res).toEqual(orderStub);
      });
      const req = httpTestingController.expectOne('api/orders/getOrder?orderId=1');
      expect(req.request.method).toEqual('GET');
      req.flush(orderStub);
      httpTestingController.verify();
    });

    it('get order statuses api call', () => {
      const httpTestingController = TestBed.inject(HttpTestingController);
      const orderStub: any = <any>{};
      service.getOrderStatuses(1).then(res => {
        expect(res).toEqual(orderStub);
      });
      const req = httpTestingController.expectOne('api/orders/getOrderStatuses?orderId=1');
      expect(req.request.method).toEqual('GET');
      req.flush(orderStub);
      httpTestingController.verify();
    });

    it('get meals for order api call', () => {
      const httpTestingController = TestBed.inject(HttpTestingController);
      const orderStub: any = <any>{};
      service.getMealsForOrder(1).then(res => {
        expect(res).toEqual(orderStub);
      });
      const req = httpTestingController.expectOne('api/orders/getMealsForOrder?orderId=1');
      expect(req.request.method).toEqual('GET');
      req.flush(orderStub);
      httpTestingController.verify();
    });

    it('get orders for regular user api call', () => {
      const httpTestingController = TestBed.inject(HttpTestingController);
      const orderStub: any = <any>{};
      service.getAllOrdersForRegularUser(1).then(res => {
        expect(res).toEqual(orderStub);
      });
      const req = httpTestingController.expectOne('api/orders/getAllOrdersForRegularUser?accountId=1');
      expect(req.request.method).toEqual('GET');
      req.flush(orderStub);
      httpTestingController.verify();
    });

    it('get orders for owner api call', () => {
      const httpTestingController = TestBed.inject(HttpTestingController);
      const orderStub: any = <any>{};
      service.getAllOrdersForOwner(1).then(res => {
        expect(res).toEqual(orderStub);
      });
      const req = httpTestingController.expectOne('api/orders/getAllOrdersForOwner?accountId=1');
      expect(req.request.method).toEqual('GET');
      req.flush(orderStub);
      httpTestingController.verify();
    });
});
