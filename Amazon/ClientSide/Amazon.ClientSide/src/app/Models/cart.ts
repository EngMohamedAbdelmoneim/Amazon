import { CartItem } from "./cart-item";

interface ICart
{
    id: string;
    items: CartItem[];
    clientSecret?: string;
    paymentSecret?: string;
    deliveryMethodID?: number;
    shippingPrice: number;
}
 
export class Cart implements ICart
{
    id: string;
    items: CartItem[];
    clientSecret?: string;
    paymentSecret?: string;
    deliveryMethodId?: number;
    shippingPrice = 0;

    // constructor(id:string, items:CartItem[])
    // {
    //     this.id = id;
    //     this.items = items
    // }
}
