import { Product } from "./product"

export class PaginatedProducts
{
    count: number;
    pageIndex: number;
    pageSize: number;
    data: Array<Product>;

    constructor(count: number, pageIndex: number, pageSize: number, data: Array<Product>)
    {
        this.count = count;
        this.pageIndex = pageIndex;
        this.pageSize = pageSize;
        this.data = data;
    }
}