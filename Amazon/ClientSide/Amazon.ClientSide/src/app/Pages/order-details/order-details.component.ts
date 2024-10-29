import {Component, OnInit} from '@angular/core';
import {Subscription} from "rxjs";
import {OrderService} from "../../Services/order.service";
import {ActivatedRoute, RouterLink} from "@angular/router";
import {Order} from "../../Models/order";
import {CommonModule, CurrencyPipe, DatePipe, NgForOf} from "@angular/common";
import {AddressService} from "../../Services/address.service";
import {Address} from "../../Models/address";
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-order-details',
  standalone: true,
  imports: [
    DatePipe,
    CurrencyPipe,
    NgForOf,
    RouterLink,
    CommonModule
  ],
  templateUrl: './order-details.component.html',
  styleUrl: './order-details.component.css'
})
export class OrderDetailsComponent implements OnInit
{
  constructor(private  orderService: OrderService, private activatedRoute: ActivatedRoute, private addressService: AddressService, private toastr: ToastrService) {
  }
  orderSub: Subscription;
  addressSub: Subscription;

  order: any;
  address: Address;
  products: any;

    ngOnInit(): void
    {
      this.orderSub = this.activatedRoute.params.subscribe(p => {
        this.orderService.getOrderById(p['id']).subscribe({
          next: data1 => {
            console.log(data1);
            this.order = data1;
            this.products = data1.items;
            console.log(this.products);
            console.log(this.order);
            this.addressService.getSavedAddresses().subscribe({
              next: data => {
                this.address = data.filter(a => a.id == this.order.shippingAddressId)[0];
                // this.address = data.filter(a => a.id == "487cafb1-0d97-4b1a-906a-7bd7b9e7e532")[0];
              }
            })
          }
        })
      })
    }

  cancelOrder(Id: number)
  {
    this.orderService.cancelOrder(Id).subscribe({
      next: () => {
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
