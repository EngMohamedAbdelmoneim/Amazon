import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Review } from '../Models/review';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {


  constructor(private http: HttpClient) {}

  getAllReviews(): Observable<Review[]> {
    return this.http.get<Review[]>(`https://localhost:7283/api/Review/GetAllReviews`);
  }
  getAllProductReviewsById(productId:number): Observable<Review[]> {
    return this.http.get<Review[]>(`https://localhost:7283/api/Review/GetAllProductReviewsById/${productId}`);
  }

  // Get review by id
  getReviewById(id: number): Observable<Review> {
    return this.http.get<Review>(`https://localhost:7283/api/Review/GetReviewById/${id}`);
  }

  // Add a new review
  addReview(reviewDto: Review): Observable<Review> {
    return this.http.post<Review>(`https://localhost:7283/api/Review/AddReview`, reviewDto);
  }
  
  // Update an existing review
  updateReview(id: number, reviewDto: Review): Observable<Review> {
    return this.http.put<Review>(`https://localhost:7283/api/Review/UpdateReview/${id}`, reviewDto);
  }

  // Delete a review
  deleteReview(id: number): Observable<Review[]> {
    return this.http.delete<Review[]>(`https://localhost:7283/api/Review/DeleteReview/${id}`);
  }
}
