import { Component, EventEmitter, Input, Output } from '@angular/core';
import { WishListService } from '../../Services/wish-list.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-wishlist-card',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './wish-list-card.component.html',
  styleUrl: './wish-list-card.component.css'
})
export class WishlistCardComponent {
  constructor(private wishListService: WishListService) { }
  @Input({ required: true })
  prop: {
    id: Number;
    productName: string;
    category: string;
    brand: string;
    price: Number;
    pictureUrl: string;
    quantity: Number;
  }
  @Output() itemListDeleted = new EventEmitter<void>();
  @Output() addToCart = new EventEmitter<void>();

  Delete() {
    this.itemListDeleted.emit();
    console.log("on item deleting");
  }
  AddToCart() {
    this.addToCart.emit();
    console.log("on item adding");
  }

  FormatDate(): string {
    const addedTime:Date = new Date();
    const options: Intl.DateTimeFormatOptions = { day: 'numeric', month: 'long', year: 'numeric' };
    return new Intl.DateTimeFormat('en-GB', options).format(addedTime);
  }

}
