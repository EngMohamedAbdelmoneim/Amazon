import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Address } from '../Models/address';

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

  getAddresses()
  {
    return this.http.get<Array<Address>>('https://localhost:7283/api/Account/getAddresses');
  }

  getDeliveryMethods()
  {
    return this.http.get("https://localhost:7283/api/DeliveryMethods");
  }

  getPaymentMethods()
  {
    return this.http.get("https://localhost:7283/api/PaymentMethods");
  }

  placeOrder(cartId: string, deliveryMethodId: number, paymentMethodId: number = 2, shippingAddressId: string)
  {
    return this.http.post("https://localhost:7283/api/Order", {cartId: cartId, deliveryMethodId: deliveryMethodId, paymentMethodId: paymentMethodId, shippingAddressId: shippingAddressId});
  }
}