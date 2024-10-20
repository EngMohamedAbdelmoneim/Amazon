
export class Review {
    id: number;
    rating: number;
    reviewHeadLine: string;
    reviewText: string;
    reviewDate: Date;
    productId: number;
    appUserName: string;
    appUserEmail: string;

    constructor(
        id: number,
        rating: number,
        reviewHeadLine: string,
        reviewText: string,
        reviewDate: Date,
        productId: number,
        appUserName: string,
        appUserEmail: string
    ) {
        this.id = id
        this.rating = rating
        this.reviewHeadLine = reviewHeadLine
        this.reviewText = reviewText
        this.reviewDate = reviewDate
        this.productId = productId
        this.appUserName = appUserName
        this.appUserEmail = appUserEmail
    }
}
