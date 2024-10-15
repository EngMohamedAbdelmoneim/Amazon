import { CartItem } from "./cart-item";
 
export class Cart 
{
    id: string
    items: CartItem[]

    constructor(id:string, items:CartItem[])
    {
        this.id = id;
        this.items = items
    }
}
