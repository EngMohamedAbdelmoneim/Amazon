export class WishListItem {
    id:number
    productName:string
    category:string
    brand:string
    price:number
    pictureUrl:string
    quantity:number


    constructor(id: number, ProductName: string, Category: string,Brand: string = "none",Price:number,PictureUrl:string,Quantity:number)
    {
        this.id = id;
        this.productName = ProductName;
        this.category = Category;
        this.brand = Brand;
        this.price = Price;
        this.pictureUrl = PictureUrl;
        this.quantity = Quantity;
    }
}
