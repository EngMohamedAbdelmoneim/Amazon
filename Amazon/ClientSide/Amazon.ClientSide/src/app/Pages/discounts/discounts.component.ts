import { Component, OnInit } from '@angular/core';
import { ProductCardComponent } from '../../Components/product-card/product-card.component';
import { Product } from '../../Models/product';
import { CartService } from '../../Services/cart.service';
import { CartItem } from '../../Models/cart-item';
import { GuidService } from '../../Services/guid.service';
import { Subscription } from 'rxjs';
import { ProductService } from '../../Services/product.service';

@Component({
  selector: 'app-discounts',
  standalone: true,
  imports: [ProductCardComponent],
  templateUrl: './discounts.component.html',
  styleUrl: './discounts.component.css'
})

export class DiscountsComponent implements OnInit
{

  constructor(private cartService: CartService, private guidService: GuidService, private productService: ProductService) {}

  products: Array<Product> = [];
  productSub: Subscription;

  ngOnInit(): void
  {
    this.productSub = this.productService.getProducts().subscribe({
      next: data => {
        console.log(data);
        // this.products = data;
        this.products = data.filter(d => d.discount != null);
        // this.products = data.filter(d => d.discount.discountStarted != false);
      }
    })
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
