export class Product {
    id: number
    name: string;
    description: string;
    price: number;
    pictureUrl: string;
    productImages: string[];
    categoryName: string;
    brandName: string;
    quantityInStock: number;

    constructor
        (
            id: number,
            name: string,
            price: number,
            description: string,
            pictureUrl: string,
            productImages: string[],
            categoryName: string,
            brandName: string,
            quantityInStock: number
        ) {
        this.id = id;
        this.name = name;
        this.price = price;
        this.description = description;
        this.pictureUrl = pictureUrl;
        this.productImages = productImages;
        this.categoryName = categoryName;
        this.brandName = brandName;
        this.quantityInStock = quantityInStock;
    }
}
