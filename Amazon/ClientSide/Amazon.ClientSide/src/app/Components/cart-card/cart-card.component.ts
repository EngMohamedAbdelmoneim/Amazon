import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CartService } from '../../Services/cart.service';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-cart-card',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './cart-card.component.html',
  styleUrl: './cart-card.component.css'
})
export class CartCardComponent {
  constructor(private cartService: CartService) { }
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
  @Output() qntChanged = new EventEmitter<any>();
  Quantity(max: number) {
    return Array.from({ length: Number(max) - 0 + 1 }, (v, k) => k + 0);

  }
  Price() {
    return Number(this.prop.price) * Number(this.prop.quantity);
  }
  Discount() {
    return Number(this.prop.price) * Number(0.5);
  }
  Delete() {
    this.itemDeleted.emit();
    console.log("on item deleting");

  }
  QNTChanged() {
    this.qntChanged.emit();
    console.log("on item QNT Changed");
  }
} 