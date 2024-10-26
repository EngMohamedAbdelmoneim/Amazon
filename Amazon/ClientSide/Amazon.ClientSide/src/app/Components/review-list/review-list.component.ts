import { Component, EventEmitter, HostListener, Input, OnInit, Output } from '@angular/core';
import { ReviewService } from '../../Services/review.service';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Review } from '../../Models/review';

@Component({
  selector: 'app-review-list',
  standalone: true,
  imports: [CommonModule,RouterModule,FormsModule],
  templateUrl: './review-list.component.html',
  styleUrl: './review-list.component.css'
})
export class ReviewListComponent implements OnInit{
  constructor(public router: Router,public reviewService:ReviewService) { }
  @Input({required:true}) productReviews : any;
  @Input({required:true}) productId : any;
  @Input({required:true}) productName : any;

  @Output() itemDeleted = new EventEmitter<number>();
  userName:string = '';
  isAuthenticated : boolean = false;
  ngOnInit(): void {
    if(localStorage.getItem('isAuthenticated'))
      {
        this.isAuthenticated = true;
        this.userName = localStorage.getItem('userName');
      }
      else{
        this.isAuthenticated = false;
      }
  }
  GetRatingArray(rating: number): boolean[] {
    const maxStars = 5;  
    return Array.from({ length: maxStars }, (_, index) => index < rating);
  }
  ReviewDate(date:Date){
    return new Date(date).getTime();
  }

  navigateWithObject(pid:number,pname:string,rev:Review) {
    const serializedObject = encodeURIComponent(JSON.stringify(rev));

    // Navigate to the route with parameters
    this.router.navigate(['/review', pid, pname, serializedObject]);
  }
  edit() {
    console.log('Edit clicked');
  }

  delete(revId:number) {
    this.itemDeleted.emit(revId);
  }
}
