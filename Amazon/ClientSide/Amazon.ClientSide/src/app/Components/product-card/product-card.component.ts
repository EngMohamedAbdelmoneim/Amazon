import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Product } from '../../Models/product';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ReviewService } from '../../Services/review.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-product-card',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './product-card.component.html',
  styleUrl: './product-card.component.css'
})
export class ProductCardComponent implements OnInit{
   @Input() product: Product;
   @Output() addToCart = new EventEmitter<void>();
   avgRatiing: number;
   numberOfReviews: number;
   cardVisible = false;
   constructor(public reviewService:ReviewService,private toastr: ToastrService){}
   ngOnInit() {
    this.NumberOfReviews() ;
    this.AverageRating() ;
    if(this.product.discount != null  && this.IsDiscountEnded()){
      this.product.discount.discountStarted = false;
    }
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
  CurrentDate(){
    let EndDate:any =new Date().getTime();
    return EndDate;
  }
  DiscountEndDate(){
    let EndDate:any =new Date(this.product.discount.endDate).getTime();
    return EndDate;
  }
  DiscountTimeOut(){
    let TodeyDate:any =new Date().getTime();
    let EndDate:any =new Date(this.product.discount.endDate).getTime();
    let Days = EndDate - TodeyDate;
    const DaysOut = Math.floor(Days / (1000 * 60 * 60 * 24));
    const HoursOut = Math.floor((Days % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    return DaysOut + " Days - " + HoursOut + " Hours";
  }

  IsDiscountEnded(){
    let TodeyDate:any =new Date().getTime();
    let EndDate:any =new Date(this.product.discount.endDate).getTime();
    let Days = EndDate - TodeyDate;
    const HoursOut = Math.floor((Days % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    return HoursOut<0;
  }
  AverageRating() {
    let fullRate = 0;
    this.reviewService.getAllProductReviewsById(this.product.id).subscribe({
      next: async data => {
        data.forEach(rev => {
          fullRate += rev.rating;
        });
        this.avgRatiing = fullRate / data.length;
      }
    })
  }
  NumberOfReviews() {
    this.reviewService.getAllProductReviewsById(this.product.id).subscribe({
      next: data =>{
       this.numberOfReviews = data.length;
      }
    })
  }

}
