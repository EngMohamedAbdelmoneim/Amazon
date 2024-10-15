export class WishListItem {
    id:Number
    productName:string
    category:string
    brand:string
    price:Number
    pictureUrl:string
    quantity:Number


    constructor(id: Number, ProductName: string, Category: string,Brand: string = "none",Price:Number,PictureUrl:string,Quantity:Number
    ){
        this.id = id;
        this.productName = ProductName;
        this.category = Category;
        this.brand = Brand;
        this.price = Price;
        this.pictureUrl = PictureUrl;
        this.quantity = Quantity;
    }
}
