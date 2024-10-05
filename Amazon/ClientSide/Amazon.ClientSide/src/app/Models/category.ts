export class Category 
{
    id: number
    name: string;
    parentCategory:string;

    constructor(id:number, name: string,parentCategory:string)
    {
        this.id = id;
        this.name = name;
        this.parentCategory = parentCategory;
    }
}
