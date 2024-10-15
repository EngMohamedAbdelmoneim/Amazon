export class Order {
    id:number;
    UserId:number;
    UserName:string;
    UserAddress:string[];
    SelectedAddress:number|null;
    QuantityOrdered:number;
    OrderDate:string;
    TotalPrice:number;
    PaymentStatus:string;
    OrderStatus:string;

    constructor(Id:number,UserId:number,UserName:string,UserAddress:string[],QuantityOrdered:number,
        OrderDate:string,TotalPrice:number,PaymentStatus:string,OrderStatus:string){
        this.id= Id;
        this.UserId = UserId;
        this.UserName = UserName;
        this.UserAddress = UserAddress;
        this.SelectedAddress = null;
        this.QuantityOrdered = QuantityOrdered;
        this.OrderDate = OrderDate;
        this.TotalPrice = TotalPrice;
        this.PaymentStatus = PaymentStatus;
        this.OrderStatus = OrderStatus;
}       
}