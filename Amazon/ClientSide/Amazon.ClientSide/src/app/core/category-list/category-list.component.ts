import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../../Services/category.service';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './category-list.component.html',
  styleUrl: './category-list.component.css'
})
export class CategoryListComponent implements OnInit {
  parentcategories: Array<any> = [];
  sub: Subscription | null = null;
  constructor(private router: Router, public categoryService: CategoryService) { }

  ngOnInit() {
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

  SignOut()
  {
    localStorage.clear();
    window.location.href = 'http://localhost:4200/login'
  }
}
