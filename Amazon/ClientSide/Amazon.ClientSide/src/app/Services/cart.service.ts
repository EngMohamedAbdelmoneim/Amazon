import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of, Subscription } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { CartItem } from '../Models/cart-item';
import { CookieService } from 'ngx-cookie-service';
import { Cart } from '../Models/cart';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private cartProductSource = new BehaviorSubject<any[]>([]);
  cartProduct$ = this.cartProductSource.asObservable();

  private cartQntSource = new BehaviorSubject<any>(0);
  cartQnt = this.cartQntSource.asObservable();

  private basketSource = new BehaviorSubject<Cart | null>(null);

  sub: Subscription | null = null;
  ClientSecret: string;

  constructor(
    private http: HttpClient,
    public cookieService: CookieService,
    public toastr: ToastrService
  ) {}

  updateCart(cartProducts: any[]) {
    this.cartProductSource.next(cartProducts);
    this.CartQnt();
  }

  updateCartQnt(cartQnt: number) {
    this.cartQntSource.next(cartQnt);
  }

  addToCart(cartId: string, cartItam: any) {
    this.sub = this.getAllFromCart(cartId).subscribe({
      next: (data) => {
        if (data != null) {
          console.log(data.items);
          const updatedProducts = [...data.items, cartItam];
          console.log('products :', updatedProducts);
          this.updateCart(updatedProducts);
          this.setToCart(cartId, updatedProducts);
        } else {
          this.updateCart([cartItam]);
          this.setToCart(cartId, [cartItam]);
        }
      },
    });
  }

  updateCartWithItem(cartId: string, newItem: CartItem): void {
    this.getAllFromCart(cartId).subscribe((cartData) => {
      if (cartData != null) {
        let cart = cartData.items;

        const existingItemIndex = cart.findIndex(
          (item) => item.id === newItem.id
        );

        if (existingItemIndex !== -1) {
          console.log('old qnt:', cart[existingItemIndex].quantity);
          console.log('new qnt:', newItem.quantity);
          if (
            cart[existingItemIndex].quantity + Number(newItem.quantity) <=
            10
          ) {
            cart[existingItemIndex].quantity += Number(newItem.quantity);
          } else {
            cart[existingItemIndex].quantity = 10;
          }
          console.log(
            `Item exists, new quantity: ${cart[existingItemIndex].quantity}`
          );
        } else {
          cart.push(newItem);
          console.log(`Item added to the cart: ${newItem.productName}`);
        }
        this.updateCart(cart);
        this.setToCart(cartId, cart);
      } else {
        this.addToCart(cartId, newItem);
      }
    });
  }

  updateCartItemQnt(cartId: string, newItem: CartItem): void {
    if (newItem.quantity != 0) {
      this.getAllFromCart(cartId).subscribe((cartData) => {
        let cart = cartData.items;

        const existingItemIndex = cart.findIndex(
          (item) => item.id === newItem.id
        );
        console.log('old qnt:', cart[existingItemIndex].quantity);
        console.log('new qnt:', newItem.quantity);
        cart[existingItemIndex].quantity = newItem.quantity;
        console.log(
          `Item exists, new quantity: ${cart[existingItemIndex].quantity}`
        );
        this.updateCart(cart);
        this.setToCart(cartId, cart);
      });
    } else {
      this.removeFromCart(cartId, newItem.id);
    }
  }

  CartQnt() {
    console.log('cart qtn loaded');
    this.cartProduct$.subscribe({
      next: (data) => {
        let Qnt: number = 0;
        if (data != null) {
          data.forEach((item) => {
            Qnt += Number(item.quantity);
          });
          this.updateCartQnt(Qnt);
          this.cookieService.set('Qnt', String(Qnt), {
            expires: 30,
            path: '/',
          });
        }
        this.updateCartQnt(Qnt);
        this.cookieService.set('Qnt', String(Qnt), { expires: 30, path: '/' });
      },
    });
  }

  // #region API Methods

  setToCart(cartId: string, cartItems: any) {
    console.log(this.cartProductSource.getValue());
    return this.http
      .post<any>(`https://localhost:7283/api/Cart/SetCart?cartId=${cartId}`, {
        id: cartId,
        items: cartItems,
      })
      .pipe(
        catchError((error) => {
          console.error('Error setting cart:', error);
          this.toastr.error('Failed', 'Error', {
            positionClass: 'toast-bottom-right',
          });
          return of(null);
        })
      )
      .subscribe((response) => {
        if (response) {
          console.log('Cart updated successfully:', response);
          this.toastr.success('Success', 'Success', {
            positionClass: 'toast-bottom-right',
          });
        }
      });
  }

  getAllFromCart(cartId: string): Observable<any> {
    return this.http
      .get<any>(`https://localhost:7283/api/Cart/GetCartById/${cartId}`, {
        params: { cartId: `${cartId}` },
      })
      .pipe(
        catchError((error) => {
          if (error.status === 400 || error.status === 404) {
            return of(null);
          } else {
            console.error('Error fetching cart data:', error);
            return of(null);
          }
        })
      );
  }

  deleteCart(cartId: string) {
    return this.http
      .delete<any>(`https://localhost:7283/api/Cart/DeleteCart/${cartId}`)
      .pipe(
        catchError((error) => {
          console.error('Error deleting cart:', error);
          this.toastr.error('Failed Deleting Cart', 'Failed Cart', {
            positionClass: 'toast-bottom-right',
          });
          return of(null); // Handle error
        })
      );
  }

  removeFromCart(cartId: string, productId: Number) {
    const currentProducts = this.cartProductSource.getValue();
    console.log('cart for deleting', currentProducts);
    const updatedProducts = currentProducts.filter(
      (item) => item.id !== productId
    );
    this.updateCart(updatedProducts);
    this.setToCart(cartId, updatedProducts);
  }

  removeCart(cartId: string) {
    this.deleteCart(cartId).subscribe({
      next: (response) => {
        this.cartProductSource.next([]);
        this.cookieService.delete('Qnt', '/');
        this.updateCartQnt(0);
        console.log('is/Empty');
        this.toastr.success('Success', 'Success', {
          positionClass: 'toast-bottom-right',
        });
      },
      error: error =>{
        console.error('Error deleting cart:', error);
          this.toastr.error('Failed Deleting Cart', 'Failed Cart', {
            positionClass: 'toast-bottom-right',
          });
      }
    });
  }

  // #endregion

  calculateSubtotal(cart: Cart) {
    let Total = 0;
    Total = cart.items.reduce(
      (sum, item) => sum + item.price * item.quantity,
      0
    );
  }

  ///////////////////////// Payment Methods /////////////////////////

  createPaymentIntent(cartId: string) {
    return this.http
      .post<Cart>('https://localhost:7283/api/Payments/' + cartId, {})
      .pipe(
        map((cart) => {
          this.basketSource.next(cart);
          this.ClientSecret = cart.clientSecret;
          console.log(cart);
        })
      );
  }
}
