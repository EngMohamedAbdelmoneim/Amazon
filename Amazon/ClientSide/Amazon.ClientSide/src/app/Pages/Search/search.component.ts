import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Console, log } from 'node:console';
import { Product } from '../../Models/product';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Subscription } from 'rxjs';
import { SearchService } from '../../Services/search.service';
import { CommonModule } from '@angular/common';
import { PaginatedProducts } from '../../Models/PaginatedProducts';
import { CartService } from '../../Services/cart.service';
import { ProductCardComponent } from "../../Components/product-card/product-card.component";
import { CartItem } from '../../Models/cart-item';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [FormsModule, RouterModule, CommonModule, ProductCardComponent],
  templateUrl: './search.component.html',
  styleUrl: './search.component.css'
})
export class SearchComponent implements OnInit{

  constructor(public http: HttpClient, public activatedRoute: ActivatedRoute, public SearchService: SearchService,public cartService:CartService, public router: RouterModule) { }

  @ViewChild('Category') Category:ElementRef;
  @ViewChild('minValue') minValue:ElementRef;
  @ViewChild('maxValue') maxValue:ElementRef;

  products: Array<Product> = [];
  orginalProducts: Array<Product> = [];
  paginatedProducts: PaginatedProducts;
  sub: Subscription | null = null;
  pageNo: Array<number>;
  pageIndex: number = 1;
  spec: string;

   ngOnInit(): void
  {
    this.sub = this.activatedRoute.params.subscribe(p => {
      this.SearchService.Search(p['productName']).subscribe({
        next: data => {
          console.log(data);
          this.paginatedProducts = data;
          this.products = data.data;
          this.orginalProducts = data.data;
          this.pageNo = new Array(Math.ceil(data.count / 8));
          console.log(this.pageNo);
        }
      })
    })
  }

  Paginate(pageIndex: number)
  {
    this.pageIndex = pageIndex;
    switch (this.spec)
    {
      case "priceDesc":
        this.HightoLowSort();
        window.scrollTo({ top: 0, behavior: 'smooth' });
        break;
      case "priceAsc":
        this.LowtoHighSort();
        window.scrollTo({ top: 0, behavior: 'smooth' });
        break;
      default:
        this.sub = this.activatedRoute.params.subscribe(p => {
            this.SearchService.Search(p['productName'], pageIndex).subscribe({
              next: data => {
                console.log(data);
                this.paginatedProducts = data;
                this.products = data.data;
                this.orginalProducts = data.data;
                window.scrollTo({ top: 0, behavior: 'smooth' });
              }
            })
          })
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

  LowtoHighSort()
  {
    this.spec = "priceAsc";
    this.sub = this.activatedRoute.params.subscribe(p => {
      this.SearchService.Filter(p['productName'], "priceAsc", this.pageIndex).subscribe({
        next: data => {
          console.log(data);
          this.paginatedProducts = data;
          this.products = data.data;
          this.orginalProducts = data.data;
          this.pageNo = new Array(Math.ceil(data.count / 8));
          console.log(this.pageNo);
        }
      })
    })
  }

  HightoLowSort()
  {
    this.spec = "priceDesc";
    this.sub = this.activatedRoute.params.subscribe(p => {
      this.SearchService.Filter(p['productName'], "priceDesc", this.pageIndex).subscribe({
        next: data => {
          console.log(data);
          this.paginatedProducts = data;
          this.products = data.data;
          this.orginalProducts = data.data;
          this.pageNo = new Array(Math.ceil(data.count / 8));
          console.log(this.pageNo);
        }
      })
    })
  }

  filterProducts()
  {
    let maxValue = this.maxValue.nativeElement.value;
    let minValue = this.minValue.nativeElement.value;

    if(minValue == undefined || minValue == null || minValue == "")
    {
      minValue = 0;
    }
    else
    {
      minValue = parseInt(this.minValue.nativeElement.value);
    }

    if(maxValue == undefined || maxValue == null || maxValue == "")
    {
      maxValue = Number.MAX_VALUE;
    }
    else
    {
      maxValue = parseInt(this.maxValue.nativeElement.value);
    }

    console.log(minValue);
    console.log(maxValue);

    // debugger
    this.products = this.orginalProducts.filter(p => p.price > minValue && p.price < maxValue);

    console.log(this.products);
  }

}
