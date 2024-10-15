import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { CartCardComponent } from "../../Components/cart-card/cart-card.component";
import { delay, Subscription } from 'rxjs';
import { CartService } from '../../Services/cart.service';
import { CartItem } from '../../Models/cart-item';
import { GuidService } from '../../Services/guid.service';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [RouterModule, CommonModule, CartCardComponent],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})

export class CartComponent implements OnInit {

  cartItems: any | null;
  Qnt:number=0;
  sub: Subscription | null = null;
  loading: boolean = true;
  constructor(public http: HttpClient, public activatedRoute: ActivatedRoute, private cartService: CartService , public guidServices: GuidService ,public cookieService:CookieService) { }


  ngOnInit(): void {
    this.cartService.cartQnt.subscribe({
      next: p => { this.Qnt = p; }
    });
    if (this.cookieService.get('Qnt') != null) {
      this.Qnt = Number(this.cookieService.get('Qnt'));
    }
    else{
      this.Qnt = 0;
    }

    this.cartService.cartProduct$
      .pipe(delay(100))
      .subscribe({
        next: products => {
          if (products.length !== 0) {
            console.log('from list');
            this.cartItems = products; 
            console.log('Updated cartProducts:', this.cartItems);
            this.loading = false;
            console.log('Cart data loaded:', products);
          }
          else {
            console.log('from database');
            this.sub = this.activatedRoute.
              params.subscribe(params => {
                this.loading = true;
                this.cartService.getAllFromCart(params['cartId'])
                  .subscribe({
                    next: cart => {
                      if (cart != null) {
                        this.cartItems = cart.items;
                        this.cartService.updateCart(cart.items);
                        console.log('Updated cartProducts:', this.cartItems);
                        this.loading = false;
                        console.log('Cart data loaded:', cart);
                      }
                      else {
                        this.cartItems = null;
                        this.loading = false;
                      }
                    },
                    error: err => {
                      this.loading = false;
                      console.error('Error fetching cart data:', err);
                    }
                  });
              });
          }
        },
        error: err => {
          this.loading = false;
          console.error('Error fetching cart data:', err);
        }
      });
  }

  ngOnDestroy(): void {
    if (this.sub) {
      this.sub.unsubscribe();
    }
  }
  getGuid():string{
    return this.guidServices.getGUID();
  }

  RemoveFromCart(cartId: string, cartItamId: number): void {
    this.cartService.removeFromCart(cartId, cartItamId);
    this.loading = false;
  }
  UpdateQnt(cartId: string, item: CartItem): void {
    console.log(item.quantity)
    this.cartService.updateCartItemQnt(cartId,item);
    this.loading = false;
  }

  TotalPrice(){
    let TotalPrice = 0;
    if(this.cartItems != null){
      this.cartItems.forEach(item => {
        TotalPrice += Number(item.price * item.quantity); 
      });
      return TotalPrice.toFixed(2);
    }
    return TotalPrice.toFixed(2);
  }

}