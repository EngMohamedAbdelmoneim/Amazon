import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { WishlistCardComponent } from "../../Components/wish-list-card/wish-list-card.component";
import { delay, Subscription } from 'rxjs';
import { WishListService } from '../../Services/wish-list.service';
import { CookieService } from 'ngx-cookie-service';
import { GuidService } from '../../Services/guid.service';
import { CommonModule } from '@angular/common';
import { WishListItem } from '../../Models/wish-list-item';
import { Product } from '../../Models/product';
import { CartItem } from '../../Models/cart-item';
import { CartService } from '../../Services/cart.service';

@Component({
  selector: 'app-wish-list',
  standalone: true,
  imports: [WishlistCardComponent, RouterModule, CommonModule,],
  templateUrl: './wish-list.component.html',
  styleUrl: './wish-list.component.css'
})
export class WishListComponent {
  wishListItems: any | null;
  filteredProducts: any | null;
  Qnt: number = 0;
  sub: Subscription | null = null;
  loading: boolean = true;
  isAuthenticated:boolean;
  constructor(public http: HttpClient, public activatedRoute: ActivatedRoute, private wishListService: WishListService, private cartService: CartService, public guidServices: GuidService, public cookieService: CookieService) { }


  ngOnInit(): void {
    if(localStorage.getItem('isAuthenticated')){
      this.isAuthenticated = true;
    }
    else{
      this.isAuthenticated = false;
    }
    if (this.cookieService.get('Qnt') != null) {
      this.Qnt = Number(this.cookieService.get('Qnt'));
    }
    else {
      this.Qnt = 0;
    }

    this.wishListService.wishListProduct$
      .pipe(delay(100))
      .subscribe({
        next: products => {

          if (products.length !== 0) {
            console.log('from list');
            this.wishListItems = products;
            console.log('Updated wishListProducts:', this.wishListItems);
            this.loading = false;
            console.log('wishList data loaded:', products);
          }
          else {
            console.log('from database');
            this.sub = this.activatedRoute.
              params.subscribe(params => {
                this.loading = true;
                this.wishListService.getAllFromWishList(params['wishlistId'])
                  .subscribe({
                    next: wishList => {
                      if (wishList != null) {
                        this.wishListItems = wishList.items;
                        this.wishListService.updateWishList(wishList.items);
                        console.log('Updated wishListProducts:', this.wishListItems);
                        this.loading = false;
                        console.log('wishList data loaded:', wishList);
                      }
                      else {
                        this.wishListItems = null;
                        this.loading = false;
                      }
                    },
                    error: err => {
                      this.loading = false;
                      console.error('Error fetching wishList data:', err);
                    }
                  });
              });
          }
        },
        error: err => {
          this.loading = false;
          console.error('Error fetching wishList data:', err);
        }
      });
  }

  ngOnDestroy(): void {
    if (this.sub) {
      this.sub.unsubscribe();
    }
  }
  getGuid(): string {
    return this.guidServices.getGUID();
  }

  RemoveFromWishList(wishListId: string, wishListItemId: number): void {
    console.log(wishListId, wishListItemId);
    if (this.wishListItems.length > 1) {
      
      this.wishListService.removeFromWishList(wishListId, wishListItemId);
    }
    else{
      this.wishListService.removeWishList(wishListId);
    }
    this.loading = false;
  }
  AddToCart(wishListItem: WishListItem, _id: string) {
    const cartitem: CartItem =
    {
      id: wishListItem.id,
      productName: wishListItem.productName,
      category: wishListItem.category,
      price: wishListItem.price,
      pictureUrl: wishListItem.pictureUrl,
      quantity: 1,
    };
    this.cartService.updateCartWithItem(("cart-"+_id),cartitem);
  }

  searchArray(event): void {
    const inputValue = event.target.value
    const lowerCaseQuery = inputValue.toLowerCase();
    const searchResults = document.getElementById('no-result');
    if (!searchResults) return;
    if (lowerCaseQuery != '') {
      this.wishListService.wishListProduct$.subscribe({
        next: data => {
          this.filteredProducts = data.filter(item =>
            item.productName.toLowerCase().includes(lowerCaseQuery) ||
            item.category.toLowerCase().includes(lowerCaseQuery) ||
            item.brand.toLowerCase().includes(lowerCaseQuery)
          );
          if (this.filteredProducts.length == 0 || this.filteredProducts  == null) {
            searchResults.innerHTML = `
                    <div class="wishList-section alert alert-danger">
                      <div class="wishList-info">
                        <img src="https://m.media-amazon.com/images/G/01/wishlist/list_icon._CB458180717_.png"
                           style="width: 100px; height: 100px;" alt="Coffee Image">
                        <div class="wishList-details">
                        <h1>No Match for your Search</h1>
                         <a href="#" class="btn btn-light rounded-pill">Shop today's deals</a>
                    </div>
                </div>
                `;
              this.filteredProducts = null;
          }
          else{
            searchResults.innerHTML = ``;
          }
        }
      });
    }
    else {
      this.wishListService.wishListProduct$.subscribe({
        next: data => {
          this.filteredProducts = data;
          
        }
      });
      searchResults.innerHTML = ``;
    }
  }
}
