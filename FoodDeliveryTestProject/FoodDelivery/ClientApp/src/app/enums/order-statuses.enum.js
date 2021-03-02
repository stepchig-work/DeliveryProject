export var OrderStatuses;
(function (OrderStatuses) {
    OrderStatuses[OrderStatuses["Created"] = 0] = "Created";
    OrderStatuses[OrderStatuses["Placed"] = 1] = "Placed";
    OrderStatuses[OrderStatuses["Canceled"] = 2] = "Canceled";
    OrderStatuses[OrderStatuses["Processed"] = 3] = "Processed";
    OrderStatuses[OrderStatuses["InRoute"] = 4] = "InRoute";
    OrderStatuses[OrderStatuses["Delivered"] = 5] = "Delivered";
    OrderStatuses[OrderStatuses["Received"] = 6] = "Received";
})(OrderStatuses || (OrderStatuses = {}));
export const OrderStatuses2LabelMapping = {
    [OrderStatuses.Created]: "Created",
    [OrderStatuses.Placed]: "Placed",
    [OrderStatuses.Canceled]: "Canceled",
    [OrderStatuses.Processed]: "Processing",
    [OrderStatuses.InRoute]: "In route",
    [OrderStatuses.Delivered]: "Delivered",
    [OrderStatuses.Received]: "Received"
};
//# sourceMappingURL=order-statuses.enum.js.map