export enum OrderStatuses {
  Created = 0,
  Placed = 1,
  Canceled = 2,
  Processed = 3,
  InRoute = 4,
  Delivered = 5,
  Received = 6
}
export const OrderStatuses2LabelMapping: Record<OrderStatuses, string> = {
  [OrderStatuses.Created]: "Created",
  [OrderStatuses.Placed]: "Placed",
  [OrderStatuses.Canceled]: "Canceled",
  [OrderStatuses.Processed]: "Processing",
  [OrderStatuses.InRoute]: "In route",
  [OrderStatuses.Delivered]: "Delivered",
  [OrderStatuses.Received]: "Received"

};
