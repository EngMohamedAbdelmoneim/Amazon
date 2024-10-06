import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '../Models/product';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = 'https://localhost:7283/api/Product/GetAllProducts';
  private apiUrlId = 'https://localhost:7283/api/Product/getProductById'
  constructor(private http:HttpClient) { }

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiUrl);
  }

  getProductById(id:number):Observable<Product>{
    return this.http.get<Product>(`${this.apiUrlId}/${id}`);
  }
}
