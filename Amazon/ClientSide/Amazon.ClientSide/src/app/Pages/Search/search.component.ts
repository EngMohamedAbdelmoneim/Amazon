import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { log } from 'node:console';
import { Product } from '../../Models/product';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Subscription } from 'rxjs';
import { SearchService } from '../../Services/search.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './search.component.html',
  styleUrl: './search.component.css'
})
export class SearchComponent implements OnInit{

  constructor(public http: HttpClient, public activatedRoute: ActivatedRoute, public SearchService: SearchService) { }

  @ViewChild('Category') Category:ElementRef;
  products: Array<Product> = [];
  sub: Subscription | null = null;

  ngOnInit(): void
  {
    this.sub = this.activatedRoute.params.subscribe(p => {
      this.SearchService.Search(p['productName']).subscribe({
        next: data => {
          console.log(data);
          this.products = data;
        }
      })
    })
  }


  LowtoHighSort()
  {
    let p = this.products.sort((a, b) => a.price - b.price);
    console.log(p);
  }

  HightoLowSort()
  {
    let p = this.products.sort((a, b) => b.price - a.price);
    console.log(p); 
  }

  filterProducts()
  {
    let minValue = (<HTMLInputElement>document.getElementById("minValue")).value;
    let maxValue = (<HTMLInputElement>document.getElementById("maxValue")).value;

    console.log(minValue);
    console.log(maxValue);
    
    let p = this.products.filter(p => p.price > parseInt(minValue) && p.price < parseInt(maxValue));
    console.table(p);
  }

}
