import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../Services/order.service';
import { Subscription } from 'rxjs';
import { Order } from '../../Models/order';
import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import { MyOrder } from '../../Models/MyOrder';
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-your-orders',
  standalone: true,
  imports: [DatePipe, CurrencyPipe, CommonModule],
  templateUrl: './your-orders.component.html',
  styleUrl: './your-orders.component.css'
})
export class YourOrdersComponent implements OnInit{

  constructor(private orderService: OrderService, private toastr: ToastrService){}

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

  cancelOrder(Id: number)
  {
    this.orderService.cancelOrder(Id).subscribe({
      next: data => {
        console.log('cancelled');
        this.toastr.success("Money Refunded", "Order Cancelled");
        setTimeout(() => {
          window.location.reload();
        }, 1000)
      },
      error: e => {
        console.log('error while cancelling the order');
        this.toastr.error("Error while cancelling the order");
      }
    })
  }

}
