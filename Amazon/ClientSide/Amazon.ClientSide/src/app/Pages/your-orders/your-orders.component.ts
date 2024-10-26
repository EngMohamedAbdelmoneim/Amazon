import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../Services/order.service';
import { Subscription } from 'rxjs';
import { Order } from '../../Models/order';
import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import { MyOrder } from '../../Models/MyOrder';

@Component({
  selector: 'app-your-orders',
  standalone: true,
  imports: [DatePipe, CurrencyPipe, CommonModule],
  templateUrl: './your-orders.component.html',
  styleUrl: './your-orders.component.css'
})
export class YourOrdersComponent implements OnInit{

  constructor(private orderService: OrderService){}

  orders: Array<MyOrder>;
  orderSub: Subscription;

  ngOnInit(): void 
  {
    this.orderSub = this.orderService.getOrders().subscribe({
      next: data => {
        this.orders = data;
        console.log(this.orders);
      },
      error: e => {
        console.log(e);
      }
    })
  }

}
