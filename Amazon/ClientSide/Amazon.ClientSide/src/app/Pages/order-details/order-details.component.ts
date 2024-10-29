import {Component, OnInit} from '@angular/core';
import {Subscription} from "rxjs";
import {OrderService} from "../../Services/order.service";
import {ActivatedRoute, RouterLink} from "@angular/router";
import {Order} from "../../Models/order";
import {CommonModule, CurrencyPipe, DatePipe, NgForOf} from "@angular/common";
import {AddressService} from "../../Services/address.service";
import {Address} from "../../Models/address";

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
  constructor(private  orderService: OrderService, private activatedRoute: ActivatedRoute, private addressService: AddressService) {
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
          next: data => {
            console.log(data);
            this.order = data;
            this.products = data.items;
            console.log(this.products);
            console.log(this.order);
            this.addressService.getSavedAddresses().subscribe({
              next: data => {
                console.log(data.filter(a => a.id == this.order.shippingAddressId));
                // this.address = data.filter(a => a.id == "487cafb1-0d97-4b1a-906a-7bd7b9e7e532")[0];
                console.log(this.address);
              }
            })
          }
        })
      })
    }

}
