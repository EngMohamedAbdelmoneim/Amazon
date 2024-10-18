import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Category } from '../Models/category';
import { Product } from '../Models/product';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {


  constructor(public http: HttpClient) { }

  getParentCategories() {
    return this.http.get<Array<Category>>("https://localhost:7283/api/ParentCategory/GetAllParentCategories");
  }
  getCategories() {
    return this.http.get<Array<Category>>("https://localhost:7283/api/Category/GetAllCategories");
  }
  getParentCategoryProducts(ParentCategoryName: string) {
    return this.http.get<Array<Product>>("https://localhost:7283/api/Search/SearchByParentCategoryName", { params: {ParentCategoryName: `${ParentCategoryName}`} });
  }
  getCategoryProducts(ParentCategoryName: string, categoryName: string) {
    return this.http.get<Array<Product>>("https://localhost:7283/api/Search/SearchByCategoryName", { params: { categoryName: `${categoryName}`, ParentCategoryName: `${ParentCategoryName}` } });
  }
}
