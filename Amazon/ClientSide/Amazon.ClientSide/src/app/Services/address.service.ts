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
    const headers = new HttpHeaders({
      'Authorization':'bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWJkZWxSYWhtYW5TYWxlaCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL3NpZCI6ImVhOGY4Y2QzLWE2YTAtNGU4Ni1iYWM1LTc0YWJlZGY1OGNmOSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6ImFkbWluQGdtYWlsLmNvbSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNzI5MzUzNzUyLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjgyIiwiYXVkIjoiU2VjdXJlZEFQSVVzZXJzIn0.ZGNFxsDfcLQt-4fbC6Nl_LCcZk6UMvzyoSNuYRqMyMk',
      'Content-Type' :'application/json'
    });
    return this.http.get<Address[]>(`${this.apiUrl}/getAddresses`,{headers});
  }
  addAddresses(address:Address):Observable<Address>{
    const headers = new HttpHeaders({
      'Authorization':'bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWJkZWxSYWhtYW5TYWxlaCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL3NpZCI6ImVhOGY4Y2QzLWE2YTAtNGU4Ni1iYWM1LTc0YWJlZGY1OGNmOSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6ImFkbWluQGdtYWlsLmNvbSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNzI5MzUzNzUyLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjgyIiwiYXVkIjoiU2VjdXJlZEFQSVVzZXJzIn0.ZGNFxsDfcLQt-4fbC6Nl_LCcZk6UMvzyoSNuYRqMyMk',
      'Content-Type' :'application/json'
    });
    return this.http.post<Address>(`${this.apiUrl}/addAddress`,address,{headers});
  }
  updateAddress(address): Observable<any> {
    return this.http.put<any>(`${this.apiUrlGetAddress}/${address.id}`, address);
  }
  deleteAddress(id): Observable<any> {
    return this.http.delete<any>(`${this.apiUrlGetAddress}/${id}`);
}
}
