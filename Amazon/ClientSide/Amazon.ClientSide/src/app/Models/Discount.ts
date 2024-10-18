export class Discount {
    discountPercentage: number;
    priceAfterDiscount: number;
    discountStarted: boolean;
    startDate: Date;
    endDate: Date;

    constructor(
        discountPercentage: number,
        priceAfterDiscount: number,
        discountStarted: boolean,
        startDate: Date,
        endDate: Date
    ) {
        this.discountPercentage = discountPercentage;
        this.priceAfterDiscount = priceAfterDiscount;
        this.discountStarted = discountStarted;
        this.startDate = startDate;
        this.endDate = endDate;
    }
}
