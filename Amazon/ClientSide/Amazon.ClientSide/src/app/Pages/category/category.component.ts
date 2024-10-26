import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Product } from '../../Models/product';
import { Subscription } from 'rxjs';
import { CategoryService } from '../../Services/category.service';
import { CategoryListComponent } from "../../core/category-list/category-list.component";
import { CartService } from '../../Services/cart.service';
import { CartItem } from '../../Models/cart-item';
import { CartCardComponent } from "../../Components/cart-card/cart-card.component";
import { ProductCardComponent } from "../../Components/product-card/product-card.component";
import { GuidService } from '../../Services/guid.service';

@Component({
  selector: 'app-category',
  standalone: true,
  imports: [RouterModule, CommonModule, CategoryListComponent, CartCardComponent, ProductCardComponent],
  templateUrl: './category.component.html',
  styleUrl: './category.component.css'
})
export class CategoryComponent {
  constructor(public http: HttpClient, public activatedRoute: ActivatedRoute,public guidServices:GuidService ,public categoryService: CategoryService,public cartService:CartService) { }
  parentcategories: Array<any> = [];
  products: Array<Product> = [];
  sub: Subscription | null = null;
  
  ParentCategoryName: string;
  ngOnInit(): void {
    this.sub = this.activatedRoute.params.subscribe(p => {
      if (p['categoryName']!=null) {
        this.ParentCategoryName = p['ParentCategoryName'];
        this.categoryService.getCategoryProducts(p['ParentCategoryName'], p['categoryName']).subscribe({
          next: data => {
            console.log(data);
            this.products = data;
            console.log(this.products);

          }
        })
      }
      else {
        this.ParentCategoryName = p['ParentCategoryName'];
        this.categoryService.getParentCategoryProducts(p['ParentCategoryName']).subscribe({
          next: data => {
            console.log(data);
            this.products = data;
          }
        })
      }
    })
    try {
      this.sub = this.categoryService.getParentCategories().subscribe({
        next: data => {
          this.parentcategories = data;
          console.log(this.parentcategories);
        }
      });
    } catch (error) {
      console.error(error);
    }

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
    this.cartService.updateCartWithItem(("cart-"+_id),cartitem);
  }
  getGuid(): string {
    return this.guidServices.getGUID();
  }

  LowtoHighSort() {
    let p = this.products.sort((a, b) => a.price - b.price);
    console.log(p);
  }

  HightoLowSort() {
    let p = this.products.sort((a, b) => b.price - a.price);
    console.log(p);
  }

  filterProducts() {
    let minValue = (<HTMLInputElement>document.getElementById("minValue")).value;
    let maxValue = (<HTMLInputElement>document.getElementById("maxValue")).value;

    console.log(minValue);
    console.log(maxValue);

    let p = this.products.filter(p => p.price > parseInt(minValue) && p.price < parseInt(maxValue));
    console.table(p);
  }
}
