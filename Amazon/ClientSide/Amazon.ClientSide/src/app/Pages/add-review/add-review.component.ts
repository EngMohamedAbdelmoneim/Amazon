import { Component, Input, input, OnInit } from '@angular/core';
import { Review } from '../../Models/review';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ReviewService } from '../../Services/review.service';
import { Subscription } from 'rxjs';
import { StarRatingComponent } from "../../Components/star-rating/star-rating.component";

@Component({
  selector: 'app-add-review',
  standalone: true,
  imports: [RouterModule, CommonModule, FormsModule, StarRatingComponent],
  templateUrl: './add-review.component.html',
  styleUrls: ['./add-review.component.css']
})
export class AddReviewComponent implements OnInit {
  review :Review =  new Review(0,0,'','',null,0,'','');
  sub: Subscription | null = null;
  productName:string;
  productRating: number = 0; // Initial rating

  constructor(public route: ActivatedRoute, public reviewService: ReviewService , private router: Router) { }
  ngOnInit(): void {
    this.sub = this.route.params.subscribe(params => {
      this.productName= params['pname'];
      if(params['edit']){
        this.review = JSON.parse(decodeURIComponent(params['edit']));
        console.log('this.review:', this.review);
        console.log('this.review:', params['edit']);
      }
    })
  } 
  
  onRatingChange(newRating: number): void {
    console.log('New rating:', newRating);
    this.productRating = newRating;
  }

  submitReview() {
    console.log('Review submitted:', this.review);
    this.sub = this.route.params.subscribe(params => {
      this.review.productId= params['id'];
      this.review.reviewDate = new Date();
      this.review.rating = this.productRating;
      console.log(this.review);
      if(params['edit']){
        this.reviewService.updateReview(this.review.id,this.review).subscribe({
          next: data =>{
            console.log(data);
            this.router.navigate(['/product', params['id']])
          }
        })
      }
      else{
        this.reviewService.addReview(this.review).subscribe({
        next: data =>{
          console.log(data);
          this.router.navigate(['/product', params['id']])
        }
      })
      }
  })
  }


  
}
