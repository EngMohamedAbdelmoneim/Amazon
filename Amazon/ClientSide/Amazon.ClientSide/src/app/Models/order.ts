import { Address } from "./address";
import {Product} from "./product";

export class Order {
    id: number;
    UserId: number;
    UserName: string;
    UserAddress: Array<Address>;
    SelectedAddress: number | null;
    shippingAddressId: string;
    QuantityOrdered: number;
    orderDate: string;
    TotalPrice: number;
    PaymentStatus: string;
    OrderStatus: string;
    items: Array<Product>

    constructor(Id: number, UserId: number, UserName: string, UserAddress: Array<Address>, QuantityOrdered: number,
        orderDate: string, TotalPrice: number, PaymentStatus: string, OrderStatus: string, shippingAddressId: string, items: Array<Product>)
    {
        this.id = Id;
        this.UserId = UserId;
        this.UserName = UserName;
        this.UserAddress = UserAddress;
        this.SelectedAddress = null;
        this.shippingAddressId = shippingAddressId;
        this.QuantityOrdered = QuantityOrdered;
        this.orderDate = orderDate;
        this.TotalPrice = TotalPrice;
        this.PaymentStatus = PaymentStatus;
        this.OrderStatus = OrderStatus;
        this.items = items;
    }
}


