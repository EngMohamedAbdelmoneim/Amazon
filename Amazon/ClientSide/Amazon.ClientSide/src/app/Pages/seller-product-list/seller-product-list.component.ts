import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../Services/product.service';
import { Product } from '../../Models/product';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReviewService } from '../../Services/review.service';
import { Review } from '../../Models/review';
import { Subscription } from 'rxjs';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-admin-product-list',
  standalone: true,
  imports: [RouterModule, CommonModule,ReactiveFormsModule],
  templateUrl: './seller-product-list.component.html',
  styleUrl: './seller-product-list.component.css'
})
export class SellerProductListComponent implements OnInit {
  products: Product[] = []; // Initialized as an empty array
  isAuthenticated: boolean = false; // Initialized to false
  userName: string | null = null; // Initialized to null
  selectedProductName: string | null = null; 
  selectedProductId: number | null = null;
  sub:Subscription | null = null;
  constructor(
    private router: Router,
    private productService: ProductService,
    private reviewService: ReviewService
  ) {}

  ngOnInit(): void {
    const isAuthenticated = localStorage.getItem('isAuthenticated');
    this.isAuthenticated = isAuthenticated ? JSON.parse(isAuthenticated) : false; // Handle potential null
    this.userName = localStorage.getItem('userName');

     this.loadSellerProducts(); // Method to load products
  }

  loadSellerProducts(): void {
    this.sub = this.productService.GetSellerProducts().subscribe({
      next: (data: Product[]) => {
        this.products = data; // Assuming data is of type Product[]
      },
      error: (error) => {
        console.error('Error loading seller products:', error);
      }
    });
  }
  add(product: Product): void {
    const serializedObject = encodeURIComponent(JSON.stringify(product));
    this.router.navigate(['/seller/edit-product', serializedObject]);
  }
  edit(product: Product): void {
    const serializedObject = encodeURIComponent(JSON.stringify(product));
    this.router.navigate(['/seller/edit-product', serializedObject]);
  }
  selectProductName(Name: string) {
    this.selectedProductName = Name;
  }
  selectProduct(id: number) {
    this.selectedProductId = id;
  }
  
  deleteProduct(id: number): void {
    this.productService.DeleteProduct(id).subscribe({
      next: () => {
        this.products = this.products.filter(product => product.id !== id);
      },
      error: (error) => {
        console.error('Error deleting product:', error);
      }
    });
  }

  signOut(): void {
    localStorage.clear();
    this.router.navigate(['/login']); // Redirect to login using the router
  }
}