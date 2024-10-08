import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../../Services/product.service';
import { Product } from '../../Models/product';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  productImages: any;
  product: Product | null = new Product(0, "", 0, "", "", [], "", 0);
  errorMessage: string | null = null;
  selectedColorName: string | null = null;
  selectedStar: number | null = null;
  ratingMessage:String | null = null;
  availableColors: string[] = ['#ffffff', '#ac9a9a', '#36525f', '#124055', '#000000'];
  hoveredStar:number | null = null;
  sub: Subscription | null = null;

  constructor(private productService: ProductService, private route: ActivatedRoute) {}

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
  resetRate():void{
    this.selectedStar = null;
    
  }

  submitRate(starValue:number):void{
    this.selectedStar = starValue;
    this.ratingMessage = `Thanks for rating our product: ${starValue} star(s)`;

    setTimeout(() => {
      this.ratingMessage=null;
    }, 2000);
  }
  zoomImage(event:MouseEvent):void{
    const fullImage = document.getElementById('Product-Full-Size') as HTMLImageElement;

    if(fullImage){
    // const fullImage = fullImageElement.querySelector('img');

    const rectangle = fullImage.getBoundingClientRect();
    const x = (event.clientX - rectangle.left) / rectangle.width*100;
    const y = (event.clientY - rectangle.top) / rectangle.height*100;

    fullImage.style.transformOrigin = `${x}% ${y}%`
    fullImage.style.transform = "scale(2)";
  }
}
  resetImage():void{
    const image = document.getElementById('Product-Full-Size') as HTMLImageElement;
    if(image){
      image.style.transform = "scale(1)";
      image.style.transition="ease(1s)";
    }
  }
  hoverStar(starValue:number):void{
    this.hoveredStar = starValue;
  }
  resetHoverStar():void{
    this.hoveredStar = null;
  }
}

    //}
  //}
    // this.productService.getProducts().subscribe(
    //   (data:Product[])=>{this.products = data;
    //     this.errorMessage=null;
    //   }, 
