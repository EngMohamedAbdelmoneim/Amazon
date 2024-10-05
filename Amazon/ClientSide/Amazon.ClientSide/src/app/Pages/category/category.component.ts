import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Product } from '../../Models/product';
import { Subscription } from 'rxjs';
import { CategoryService } from '../../Services/category.service';
import { CategoryListComponent } from "../../core/category-list/category-list.component";

@Component({
  selector: 'app-category',
  standalone: true,
  imports: [RouterModule, CommonModule, CategoryListComponent],
  templateUrl: './category.component.html',
  styleUrl: './category.component.css'
})
export class CategoryComponent {
  constructor(public http: HttpClient, public activatedRoute: ActivatedRoute, public categoryService: CategoryService) { }
  parentcategories: Array<any> = [];
  products: Array<Product> = [];
  sub: Subscription | null = null;
  ParentCategoryName: string;
  ngOnInit(): void {
    this.sub = this.activatedRoute.params.subscribe(p => {
      this.ParentCategoryName = p['ParentCategoryName'];
      this.categoryService.getCategoryProducts(p['ParentCategoryName'], p['categoryName']).subscribe({
        next: data => {
          console.log(data);
          this.products = data;
        }
      })
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
