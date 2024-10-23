import { Injectable } from '@angular/core';
import { HttpClient, HttpParams} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '../Models/product';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = 'https://localhost:7283/api/Product/GetAllProducts';
  private apiUrlId = 'https://localhost:7283/api/Product/GetProduct';
  private apiUrlSellerId = 'https://localhost:7283/api/Product/GetSellerProducts';
  private apiUrlAdd = 'https://localhost:7283/api/Product/AddProduct';
  private apiUrlUpdate = 'https://localhost:7283/api/Product/UpdateProduct';
  private apiUrlDelete = 'https://localhost:7283/api/Product/DeleteProduct';
  
  constructor(private http:HttpClient) { }

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiUrl);
  }
  GetSellerProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiUrlSellerId);
  }

  getProductById(id:number):Observable<Product>{
    return this.http.get<Product>(`${this.apiUrlId}/${id}`);
  }

  AddProduct(product:any):Observable<Product>{
    return this.http.post<Product>(`${this.apiUrlAdd}`,product );
  }
  UpdateProduct(product:any,id:number):Observable<Product>{
    return this.http.put<Product>(`${this.apiUrlUpdate}/${id}`,product);
  }
  DeleteProduct(id:number):Observable<Product[]>{
    return this.http.delete<Product[]>(`${this.apiUrlDelete}/${id}`);
  }
}
