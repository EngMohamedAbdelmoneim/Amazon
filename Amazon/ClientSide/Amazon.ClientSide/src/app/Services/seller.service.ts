import { Injectable } from '@angular/core';
import {Observable} from "rxjs";
import {Product} from "../Models/product";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class SellerService
{

  constructor(private http: HttpClient) { }

  private SellerProducts = 'https://localhost:7283/api/Seller/GetAllSellerProducts';
  private SellerProduct = 'https://localhost:7283/api/Seller/GetSellerProductById';




  GetSellerProducts(): Observable<Product[]>
  {
    return this.http.get<Array<Product>>(this.SellerProducts);
  }

  GetSellerProductById(Id: number)
  {
    return this.http.get<Product>(`${this.SellerProduct}/${Id}`);
  }

}
