export class CartItem {
    id:number
    productName:string
    category:string
    price:number
    pictureUrl:string
    quantity:number


    constructor(id: number, ProductName: string, Category: string,Price:number,PictureUrl:string,Quantity:number)
    {
        this.id = id;
        this.productName = ProductName;
        this.category = Category;
        this.price = Price;
        this.pictureUrl = PictureUrl;
        this.quantity = Quantity;
    }
}
