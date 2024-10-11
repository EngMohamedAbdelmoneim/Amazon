import { CartItems } from "./cart-items";
import { Product } from "./product";

export class Cart 
{
    id: string
    items: CartItems[]

    constructor(id:string, items:CartItems[])
    {
        this.id = id;
        this.items = items
    }
}
