import { Discount } from "./Discount";

export class ProductDto {
    Name: string;
    Description: string;
    Price: number;
    ImageFile: File;
    QuantityInStock: number;
    BrandId: number;
    CategoryId: number;
    Discount:Discount;
    ImagesFiles: File[];

    constructor
        (
            Name: string,
            Price: number,
            Description: string,
            ImageFile: File,
            QuantityInStock: number,
            BrandId: number,
            CategoryId: number,
            Discount:Discount,
            ImagesFiles: File[],
        ) {
        this.Name = Name;
        this.Price = Price;
        this.Description = Description;
        this.ImageFile = ImageFile;
        this.QuantityInStock = QuantityInStock;
        this.BrandId = BrandId;
        this.CategoryId = CategoryId;
        this.Discount = Discount;
        this.ImagesFiles = ImagesFiles;
    }
}
