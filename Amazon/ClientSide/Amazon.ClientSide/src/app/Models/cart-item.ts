export class CartItem {
    id:Number
    productName:string
    category:string
    price:Number
    pictureUrl:string
    quantity:Number


    constructor(id: Number, ProductName: string, Category: string,Price:Number,PictureUrl:string,Quantity:Number
    ){
        this.id = id;
        this.productName = ProductName;
        this.category = Category;
        this.price = Price;
        this.pictureUrl = PictureUrl;
        this.quantity = Quantity;
    }
}
