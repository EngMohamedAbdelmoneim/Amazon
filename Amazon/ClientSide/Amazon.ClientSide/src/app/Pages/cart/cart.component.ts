import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { CartCardComponent } from "../../Components/cart-card/cart-card.component";
import { Observable, Subscription } from 'rxjs';
import { CartService } from '../../Services/cart.service';
import { Product } from '../../Models/product';
import { CartItems } from '../../Models/cart-items';
import { start } from 'node:repl';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [RouterModule, CommonModule, CartCardComponent],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})

export class CartComponent implements OnInit {

  cartProduct: any | null;
  sub: Subscription | null = null;
  constructor(public http: HttpClient, public activatedRoute: ActivatedRoute, private cartService: CartService) { }

  ngOnInit() {
    this.sub = this.activatedRoute.params.subscribe(p => {
        this.cartService.getAllFromCart(p['cartId']).subscribe({
          next: async data => {
            this.cartProduct = await data;
            console.log(data);
          }
        })
    })
  }

  AddAndUpdateCart(cartItem: CartItems, _id: string) {
    const cartitem: CartItems =
    {
      id: cartItem.id,
      productName: cartItem.productName,
      category: "1",
      price: cartItem.price,
      pictureUrl: cartItem.pictureUrl,
      quantity: 0,
    };
    console.log(cartItem);
    this.cartService.getAllFromCart(_id).subscribe({
      next: data => {
        
        this.cartProduct = data.items;
        let index = this.cartProduct.findIndex(i => i.id == cartItem.id);
        this.cartProduct.splice(index,1);
        if (this.cartProduct) {
          this.cartService.addToCart(_id, this.cartProduct).subscribe({
            next: addedCart => {
              console.log(addedCart);
              this.sub = this.activatedRoute.params.subscribe(p => {
                this.cartService.getAllFromCart(p['cartId']).subscribe({
                  next: async data => {
                    this.cartProduct = await data;
                    console.log(data);
                  }
                })
            })
            },
            error: err => console.error('Failed to add item to cart:', err)
          });
        
      }
    },
      error: err => {
        console.error('Failed to fetch cart data:', err);
      }
    });
  }

  // async onItemDeleted(cartId: string, id: number) {
  //   this.cartService.deleteCartItem(cartId, id).subscribe({
  //     next: updatedCart => {
  //       this.cartService.cartItems = updatedCart;
  //       console.log('Cart updated after item deletion:', updatedCart);
  //     },
  //     error: err => {
  //       console.error('Error deleting item:', err);
  //     }
  //   });
  // }


}
