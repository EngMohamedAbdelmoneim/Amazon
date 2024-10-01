import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../Models/product';
import { hasUncaughtExceptionCaptureCallback } from 'process';

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  constructor(public http: HttpClient) { }

  searchType: string = "All"; 

  Search(query: string)
  {
    if(this.searchType == "All")
    {
      console.log("all");
      return this.http.get<Array<Product>>("https://localhost:7283/api/Search/SearchByString", {params: {str:`${query}`}});
    }
    else
    {
      console.log("specific");
      console.log(this.searchType);
      return this.http.get<Array<Product>>("https://localhost:7283/api/Search/SearchByProductNameAndCategoryId", {params: {productName:`${query}`, categoryId: this.searchType}});
      // switch(this.searchType)
      // {
      //   case "ProductCategory":
      //     return this.http.get<Array<Product>>("https://localhost:7283/api/Search/SearchByProductNameAndCategoryId", {params: {productName:`${query}`, categoryId: "1"}});
      //   default:
      //     console.log("Epic Fail")
      //     return this.http.get<Array<Product>>("https://localhost:7283/api/Search/SearchByString", {params: {str:`${query}`}}); 
      // }
    }
  }
}
