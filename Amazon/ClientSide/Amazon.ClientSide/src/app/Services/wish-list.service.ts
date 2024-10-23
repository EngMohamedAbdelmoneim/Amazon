import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, Observable, of, Subscription } from 'rxjs';
import { WishListItem } from '../Models/wish-list-item';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class WishListService {

  private wishListProductSource = new BehaviorSubject<any[]>([]);
  wishListProduct$ = this.wishListProductSource.asObservable();
 
  sub: Subscription | null = null;

  constructor(private http: HttpClient,public toastr:ToastrService) { }

  updateWishList(wishListProducts: any[]) {
    this.wishListProductSource.next(wishListProducts);
  }


  addTowishList(wishListId: string, wishListItem: any) {
    this.sub = this.getAllFromWishList(wishListId).subscribe({
      next: data => {
        if (data != null) {
          console.log(data.items)
          const updatedProducts = [...data.items, wishListItem];
          console.log("prudects :", updatedProducts)
          this.updateWishList(updatedProducts);
          this.setToWishList(wishListId, updatedProducts);
        }
        else {
          this.setToWishList(wishListId, [wishListItem]);
        }
      }
    });
  }
  updateWishListWithItem(wishListId: string, newItem: WishListItem): void {
    this.getAllFromWishList(wishListId).subscribe(wishListData => {
      if (wishListData != null) {
        let wishList = wishListData.items;

        const existingItemIndex = wishList.findIndex(item => item.id === newItem.id);

        if (existingItemIndex !== -1) {
          console.log("old qnt:", wishList[existingItemIndex].quantity);
          console.log("new qnt:", newItem.quantity);
          if ((wishList[existingItemIndex].quantity) + Number(newItem.quantity) <= 10) {
            wishList[existingItemIndex].quantity += Number(newItem.quantity);
          }
          else {
            wishList[existingItemIndex].quantity = 10;
          }
          console.log(`Item exists, new quantity: ${wishList[existingItemIndex].quantity}`);
        } else {
          wishList.push(newItem);
          console.log(`Item added to the wishList: ${newItem.productName}`);
        }
        this.updateWishList(wishList);
        this.setToWishList(wishListId, wishList);
      }
      else {
        this.addTowishList(wishListId, newItem);
      }
    });
  }


  ///////////////////////////// API Methods ////////////////////////////////////////////

  setToWishList(wishListId: string, wishListItems: any) {
    console.log(this.wishListProductSource.getValue());
    return this.http.post<any>(`https://localhost:7283/api/Wishlist/SetWishlist?wishListId=${wishListId}`, { id: wishListId, items: wishListItems })
      .pipe(
        catchError((error) => {
          console.error('Error setting wishList:', error);
          this.toastr.error("Failed", "Error", {positionClass:'toast-bottom-right'})
          return of(null);
        })
      ).subscribe(response => {
        if (response) {
          this.toastr.success("Success", "Success", {positionClass:'toast-bottom-right'})
          console.log('wishList updated successfully:', response);
        }
      });
  }

  getAllFromWishList(wishListId: string): Observable<any> {
    return this.http.get<any>(`https://localhost:7283/api/Wishlist/GetWishlistById/${wishListId}`, { params: { wishListId: `${wishListId}` } })
      .pipe(
        catchError((error) => {
          if (error.status === 400 || error.status === 404) {
            return of(null);
          } else {
            console.error('Error fetching wishList data:', error);
            return of(null);
          }
        })
      );
  }

  deleteWishList(wishListId: string) {
    return this.http.delete<any>(`https://localhost:7283/api/Wishlist/DeletewishList/${wishListId}`)
      .pipe(
        catchError((error) => {
          console.error('Error deleting wishList:', error);
          return of(null); // Handle error
        })
      );
  }

  removeFromWishList(wishListId: string, productId: Number) {
    const currentProducts = this.wishListProductSource.getValue();
    console.log("wishList for deleting", currentProducts)
    if (currentProducts.length > 1) {
      const updatedProducts = currentProducts.filter(item => item.id !== productId);
      this.updateWishList(updatedProducts);
      this.setToWishList(wishListId, updatedProducts);
    }
    else {
      this.deleteWishList(wishListId).subscribe(p => { });
      const updatedProducts = [];
      this.updateWishList(updatedProducts);
      console.log("is/Empty");
    }
  }
}
