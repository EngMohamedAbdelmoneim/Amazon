import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CartService } from '../../Services/cart.service';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CartItem } from '../../Models/cart-item';
import { Subscription } from 'rxjs';
import { ProductService } from '../../Services/product.service';
import { Product } from '../../Models/product';

@Component({
  selector: 'app-cart-card',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './cart-card.component.html',
  styleUrl: './cart-card.component.css'
})
export class CartCardComponent implements OnInit {
  constructor(public productService:ProductService) { }
  @Input({ required: true })prop: CartItem;
  @Output() itemDeleted = new EventEmitter<void>();
  @Output() qntChanged = new EventEmitter<any>();
  product:Product;
  sub: Subscription | null;
  ngOnInit(){
    this.GetProduct();
  }
  Discount(){
    console.log(this.product.discount.discountPercentage);
    return Number(this.product.discount.discountPercentage *100);
  }
  IsDiscountEnded(){
    let TodeyDate:any =new Date().getTime();
    let EndDate:any =new Date(this.product.discount.endDate).getTime();
    let Days = EndDate - TodeyDate;
    const HoursOut = Math.floor((Days % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    return HoursOut<0;
  }
  Quantity(max: number) {
    return Array.from({ length: Number(max) - 0 + 1 }, (v, k) => k + 0);
  }
  Price() {
    return Number(this.prop.price) * Number(this.prop.quantity);
  }
  Delete() {
    this.itemDeleted.emit();
    console.log("on item deleting");

  }
  QNTChanged() {
    this.qntChanged.emit();
    console.log("on item QNT Changed");
  }
  GetProduct(){
    console.log("ssssssssssssssssssssssssssssssssss",this.prop.id);
    this.productService.getProductById(this.prop.id).subscribe({
      next:async product=>{
           this.product = await product;
           console.log(this.product);
           if(this.product.discount != null  && this.IsDiscountEnded()){
            this.product.discount.discountStarted = false;
          }
      }
    })
  }
} 