import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { Subscription } from 'rxjs';
import { SellerService } from '../../../../Services/seller.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-seller-products-earnings-details',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './seller-products-earnings-details.component.html',
  styleUrl: './seller-products-earnings-details.component.css'
})
export class SellerProductsEarningsDetailsComponent {
  productsDetails: any[] = []; // Initialized as an empty array
  isAuthenticated: boolean = false; // Initialized to false
  userName: string | null = null; // Initialized to null
  selectedProductName: string | null = null;
  selectedProductId: number | null = null;
  sub:Subscription | null = null;
  constructor(
    private router: Router,
    private sellerService: SellerService,
  ) {}

  ngOnInit(): void {
    const isAuthenticated = localStorage.getItem('isAuthenticated');
    this.isAuthenticated = isAuthenticated ? JSON.parse(isAuthenticated) : false; // Handle potential null
    this.userName = localStorage.getItem('userName');

     this.loadUnVerifiedSellerProducts(); // Method to load products
  }

  loadUnVerifiedSellerProducts(): void {
    this.sub = this.sellerService.GetAllSellerEarningsWithDetails().subscribe({
      next: (data) => {
        console.log(data)
        this.productsDetails = data; // Assuming data is of type Product[]
      },
      error: (error) => {
        console.error('Error loading seller products:', error);
      }
    });
  }
}
