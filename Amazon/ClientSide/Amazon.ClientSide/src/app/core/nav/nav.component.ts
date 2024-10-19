import { CommonModule } from '@angular/common';
import { Component, ElementRef, OnInit, signal, ViewChild, ViewEncapsulation } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';
import { SearchService } from '../../Services/search.service';
import { CategoryListComponent } from "../category-list/category-list.component";
import { GuidService } from '../../Services/guid.service';
import { CartService } from '../../Services/cart.service';
import { CookieService } from 'ngx-cookie-service';
import { AccountService } from '../../Services/account.service';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [RouterModule, CommonModule, CategoryListComponent],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css',
  encapsulation: ViewEncapsulation.Emulated
})
export class NavComponent implements OnInit {
  constructor(private router: Router,
    private searchService: SearchService,
    public guidService: GuidService, 
    public cartService: CartService, 
    private cookieService: CookieService,
    private accountService: AccountService
  ) { }

  @ViewChild('Category') Category: ElementRef;
  query: string;
  open: boolean = false;
  cartQnt: number;
  ngOnInit(): void {
    this.cartService.cartQnt.subscribe({
      next: p => { this.cartQnt = p; }
    });
    if (this.cookieService.get('Qnt') != null) {
      this.cartQnt = Number(this.cookieService.get('Qnt'));
      console.log('Retrieved Qnt:', this.cartQnt);
    }
    else {
      this.cartQnt = 0;
      console.log('Retrieved Qnt:', this.cartQnt);
    }

  }
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
    if(q == "")
    {
      return;
    }
    else
    {
      this.router.navigateByUrl(`/search/${q}`);
    }
  }
  getGuid(): string {
    return this.guidService.getGUID();
  }

  OrdersPage()
  {
    if(this.accountService.isAuthenticated == true)
    {
      this.router.navigateByUrl('/order');
    }
    else
    {
      this.router.navigateByUrl('/login');
    }
  }

}