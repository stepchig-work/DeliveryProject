import { OrderStatuses } from "../../enums/order-statuses.enum";

export interface OrderStatusChangeEventArgs {
  orderStatus: OrderStatuses;
  regularUserId: number;
  ownerId: number;
  orderId: number;
}

