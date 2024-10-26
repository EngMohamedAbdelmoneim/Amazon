import { Component, OnInit, OnDestroy } from '@angular/core';
import { Product } from '../../Models/product';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../Services/product.service';
import { CommonModule } from '@angular/common';
import { ReviewService } from '../../Services/review.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-seller-product-details',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './seller-product-details.component.html',
  styleUrls: ['./seller-product-details.component.css']
})
export class SellerProductDetailsComponent implements OnInit, OnDestroy {
  product: Product | null = null;  // Holds product details
  productReviews: any[] = [];  // Holds product reviews
  sub: Subscription | null = null;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private productService: ProductService,
    private reviewService: ReviewService,
  ) {}

  async ngOnInit(): Promise<void> {
    const id = +this.route.snapshot.paramMap.get('id')!;
    await this.loadProductDetails(id);
    await this.loadProductReviews(id);
  }

  private async loadProductDetails(id: number): Promise<void> {
    this.sub = this.productService.getProductById(id).subscribe({
      next: (product) => this.product = product,
      error: (err) => console.error('Error loading product details:', err)
    });
  }

  private async loadProductReviews(id: number): Promise<void> {
    this.sub = this.reviewService.getAllProductReviewsById(id).subscribe({
      next: (reviews) => this.productReviews = reviews,
      error: (err) => console.error('Error loading reviews:', err)
    });
  }

  // Calculates the average rating
  get averageRating(): number {
    if (!this.productReviews || this.productReviews.length === 0) {
      return 0;
    }
    const totalRating = this.productReviews.reduce((sum, review) => sum + review.rating, 0);
    return totalRating / this.productReviews.length;
  }

  goBack(): void {
    this.router.navigate(['seller/product-list']);
  }

  ngOnDestroy(): void {
    this.sub?.unsubscribe();
  }
}
