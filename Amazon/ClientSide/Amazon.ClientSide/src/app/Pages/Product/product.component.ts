import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ProductService } from '../../Services/product.service';
import { Product } from '../../Models/product';
import { CommonModule, DatePipe } from '@angular/common';
import { Subscription } from 'rxjs';
import { CartService } from '../../Services/cart.service';
import { CartItem } from '../../Models/cart-item';
import { GuidService } from '../../Services/guid.service';
import { WishListService } from '../../Services/wish-list.service';
import { WishListItem } from '../../Models/wish-list-item';
import { ReviewListComponent } from "../../Components/review-list/review-list.component";
import { ReviewService } from '../../Services/review.service';
import { ToastrService } from 'ngx-toastr'
import { Review } from '../../Models/review';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [CommonModule, RouterModule, ReviewListComponent],
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  userName: string | null = null
  isAuthenticated:boolean;
  reviewed: boolean = false;
  reviewedText:string = "Write a customer review"
  productImages: any;
  product: Product | null = new Product(0, "", 0, "", "", [], 0, "", 0, "", 0, null);
  cartItems: CartItem | null = new CartItem(0, "", "", 0, "", 0);
  cart: Array<CartItem> | null;
  productReviews: any[] = [];
  @ViewChild('quantity') selectedQtn: ElementRef;
  errorMessage: string | null = null;
  selectedColorName: string | null = null;
  selectedStar: number | null = null;
  ratingMessage: String | null = null;
  availableColors: string[] = ['#ffffff', '#ac9a9a', '#36525f', '#124055', '#000000'];
  hoveredStar: number | null = null;

  avgRatiing: number | 0 = 0 ;
  ratings = [
    { stars: 5, rating: 0 },
    { stars: 4, rating: 0 },
    { stars: 3, rating: 0 },
    { stars: 2, rating: 0 },
    { stars: 1, rating: 0 },
  ];

  sub: Subscription | null = null;
  subReviews: Subscription | null = null;

  constructor(public productService: ProductService, public reviewService: ReviewService,
    public cartService: CartService,
    public wishListService: WishListService,
    public route: ActivatedRoute,
    public guidServices: GuidService,
    public toastr: ToastrService,
    private router: Router
  ) { }

  ngOnInit(): void {
    if(localStorage.getItem('isAuthenticated'))
      {
        this.isAuthenticated = true;
      }
      else{
        this.isAuthenticated = false;
      }
    this.sub = this.route.params.subscribe(p => {
      this.productService.getProductById(p['id']).subscribe({
        next: async data => {
          this.product = await data;
          this.productImages = [data.pictureUrl, ...data.productImages];

          this.fetchReviews();

          if (this.product.discount != null && this.IsDiscountEnded()) {
            this.product.discount.discountStarted = false;
          }
  
        },
        error: err => {
          console.error('Sorry, we couldn\'t fetch the data', err);
          this.errorMessage = 'Sorry';
        }
      });
    });
  }

  private fetchReviews(): void {
    if (this.product && this.product.id) {
      this.subReviews = this.reviewService.getAllProductReviewsById(this.product.id).subscribe({
        next: data => {
          this.productReviews = data;
          this.AverageRating();
          if (this.avgRatiing > 0) {
            this.updateRatings();
          }
          if (this.productReviews && this.isAuthenticated) {
            this.userName = localStorage.getItem('userName');
            let elemet = this.productReviews.find(e => e.appUserName === this.userName);
            if (elemet) {
              this.productReviews = [elemet,...data.filter(elm => elm !== elemet)]
              this.reviewed = true;
              this.reviewedText= "You reviewed"
            }
            else{
              this.reviewed = false;
              this.reviewedText = "Write a customer review"
            }
          }
          else if(this.productReviews && this.isAuthenticated==false){
            this.reviewed = true;
           this.reviewedText= "You Should sing in to add review"
         }
          
        },
        error: err => {
          this.reviewed = true;
          this.reviewedText= "You Should sing in to add review"
          console.error('Sorry, we couldn\'t fetch the reviews', err);
        }
      });
    }
  }

  updateRatings(): void {
    // Clear previous ratings
    this.ratings.forEach(r => r.rating = 0);

    // Calculate ratings based on reviews
    this.ratings[4].rating = Math.round(this.productReviews.filter(rev => rev.rating === 1).length * 100 / this.productReviews.length);
    this.ratings[3].rating = Math.round(this.productReviews.filter(rev => rev.rating === 2).length * 100 / this.productReviews.length);
    this.ratings[2].rating = Math.round(this.productReviews.filter(rev => rev.rating === 3).length * 100 / this.productReviews.length);
    this.ratings[1].rating = Math.round(this.productReviews.filter(rev => rev.rating === 4).length * 100 / this.productReviews.length);
    this.ratings[0].rating = Math.round(this.productReviews.filter(rev => rev.rating === 5).length * 100 / this.productReviews.length);
  }



  Discount() {
    return Number(this.product.discount.discountPercentage * 100);
  }
  DiscountTimeOut() {
    let TodeyDate: any = new Date().getTime();
    let EndDate: any = new Date(this.product.discount.endDate).getTime();
    let Days = EndDate - TodeyDate;
    const DaysOut = Math.floor(Days / (1000 * 60 * 60 * 24));
    const HoursOut = Math.floor((Days % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    return DaysOut + " Days - " + HoursOut + " Hours";
  }

  AddToCart(product: Product, _id: string) {
    const cartitem: CartItem =
    {
      id: product.id,
      productName: product.name,
      category: product.categoryName,
      price: product.price,
      pictureUrl: product.pictureUrl,
      quantity: Number(this.selectedQtn.nativeElement.value),
    };
    this.cartService.updateCartWithItem(("cart-" + _id), cartitem);
    // this.toastr.success("Item Added To Cart", 'Added',{positionClass:'toast-bottom-right'})
  }


  AddToWishList(product: Product, _id: string) {
    const wishListItem: WishListItem =
    {
      id: product.id,
      productName: product.name,
      category: product.categoryName,
      brand: product.brandName,
      price: product.price,
      pictureUrl: product.pictureUrl,
      quantity: Number(this.selectedQtn.nativeElement.value),
    };
    this.wishListService.updateWishListWithItem(("wishlist-" + _id), wishListItem);
  }
  // Function to display side image
  DisplaySideImage(pictureUrl: string): void {
    if (this.product) {
      this.product.pictureUrl = pictureUrl;
    }
  }
  GetRatingArray(rating: number): boolean[] {
    const maxStars = 5;
    return Array.from({ length: maxStars }, (_, index) => index < rating);
  }

  AverageRating() {
    let fullRate = 0;

    this.productReviews.forEach(rev => {
      fullRate += rev.rating;
    });
    this.avgRatiing = fullRate / this.productReviews.length | 0;
  }

  // Function to set the color name
  setColorName(color: string): void {
    this.selectedColorName = color;
  }

  // Function to set the star rating
  setRate(starValue: number): void {
    this.selectedStar = starValue;
  }
  resetRate(): void {
    this.selectedStar = null;

  }

  getGuid(): string {
    return this.guidServices.getGUID();
  }

  submitRate(starValue: number): void {
    this.selectedStar = starValue;
    this.ratingMessage = `Thanks for rating our product: ${starValue} star(s)`;

    setTimeout(() => {
      this.ratingMessage = null;
    }, 2000);
  }
  zoomImage(event: MouseEvent): void {
    const fullImage = document.getElementById('Product-Full-Size') as HTMLImageElement;

    if (fullImage) {
      // const fullImage = fullImageElement.querySelector('img');

      const rectangle = fullImage.getBoundingClientRect();
      const x = (event.clientX - rectangle.left) / rectangle.width * 100;
      const y = (event.clientY - rectangle.top) / rectangle.height * 100;

      fullImage.style.transformOrigin = `${x}% ${y}%`
      fullImage.style.transform = "scale(2)";
    }
  }
  resetImage(): void {
    const image = document.getElementById('Product-Full-Size') as HTMLImageElement;
    if (image) {
      image.style.transform = "scale(1)";
      image.style.transition = "ease(1s)";
    }
  }
  hoverStar(starValue: number): void {
    this.hoveredStar = starValue;
  }
  resetHoverStar(): void {
    this.hoveredStar = null;
  }
  IsDiscountEnded() {
    let TodeyDate: any = new Date().getTime();
    let EndDate: any = new Date(this.product.discount.endDate).getTime();
    let Days = EndDate - TodeyDate;
    const HoursOut = Math.floor((Days % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    return HoursOut < 0;
  }

  CurrentDate() {
    let EndDate: any = new Date().getTime();
    return EndDate;
  }
  DiscountEndDate() {
    let EndDate: any = new Date(this.product.discount.endDate).getTime();
    return EndDate;
  }

  DeleteReveiw(revId: number) {
    this.reviewService.deleteReview(revId).subscribe({
      next: data => {
        this.productReviews = data;
        this.AverageRating();
        if (this.avgRatiing > 0) {
          this.ratings[4].rating = Math.round(this.productReviews.filter(rev => rev.rating == 1).length * 100 / this.productReviews.length) | 0;
          this.ratings[3].rating = Math.round(this.productReviews.filter(rev => rev.rating == 2).length * 100 / this.productReviews.length) | 0;
          this.ratings[2].rating = Math.round(this.productReviews.filter(rev => rev.rating == 3).length * 100 / this.productReviews.length) | 0;
          this.ratings[1].rating = Math.round(this.productReviews.filter(rev => rev.rating == 4).length * 100 / this.productReviews.length) | 0;
          this.ratings[0].rating = Math.round(this.productReviews.filter(rev => rev.rating == 5).length * 100 / this.productReviews.length) | 0;
         
          this.updateRatings();
          this.fetchReviews()
        }
        else {
          this.ratings = [
            { stars: 5, rating: 0 },
            { stars: 4, rating: 0 },
            { stars: 3, rating: 0 },
            { stars: 2, rating: 0 },
            { stars: 1, rating: 0 },
          ];
          this.fetchReviews();
        }
      }
    });
  }
}
