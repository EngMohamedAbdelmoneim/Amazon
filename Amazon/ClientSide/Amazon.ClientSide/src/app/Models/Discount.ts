export class Discount {
    discountPercentage: number | null;
    priceAfterDiscount: number | null;
    discountStarted: boolean | null;
    startDate: Date | null;
    endDate: Date | null;

    constructor(
        discountPercentage: number  | null,
        priceAfterDiscount: number  | null,
        discountStarted: boolean  | null,
        startDate: Date  | null,
        endDate: Date | null
    ) {
        this.discountPercentage = discountPercentage;
        this.priceAfterDiscount = priceAfterDiscount;
        this.discountStarted = discountStarted;
        this.startDate = startDate;
        this.endDate = endDate;
    }
}
