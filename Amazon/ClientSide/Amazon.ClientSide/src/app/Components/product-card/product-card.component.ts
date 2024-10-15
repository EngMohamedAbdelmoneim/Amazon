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
  getRatingArray(rating: number): boolean[] {
    return Array.from({ length: 5 }, (_, i) => i < rating);
  }
   AddToCart() {
    this.addToCart.emit();
    console.log("on item adding");
  }
}
