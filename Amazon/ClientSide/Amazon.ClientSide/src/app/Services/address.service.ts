import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Address } from '../Models/address';

@Injectable({
  providedIn: 'root'
})
export class AddressService {

  private apiUrl = 'https://localhost:7283/api/Account';
  private apiUrlGetAddress = 'https://localhost:7283/api/Account'
  constructor(private http:HttpClient) { }

  getSavedAddresses():Observable<Address[]>{
    return this.http.get<Address[]>(`${this.apiUrl}/getAddresses`);
  }
  addAddresses(address:Address):Observable<Address>{
    return this.http.post<Address>(`${this.apiUrl}/addAddress`,address);
  }
  updateAddress(address): Observable<any> {
    return this.http.put<any>(`${this.apiUrlGetAddress}/${address.id}`, address);
  }
  deleteAddress(id): Observable<any> {
    return this.http.delete<any>(`${this.apiUrlGetAddress}/${id}`);
}
}
