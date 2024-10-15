import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private apiUrl = '';
  private apiUrlCountries = 'https://restcountries.com/v3.1/all';

  constructor(private http: HttpClient) { }

  fetchCountries(): Observable<any> {
    return this.http.get<any>(this.apiUrlCountries);
  }

  getCountriesWithCodes(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrlCountries);
  }
}