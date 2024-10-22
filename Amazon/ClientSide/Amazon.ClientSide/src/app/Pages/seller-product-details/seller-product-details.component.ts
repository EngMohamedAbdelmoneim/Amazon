import { Component } from '@angular/core';
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
  styleUrl: './seller-product-details.component.css'
})
export class SellerProductDetailsComponent {
  product: Product | null = null;  // Hold the product details
  productReviews: any[] | null = [];


  sub: Subscription | null = null;
  constructor(
    private router:Router,
    private route: ActivatedRoute,
    private productService: ProductService,
    private reviewService: ReviewService,
  ) {}

  ngOnInit(): void {
    const id = +this.route.snapshot.paramMap.get('id');  
    this.sub = this.reviewService.getAllProductReviewsById(id).subscribe({
      next: data => {
        this.productReviews =data;
        console.log(data);
      }
    })
    this.AverageRating()
    this.getProductDetails(id); 
  }

  getProductDetails(id: number): void {
    this.productService.getProductById(id).subscribe({
      next: p =>{
        this.product = p;
      }
    });
  }
  AverageRating() {
    let fullRate = 0;
    
     this.productReviews.forEach(rev => {
      fullRate += rev.rating;
    });
    return fullRate / 5;
  }
  
  goBack()
  {
    this.router.navigate(['seller/product-list'])
  }
}
