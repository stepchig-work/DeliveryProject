import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { OrderStatuses } from "../enums/order-statuses.enum";
import { MealForOrder } from "../models/meal-for-order.model";
import { OrderStatus } from "../models/order-status.model";
import { Order } from "../models/order.model";


@Injectable()
export class OrdersService {
  private addOrderUrl: string = "api/orders/addOrder";
  private updateStatusUrl: string = "api/orders/updateStatus";
  private getOrderUrl: string = "api/orders/getOrder";
  private getOrderStatusesUrl: string = "api/orders/getOrderStatuses";
  private getMealsForOrderUrl: string = "api/orders/getMealsForOrder";
  private getAllOrdersForRegularUserUrl: string = "api/orders/getAllOrdersForRegularUser";
  private getAllOrdersForOwnerUrl: string = "api/orders/getAllOrdersForOwner";

  constructor(private readonly httpClient: HttpClient) { }


  async addOrder(order: Order): Promise<void> {
    return await this.httpClient.post<void>(this.addOrderUrl, order).toPromise();
  }

  async changeStatus(orderStatus: OrderStatuses, orderId: number, accountid: number) {
    let orderStatusChangeModel = {
      orderStatus: orderStatus,
      accountid: accountid,
      orderId: orderId
    };
    await this.httpClient.put(this.updateStatusUrl, orderStatusChangeModel).toPromise();
  }

  async getOrder(orderId: number) {
    let params = new HttpParams();
    params = params.set('orderId', orderId.toString());
    return await this.httpClient.get<any>(this.getOrderUrl, { params: params }).toPromise();
  }

  async getAllOrdersForRegularUser(accountId: number): Promise<Order[]> {
    let params = new HttpParams();
    params = params.set('accountId', accountId.toString());
    return await this.httpClient.get<any>(this.getAllOrdersForRegularUserUrl, { params: params }).toPromise();
  }

  async getAllOrdersForOwner(accountId: number): Promise<Order[]>  {
    let params = new HttpParams();
    params = params.set('accountId', accountId.toString());
    return await this.httpClient.get<any>(this.getAllOrdersForOwnerUrl, { params: params }).toPromise();
  }

  async getOrderStatuses(orderId: number): Promise<OrderStatus[]> {
    let params = new HttpParams();
    params = params.set('orderId', orderId.toString());
    return await this.httpClient.get<OrderStatus[]>(this.getOrderStatusesUrl, { params: params }).toPromise();
  }

  async getMealsForOrder(orderId: number): Promise<MealForOrder[]> {
    let params = new HttpParams();
    params = params.set('orderId', orderId.toString());
    return await this.httpClient.get<MealForOrder[]>(this.getMealsForOrderUrl, { params: params }).toPromise();
  }
}
