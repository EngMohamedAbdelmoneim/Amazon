import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Category } from '../Models/category';
import { Product } from '../Models/product';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  BaseUrl = 'https://localhost:7283/api';

  constructor(public http: HttpClient) { }

  getParentCategories()
  {
    return this.http.get<Array<Category>>(`${this.BaseUrl}/ParentCategory/GetAllParentCategories`);
  }

  getCategories()
  {
    return this.http.get<Array<Category>>(`${this.BaseUrl}/Category/GetAllCategories`);
  }

  getParentCategoryProducts(ParentCategoryName: string)
  {
    return this.http.get<Array<Product>>(`${this.BaseUrl}/Search/SearchByParentCategoryName`, { params: {ParentCategoryName: ParentCategoryName} });
  }

  getCategoryProducts(ParentCategoryName: string, categoryName: string)
  {
    return this.http.get<Array<Product>>(`${this.BaseUrl}/Search/SearchByCategoryName`, { params: { categoryName: categoryName, ParentCategoryName: ParentCategoryName } });
  }
}
