import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Brand } from '../Models/brand';

@Injectable({
  providedIn: 'root'
})
export class BrandService {

  private apiUrl = 'https://localhost:7283/api/Brand/GetAllBrands';
  private apiUrlId = 'https://localhost:7283/api/Brand/GetBrandById';

  constructor(private http:HttpClient) { }

  getBrands(): Observable<Brand[]> {
    return this.http.get<Brand[]>(this.apiUrl);
  }

  getBrandById(id:number):Observable<Brand>{
    return this.http.get<Brand>(`${this.apiUrlId}/${id}`);
  }
}
