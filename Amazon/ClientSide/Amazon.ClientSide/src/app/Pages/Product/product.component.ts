import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../../Services/product.service';
import { Product } from '../../Models/product';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';
import { CartService } from '../../Services/cart.service';
import { Cart } from '../../Models/cart';
import { CartItem } from '../../Models/cart-item';
import { GuidService } from '../../Services/guid.service';
import { WishListService } from '../../Services/wish-list.service';
import { WishListItem } from '../../Models/wish-list-item';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  productImages: any;
  product: Product | null = new Product(0, "", 0, "", "", [],"", "", 0,null);
  cartItems: CartItem | null = new CartItem(0, "", "", 0, "", 0);
  cart: Array<CartItem> | null;
  @ViewChild( 'quantity') selectedQtn : ElementRef ;
  errorMessage: string | null = null;
  selectedColorName: string | null = null;
  selectedStar: number | null = null;
  ratingMessage: String | null = null;
  availableColors: string[] = ['#ffffff', '#ac9a9a', '#36525f', '#124055', '#000000'];
  hoveredStar: number | null = null;
  sub: Subscription | null = null;

  constructor(private productService: ProductService, public cartService: CartService, public wishListService: WishListService, private route: ActivatedRoute , public guidServices: GuidService) { }

  ngOnInit(): void {
    this.sub = this.route.params.subscribe(p => {
      this.productService.getProductById(p['id']).subscribe({
        next: data => {
          this.product = data;
          this.productImages = [data.pictureUrl, ...data.productImages];
          console.log(data)
        },
        error: err => {
          console.error('Sorry, we couldn\'t fetch the data', err);
          this.errorMessage = 'Sorry';
        }
      })
    })
  }
  Discount(){
    console.log(this.product.discount.discountPercentage);
    return Number(this.product.discount.discountPercentage *100);
  }
  DiscountTimeOut(){
    let StartDate:any =new Date(this.product.discount.startDate).getTime();
    let EndDate:any =new Date(this.product.discount.endDate).getTime();
    let Days = EndDate - StartDate;
    const DaysOut = Math.floor(Days / (1000 * 60 * 60 * 24));
    console.log(DaysOut);
    return DaysOut;
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
    this.cartService.updateCartWithItem(("cart-"+_id),cartitem);
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
    this.wishListService.updateWishListWithItem(("wishlist-"+_id),wishListItem);
  }
  // Function to display side image
  DisplaySideImage(pictureUrl: string): void {
    if (this.product) {
      this.product.pictureUrl = pictureUrl;
    }
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

  getGuid():string{
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
}

//}
//}
// this.productService.getProducts().subscribe(
//   (data:Product[])=>{this.products = data;
//     this.errorMessage=null;
//   }, 
