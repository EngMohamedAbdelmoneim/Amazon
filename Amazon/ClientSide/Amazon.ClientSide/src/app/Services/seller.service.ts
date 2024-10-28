import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../Models/product';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class SellerService {
  constructor(private http: HttpClient) {}

  private SellerUrl = 'https://localhost:7283/api/Seller';

  // private apiUrlAdd = 'https://localhost:7283/api/Product/AddProduct';
  // private apiUrlUpdate = 'https://localhost:7283/api/Product/UpdateProduct';
  // private apiUrlDelete = 'https://localhost:7283/api/Product/DeleteProduct';

  GetSellerProducts(): Observable<Product[]> {
    return this.http.get<Array<Product>>(
      `${this.SellerUrl}/GetAllSellerProducts`
    );
  }
  GetSellerAcceptedProducts(): Observable<Product[]> {
    return this.http.get<Array<Product>>(
      `${this.SellerUrl}/GetSellerAcceptedProducts`
    );
  }
  GetSellerPendingProducts(): Observable<Product[]> {
    return this.http.get<Array<Product>>(
      `${this.SellerUrl}/GetSellerPendingProducts`
    );
  }

  GetSellerProductById(Id: number) {
    return this.http.get<Product>(
      `${this.SellerUrl}/GetSellerProductById/${Id}`
    );
  }

  AddProduct(product: any): Observable<Product> {
    return this.http.post<Product>(`${this.SellerUrl}/AddProduct`, product);
  }
  UpdateProduct(product: any, id: number): Observable<Product> {
    return this.http.put<Product>(
      `${this.SellerUrl}/UpdateProduct/${id}`,
      product
    );
  }
  DeleteProduct(id: number): Observable<Product[]> {
    return this.http.delete<Product[]>(`${this.SellerUrl}/DeleteProduct/${id}`);
  }

  GetAllSellerEarnings(): Observable<any> {
    return this.http.get<any>(`${this.SellerUrl}/GetAllSellerEarnings`);
  }

  GetAllSellerEarningsWithDetails(): Observable<any[]> {
    return this.http.get<Array<any>>(
      `${this.SellerUrl}/GetAllSellerEarningsWithDetails`
    );
  }

  GetSellerEarningsByProductId(Id: number) {
    return this.http.get<Product>(
      `${this.SellerUrl}/GetSellerEarningsByProductId/${Id}`
    );
  }
}
