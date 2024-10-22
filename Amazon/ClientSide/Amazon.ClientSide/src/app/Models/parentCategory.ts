
export class ParentCategory 
{
    id: number
    name: string;
    categories: string[];


    constructor(id:number, name: string,categories: string[])
    {
        this.id = id;
        this.name = name;
        this.categories = categories;
    }
}
