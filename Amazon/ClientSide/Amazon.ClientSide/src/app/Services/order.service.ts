import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Address } from '../Models/address';
import { Order } from '../Models/order';
import { MyOrder } from '../Models/MyOrder';

@Injectable({
  providedIn: 'root'
})
export class OrderService
{
  private apiUrlCountries = 'https://restcountries.com/v3.1/all';

  constructor(private http: HttpClient) { }

  // region Get Methods

  fetchCountries(): Observable<any>
  {
    return this.http.get<any>(this.apiUrlCountries);
  }

  getCountriesWithCodes(): Observable<any[]>
  {
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

  getOrders()
  {
    return this.http.get<Array<MyOrder>>('https://localhost:7283/api/Order');
  }

  getOrderById(Id: string)
  {
    return this.http.get<Order>('https://localhost:7283/api/Order/' + Id);
  }

  // endregion

  // region Post Methods

  placeOrder(cartId: string, deliveryMethodId: number, paymentMethodId: number = 2, shippingAddressId: string)
  {
    return this.http.post("https://localhost:7283/api/Order", {cartId: cartId, deliveryMethodId: deliveryMethodId, paymentMethodId: paymentMethodId, shippingAddressId: shippingAddressId});
  }

  cancelOrder(Id: number)
  {
    return this.http.post(`https://localhost:7283/api/Order/cancel/${Id}`, null);
  }

  // endregion
}
