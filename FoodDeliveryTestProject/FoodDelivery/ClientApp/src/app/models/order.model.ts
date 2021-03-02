import { OrderStatuses } from "../enums/order-statuses.enum";
import { MealForOrder } from "./meal-for-order.model";
import { OrderStatus } from "./order-status.model";

export interface Order {
  entityId: number;
  restaurantId: number;
  customerId: number;
  creationDate: Date;
  mealsForOrder: MealForOrder[]
  orderStatuses: OrderStatus[];
  latestOrderStatus: OrderStatuses;
  price: number;
  restaurantName: string;
}

