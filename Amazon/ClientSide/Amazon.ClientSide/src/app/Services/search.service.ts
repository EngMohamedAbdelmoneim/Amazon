import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../Models/product';
import { hasUncaughtExceptionCaptureCallback } from 'process';
import { PaginatedProducts } from '../Models/PaginatedProducts';

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  constructor(public http: HttpClient) { }

  searchType: string = "All"; 

  Search(query: string, page: number = 1)
  {
    if(this.searchType == "All")
    {
      return this.http.get<PaginatedProducts>(`https://localhost:7283/api/Product/GetProducts/GetAll?PageIndex=${page}`, {params: {Search:query}});
    }
    else
    {
      console.log(this.searchType);
      return this.http.get<PaginatedProducts>(`https://localhost:7283/api/Product/GetProducts/GetAll?PageIndex=${page}`, {params: {Search: query, CategoryId: this.searchType}});
    }
  }

  SearchByBrand(brandId: number, page: number = 1)
  {
    return this.http.get<PaginatedProducts>(`https://localhost:7283/api/Product/GetProducts/GetAll?PageIndex=${page}`, {params: {BrandId: brandId}});
  }

  Filter(query: string, sort: string, page: number = 1)
  {
    return this.http.get<PaginatedProducts>(`https://localhost:7283/api/Product/GetProducts/GetAll?PageIndex=${page}`, {params: {Search: query, Sort: sort}});
  }
}
