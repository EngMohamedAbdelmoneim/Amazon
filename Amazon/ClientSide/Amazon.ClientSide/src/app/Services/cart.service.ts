import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CartItems } from '../Models/cart-items';
import { catchError, Observable, of, switchMap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartService {

 
  constructor(public http: HttpClient) { }

  addToCart(id: string, cartItems: any) {
    console.log(cartItems);
    return this.http.post<any>(`https://localhost:7283/api/Cart/SetCart?cartId=${id}`, { id: id, items: cartItems });
  }
  getAllFromCart(cartId: string): Observable<any> {
    return this.http.get<any>(`https://localhost:7283/api/Cart/GetCartById/${cartId}`, { params: { cartId: `${cartId}` } })
      .pipe(
        catchError((error) => {
          if (error.status === 400 || error.status === 404) {
            console.log("55555555555555555555")
            return of(null);
          } else {
            // Handle other errors as necessary, or rethrow them
            return of(null); // Optionally return null for other errors as well
          }
        })
      );
  }
  deleteCart(id: string) {
    return this.http.delete<any>(`https://localhost:7283/api/Cart/DeleteCart/${id}`);
  }


  // deleteCartItem(cartId: string, id: number): Observable<CartItems[]> {
  //   console.log('server deleting');

  //   return this.getAllFromCart(cartId).pipe(
  //     switchMap(data => {
  //       const index = this.cartItems.items.findIndex(item => item.id === id);

  //       if (index !== -1) {
  //         this.cartItems.items.splice(index, 1);

  //         return this.addToCart(cartId,  this.cartItems.items).pipe(
  //           catchError(err => {
  //             console.error('Failed to update the cart after deletion:', err);
  //             return of([]); 
  //           })
  //         );
  //       } else {
  //         console.log('Object not found');
  //         return of( this.cartItems.items); // Return the original cart if no item found
  //       }
  //     }),
  //     catchError(err => {
  //       console.error('Failed to fetch cart data:', err);
  //       return of([]); // Return an empty array on error
  //     })
  //   );
  // }

}
