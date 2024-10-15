import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Console, log } from 'node:console';
import { Product } from '../../Models/product';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Subscription } from 'rxjs';
import { SearchService } from '../../Services/search.service';
import { CommonModule } from '@angular/common';
import { PaginatedProducts } from '../../Models/PaginatedProducts';

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './search.component.html',
  styleUrl: './search.component.css'
})
export class SearchComponent implements OnInit{

  constructor(public http: HttpClient, public activatedRoute: ActivatedRoute, public SearchService: SearchService, public router: RouterModule) { }

  @ViewChild('Category') Category:ElementRef;
  @ViewChild('minValue') minValue:ElementRef;
  @ViewChild('maxValue') maxValue:ElementRef;

  products: Array<Product> = [];
  orginalProducts: Array<Product> = [];
  test: PaginatedProducts;
  sub: Subscription | null = null;
  pageNo: Array<number>;

  ngOnInit(): void
  {
    this.sub = this.activatedRoute.params.subscribe(p => {
      this.SearchService.Search(p['productName']).subscribe({
        next: data => {
          console.log(data);
          this.test = data;
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
    this.sub = this.activatedRoute.params.subscribe(p => {
      this.SearchService.Search(p['productName'], pageIndex).subscribe({
        next: data => {
          console.log(data);
          this.test = data;
          this.products = data.data;
          this.orginalProducts = data.data;
          window.scrollTo({ top: 0, behavior: 'smooth' });
        }
      })
    })
  }

  LowtoHighSort()
  {
    this.products  = this.products.sort((a, b) => a.price - b.price);
  }

  HightoLowSort()
  {
    this.products = this.products.sort((a, b) => b.price - a.price);
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
