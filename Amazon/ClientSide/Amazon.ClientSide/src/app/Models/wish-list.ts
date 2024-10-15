import { WishListItem } from "./wish-list-item";
 
export class WishList 
{
    id: string
    items: WishListItem[]

    constructor(id:string, items:WishListItem[])
    {
        this.id = id;
        this.items = items
    }
}
