import { DatePipe } from "@angular/common";
import { OrderStatuses } from "../enums/order-statuses.enum";

export interface OrderStatus {
  entityId: number;
  orderId: number;
  status: OrderStatuses;
  statusChangeTime: Date;
}

