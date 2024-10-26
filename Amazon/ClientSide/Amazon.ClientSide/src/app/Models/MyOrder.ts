import { Address } from "./address";
import { Product } from "./product";

export class MyOrder 
{
    id: number;
    UserId: number;
    UserName: string;
    UserAddress: Array<Address>;
    SelectedAddress: number | null;
    QuantityOrdered: number;
    orderDate: string;
    total: number;
    paymentStatus: string;
    orderStatus: string;
    items: Array<MyOrderProduct>;

    constructor(Id: number, UserId: number, UserName: string, UserAddress: Array<Address>, QuantityOrdered: number,
        orderDate: string, total: number, paymentStatus: string, orderStatus: string, items: MyOrderProduct[]) 
    {
        this.id = Id;
        this.UserId = UserId;
        this.UserName = UserName;
        this.UserAddress = UserAddress;
        this.SelectedAddress = null;
        this.QuantityOrdered = QuantityOrdered;
        this.orderDate = orderDate;
        this.total = total;
        this.paymentStatus = paymentStatus;
        this.orderStatus = orderStatus;
        this.items = items;
    }
}

class MyOrderProduct
{
    brand: string;
    category: string;
    pictureUrl: string;
    price: number;
    productId: number;
    productName: string;
    quantity: number;
}


