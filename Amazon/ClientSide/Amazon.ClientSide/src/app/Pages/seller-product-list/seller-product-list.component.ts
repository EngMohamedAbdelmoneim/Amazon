import { Component } from '@angular/core';
import { ProductService } from '../../Services/product.service';
import { Product } from '../../Models/product';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReviewService } from '../../Services/review.service';
import { Review } from '../../Models/review';

@Component({
  selector: 'app-admin-product-list',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './seller-product-list.component.html',
  styleUrl: './seller-product-list.component.css'
})
export class SellerProductListComponent {

  products: Product[] | null = [];
  constructor(public router: Router, private productService: ProductService, public reviewService: ReviewService) { }

  ngOnInit(): void {
    this.productService.GetSellerProducts().subscribe({
      next: data => {
        this.products = data;
      }
    });
  }

  edit(product: Product) {
    const serializedObject = encodeURIComponent(JSON.stringify(product));
    this.router.navigate(['/seller/edit-product', serializedObject]);
  }

  delete(id: number): void {
    this.productService.DeleteProduct(id).subscribe({
      next: data => {
        this.productService.GetSellerProducts().subscribe({
          next: data => {
            this.products = data;
          }
        });
      }
    });
  }

}
