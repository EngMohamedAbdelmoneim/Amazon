import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Product } from '../../Models/product';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-product-card',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './product-card.component.html',
  styleUrl: './product-card.component.css'
})
export class ProductCardComponent {
   @Input({required:true}) product: Product;
   @Output() addToCart = new EventEmitter<void>();
   cardVisible = false;
   ngAfterViewInit() {
    setTimeout(() => {
      this.cardVisible = true;
    }, 200); 
  }
  GetRatingArray(rating: number): boolean[] {
    const maxStars = 5; // Total number of stars
    return Array.from({ length: maxStars }, (_, index) => index < rating);
  }
   AddToCart() {
    this.addToCart.emit();
    console.log("on item adding");
  }
  Discount(){
    console.log(this.product.discount.discountPercentage);
    return Number(this.product.discount.discountPercentage *100);
  }
  DiscountEndDate(){
    let EndDate:any =new Date(this.product.discount.endDate).getTime();
    return EndDate;
  }
  DiscountTimeOut(){
    let StartDate:any =new Date(this.product.discount.startDate).getTime();
    let EndDate:any =new Date(this.product.discount.endDate).getTime();
    let Days = EndDate - StartDate;
    const DaysOut = Math.floor(Days / (1000 * 60 * 60 * 24));
    console.log(DaysOut);
    return DaysOut;
  }
}
