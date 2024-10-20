import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { WishListService } from '../../Services/wish-list.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ReviewService } from '../../Services/review.service';

@Component({
  selector: 'app-wishlist-card',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './wish-list-card.component.html',
  styleUrl: './wish-list-card.component.css'
})
export class WishlistCardComponent implements OnInit {
  constructor(private reviewService: ReviewService) { }
  ngOnInit(){
    this.AverageRating();
    this.NumberOfReviews();
  }
  @Input({ required: true })
  prop: {
    id: number;
    productName: string;
    category: string;
    brand: string;
    price: Number;
    pictureUrl: string;
    quantity: Number;
  }
  @Output() itemListDeleted = new EventEmitter<void>();
  @Output() addToCart = new EventEmitter<void>();
  avgRatiing: number;
  numberOfReviews: number;

  Delete() {
    this.itemListDeleted.emit();
    console.log("on item deleting");
  }
  AddToCart() {
    this.addToCart.emit();
    console.log("on item adding");
  }

  FormatDate(): string {
    const addedTime: Date = new Date();
    const options: Intl.DateTimeFormatOptions = { day: 'numeric', month: 'long', year: 'numeric' };
    return new Intl.DateTimeFormat('en-GB', options).format(addedTime);
  }

  GetRatingArray(rating: number): boolean[] {
    console.log('kkkkkkkkkkkkkkkkkkkkkkkkkk',rating);
    const maxStars = 5; // Total number of stars
    return Array.from({ length: maxStars }, (_, index) => index < rating);
  }

  AverageRating() {
    let fullRate = 0;
    this.reviewService.getAllProductReviewsById(this.prop.id).subscribe({
      next: async data => {
        data.forEach(rev => {
          fullRate += rev.rating;
        });
        this.avgRatiing = fullRate / 5;
      }
    })
  }
  NumberOfReviews() {
    this.reviewService.getAllProductReviewsById(this.prop.id).subscribe({
      next: data =>{
       this.numberOfReviews = data.length;
      }
    })
  }
}
