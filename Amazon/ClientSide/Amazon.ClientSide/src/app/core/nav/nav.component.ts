import { CommonModule } from '@angular/common';
import { Component, ElementRef, signal, ViewChild, ViewEncapsulation } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';
import { SearchService } from '../../Services/search.service';
import { CategoryListComponent } from "../category-list/category-list.component";

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [RouterModule, CommonModule, CategoryListComponent],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css',
  encapsulation: ViewEncapsulation.Emulated
})
export class NavComponent {
  constructor(private router: Router, private searchService: SearchService) { }

  @ViewChild('Category') Category: ElementRef;
  query: string;
  open: boolean = false;

  toggleMenu() {
    const body = document.body;

    if (this.open) {
      body.style.position = '';
      body.style.top = '';
      body.style.height = '';
      body.style.overflowY = '';
    }
    else {
      body.style.height = '100vh';
      body.style.overflowY = 'hidden';
    }
    this.open = !this.open;
  }

  search(q: string) {
    let Category = this.Category.nativeElement.value;
    this.searchService.searchType = Category;
    console.log(q);
    this.router.navigateByUrl(`/search/${q}`);
  }

}