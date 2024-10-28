import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { Product } from '../../../../Models/product';
import { Subscription } from 'rxjs';
import { SellerService } from '../../../../Services/seller.service';
import { ReviewService } from '../../../../Services/review.service';

@Component({
  selector: 'app-verified-products',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './verified-products.component.html',
  styleUrl: './verified-products.component.css'
})
export class VerifiedProductsComponent {
  products: Product[] = []; // Initialized as an empty array
  isAuthenticated: boolean = false; // Initialized to false
  userName: string | null = null; // Initialized to null
  selectedProductName: string | null = null;
  selectedProductId: number | null = null;
  sub:Subscription | null = null;
  constructor(
    private router: Router,
    private sellerService: SellerService,
    private reviewService: ReviewService
  ) {}

  ngOnInit(): void {
    const isAuthenticated = localStorage.getItem('isAuthenticated');
    this.isAuthenticated = isAuthenticated ? JSON.parse(isAuthenticated) : false; // Handle potential null
    this.userName = localStorage.getItem('userName');

     this.loadUnVerifiedSellerProducts(); // Method to load products
  }

  loadUnVerifiedSellerProducts(): void {
    this.sub = this.sellerService.GetSellerAcceptedProducts().subscribe({
      next: (data: Product[]) => {
        console.log(data)
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
    this.sellerService.DeleteProduct(id).subscribe({
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
