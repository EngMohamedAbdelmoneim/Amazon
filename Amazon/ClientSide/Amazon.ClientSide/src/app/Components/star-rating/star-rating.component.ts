import { CommonModule } from '@angular/common';
import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-star-rating',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './star-rating.component.html',
  styleUrls: ['./star-rating.component.css']
})
export class StarRatingComponent {
  @Input() rating: number = 0; 
  @Input() maxRating: number = 5; 
  @Output() ratingChange = new EventEmitter<number>(); 

  rate(rating: number): void {
    this.rating = rating;
    this.ratingChange.emit(this.rating);
  }

  isFilled(star: number): boolean {
    return star <= this.rating;
  }
}
