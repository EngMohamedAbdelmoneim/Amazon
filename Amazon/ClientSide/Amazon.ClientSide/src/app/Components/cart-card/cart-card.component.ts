import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CartService } from '../../Services/cart.service';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-cart-card',
  standalone: true,
  imports: [CommonModule,RouterModule],
  templateUrl: './cart-card.component.html',
  styleUrl: './cart-card.component.css'
})
export class CartCardComponent {
constructor(private router: Router, private cartService :CartService){}
  @Input({ required: true })
  prop: {
    id: Number;
    productName: string;
    category: string;
    price: Number;
    pictureUrl: string;
    quantity: Number;
  }
  @Output() itemDeleted = new EventEmitter<void>();
  Quantity() {
    return new Array(this.prop.quantity);
  }
  Discount(){
      return Number(this.prop.price)*Number(0.5);    
  }
  Delete(){
    this.itemDeleted.emit();
    console.log("on item deleting");
                
  }
} 