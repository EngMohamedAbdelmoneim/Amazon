import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Address } from '../Models/address';

@Injectable({
  providedIn: 'root'
})
export class AddressService {

  private apiUrl = 'https://localhost:7283/api/Account';

  constructor(private http:HttpClient) {}

  getSavedAddresses():Observable<Address[]>
  {
    return this.http.get<Address[]>(`${this.apiUrl}/getAddresses`);
  }

  addAddresses(address:Address):Observable<Address>
  {
    return this.http.post<Address>(`${this.apiUrl}/addAddress`,address);
  }

  updateAddress(address:Address): Observable<Address>
  {
    return this.http.put<Address>(`${this.apiUrl}/editAddress/${address.id}`, address);
  }

  deleteAddress(addressId: string)
  {
    return this.http.delete(`${this.apiUrl}/deleteAddress/${addressId}`, { responseType: 'text'} );
  }

  setDefaultAddress(addressId:string):Observable<Address>
  {
    return this.http.post<Address>(`${this.apiUrl}/setDefaultAddress?id=${addressId}`,null);
  }
}
