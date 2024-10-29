import { Component } from '@angular/core';
import { Product } from '../../Models/product';
import { Subscription } from 'rxjs';
import { CartService } from '../../Services/cart.service';
import { GuidService } from '../../Services/guid.service';
import { SearchService } from '../../Services/search.service';
import { ActivatedRoute } from '@angular/router';
import { CartItem } from '../../Models/cart-item';
import { ProductCardComponent } from '../../Components/product-card/product-card.component';

@Component({
  selector: 'app-brands',
  standalone: true,
  imports: [ProductCardComponent],
  templateUrl: './brands.component.html',
  styleUrl: './brands.component.css'
})
export class BrandsComponent {

    
  constructor(private cartService: CartService, private guidService: GuidService, private searchService: SearchService, private activatedRoutes: ActivatedRoute) {}

  products: Array<Product> = [];
  productSub: Subscription;

  ngOnInit(): void 
  {
    this.productSub = this.activatedRoutes.params.subscribe(p => {
      this.searchService.SearchByBrand(p['brandId']).subscribe({
        next: data => {
          console.log(data);
          this.products = data.data;
        }
      })
    })




    // this.sub = this.activatedRoute.params.subscribe(p => {
    //   this.SearchService.Search(p['productName'], pageIndex).subscribe({
    //     next: data => {
    //       console.log(data);
    //       this.paginatedProducts = data;
    //       this.products = data.data;
    //       this.orginalProducts = data.data;
    //       window.scrollTo({ top: 0, behavior: 'smooth' });
    //     }
    //   })
    // })
  }




  AddToCart(product: Product, _id: string) {
    const cartitem: CartItem =
    {
      id: product.id,
      productName: product.name,
      category: product.categoryName,
      price: product.price,
      pictureUrl: product.pictureUrl,
      quantity: 1,
    };
    this.cartService.updateCartWithItem(("cart-"+_id),cartitem,product.quantityInStock);
  }
  getGuid(): string {
    return this.guidService.getGUID();
  }


}
