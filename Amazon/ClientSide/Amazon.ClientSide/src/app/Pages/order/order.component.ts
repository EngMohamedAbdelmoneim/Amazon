import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Order } from '../../Models/order';
import { FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { OrderService } from '../../Services/order.service';
import { Subscription } from 'rxjs';
import { Address } from '../../Models/address';
import { CookieService } from 'ngx-cookie-service';
import { CartService } from '../../Services/cart.service';
import { Router, RouterModule } from '@angular/router';
import {
  loadStripe,
  Stripe,
  StripeCardCvcElement,
  StripeCardExpiryElement,
  StripeCardNumberElement,
} from '@stripe/stripe-js';
import { Cart } from '../../Models/cart';
import { ToastrService } from 'ngx-toastr';
import { ProductService } from '../../Services/product.service';
import { Product } from '../../Models/product';

@Component({
  selector: 'app-order',
  standalone: true,
  imports: [FormsModule, CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './order.component.html',
  styleUrl: './order.component.css',
})
export class OrderComponent  implements OnInit
{

  order: Order;
  product: Product | null = new Product(0, "", 0, "", "", [],0, "",0, "", 0, null);

  showCardForm:boolean = false;
  cardNumberr:string = '';
  nameOnCard:string = '';
  expirationDate:string = '';
  securityCode:string = '';
  showDropDown:boolean=false;

  showAddressOptions:boolean = false;
  showAddressForm:boolean = false;
  currentShippingAddress:Address;
  selectedShippingAddress:Address;
  isAddressChanged: boolean = false;
  isPaymentChanged: boolean = false;
  isPaymentBoxOpen: boolean = true;

  cartId: string;
  DeliveryMethods: any;
  PaymentMethods: any;

  buttonLabel: string = 'Use This Payment Method';
  buttonColor: string = '';

  Total: number;

  Addresssub: Subscription;
  DeliverySub: Subscription;
  cartSub: Subscription;

  DeliveryValue: number = 0;
  DeliveryMethodId: number;

  cart: Cart;

  OnlinePaymentForm?: FormGroup;

  @ViewChild('cardNumber') cardNumberElement?: ElementRef;
  @ViewChild('cardExpiry') cardExpiryElement?: ElementRef;
  @ViewChild('cardCvc') cardCvcElement?: ElementRef;

  stripe: Stripe | null = null;

  cardNumber?: StripeCardNumberElement;
  cardExpiry?: StripeCardExpiryElement;
  cardCvc?: StripeCardCvcElement;
  cardErrors: any;

  TempTotal: number = 0;
  Items: number;

  constructor(
    private orderService: OrderService,
    private cookieService: CookieService,
    private cartService: CartService,
    private toastr: ToastrService,
    private router: Router,
    private productService: ProductService
  ) {}

  ngOnInit()
  {
    // console.log(this.cartId)

    this.cartId = this.cookieService.get('guid');

    this.cartSub = this.cartService.getAllFromCart(`cart-${this.cartId}`).subscribe({
        next: (d) => {
          this.cart = d;
          console.log(d);
          this.CalculateTotal();
          // console.log('this is the cart', this.cart);
          // this.Total = d.items.reduce((sum, item) => sum + item.price * item.quantity, 0);
          // this.Items = d.items.reduce((sum, item) => sum + item.quantity, 0);
          // this.TempTotal = this.Total;
          // console.log(this.Total)
        },
        error: (e) => {
          console.log(e);
        },
      });

    this.Addresssub = this.orderService.getAddresses().subscribe({
      next: (d) => {
        console.log('adresses', d);
        // this line breaks the page, can't remember why I wrote it
        // this.order.UserAddress = [...d];
        /////////////////////////////////////////////////////////
        this.currentShippingAddress = d[0];
        console.log(this.currentShippingAddress);
        this.selectedShippingAddress = d[0];
        // this.OrderTest.AddresId = this.selectedShippingAddress.id;

        this.DeliverySub = this.orderService.getDeliveryMethods().subscribe({
          next: (d) => {
            this.DeliveryMethods = d;
            this.PaymentMethods = this.orderService
              .getPaymentMethods()
              .subscribe({
                next: (d) => {
                  this.PaymentMethods = d;
                },
              });
          },
        });
      },
      error: () => {
        this.toastr.error('No Address was Found');
        setTimeout(() => {
          this.router.navigateByUrl('/manage-address-book');
        }, 2000);
      },
    });
  }

  // #region Payment Methods

  async onSubmit() {
    this.cartSub = this.cartService.getAllFromCart(`cart-${this.cartId}`).subscribe({
        next: (d) => {
          this.cart = d;
          console.log(this.cart);
          this.cart.shippingPrice = this.DeliveryValue;
          this.cart.deliveryMethodId = this.DeliveryMethodId;

          console.log('this is the cart', this.cart);

          this.stripe
            .confirmCardPayment(this.cart.clientSecret, {
              payment_method: {
                card: this.cardNumber,
                // billing_details: {
                //   name: this.OnlinePaymentForm?.get('paymentForm')?.get('nameOnCard')?.value
                // }
              },
            })
            .then((res) => {
              if (res.paymentIntent) {
                this.orderService.placeOrder(`cart-${this.cookieService.get('guid')}`, this.cart.deliveryMethodId, 2, this.currentShippingAddress.id)
                  .subscribe({
                    next: (d) => {
                      console.log(d);
                      this.cartService.removeCart(
                        `cart-${this.cookieService.get('guid')}`
                      );
                      this.toastr.success('Payment Successful', 'Success', {
                        positionClass: 'toast-bottom-right',
                      });
                      setTimeout(() => {
                        window.location.href = 'http://localhost:4200';
                      }, 1000);
                    },
                    error: (e) => {
                      console.log(e);
                      this.toastr.error('Payment Failed', 'Failed', {
                        positionClass: 'toast-bottom-right',
                      });
                      // this.cookieService.delete('Qnt')
                      // this.toastr.success("Payment Successful", "Success", {positionClass:'toast-bottom-right'})
                      // window.location.href = 'http://localhost:4200';
                    },
                  });
              }
            });

        this.stripe.confirmCardPayment(this.cart.clientSecret, {
          payment_method: {
            card: this.cardNumber,
            // billing_details: {
            //   name: this.OnlinePaymentForm?.get('paymentForm')?.get('nameOnCard')?.value
            // }
          }
        }).then(res =>
        {
          if(res.paymentIntent)
            {
              this.orderService.placeOrder(`cart-${this.cookieService.get('guid')}`, this.cart.deliveryMethodId, 2, this.currentShippingAddress.id).subscribe({
                next: d =>
                {
                  console.log(d);
                  this.cartService.removeCart(`cart-${this.cookieService.get('guid')}`);
                  this.cookieService.delete('Qnt');
                  this.toastr.success("Payment Successful", "Success", {positionClass:'toast-bottom-right'})
                  window.location.href = 'http://localhost:4200';
                },
                error: e =>
                {
                  console.log(e);
                  this.toastr.error("Payment Failed", "Failed", {positionClass: 'toast-bottom-right'})
                  // this.cookieService.delete('Qnt')
                  // this.toastr.success("Payment Successful", "Success", {positionClass:'toast-bottom-right'})
                  // window.location.href = 'http://localhost:4200';
                }
              })
            }
        })
      },
      error: e => {
        console.log(e)
      }
    })
  }

  paymentMethodChoice(paymentMethodId: number) {
    if (paymentMethodId == 2) {
      console.log(this.cart);
      let cartId = this.cookieService.get('guid');

      this.cartService.createPaymentIntent(`cart-${cartId}`).subscribe({
        next: (res) => {
          console.log('success');
        },
        error: (e) => console.log(e),
      });

      loadStripe(
        'pk_test_51QBxlsGxfUlD5tIRm7qqPS3KLHioihsPUsHSOxHy5pbXi4tdXhrdneN8z9epNWHNczPjc10Jyt20GIgQLJhjmg9X001nO7NxRt'
      ).then((stripe) => {
        this.stripe = stripe;
        const elements = stripe?.elements();
        if (elements) {
          this.cardNumber = elements.create('cardNumber');
          this.cardNumber.mount(this.cardNumberElement?.nativeElement);
          this.cardNumber.on('change', (e) => {
            if (e.error) this.cardErrors = e.error.message;
            else this.cardErrors = null;
          });

          this.cardExpiry = elements.create('cardExpiry');
          this.cardExpiry.mount(this.cardExpiryElement?.nativeElement);
          this.cardExpiry.on('change', (e) => {
            if (e.error) this.cardErrors = e.error.message;
            else this.cardErrors = null;
          });

          this.cardCvc = elements.create('cardCvc');
          this.cardCvc.mount(this.cardCvcElement?.nativeElement);
          this.cardCvc.on('change', (e) => {
            if (e.error) this.cardErrors = e.error.message;
            else this.cardErrors = null;
          });
        }
      });
    }
  }

  shippingMethodChoice(shippingMethodId)
  {
    switch (shippingMethodId)
    {
      case 1:
        this.DeliveryValue = 30;
        this.DeliveryMethodId = 1;
        this.cart.deliveryMethodId = 1;
        this.cart.shippingPrice = 30;
        this.Total = this.TempTotal + 30;
        break;
      case 2:
        this.DeliveryValue = 20;
        this.DeliveryMethodId = 2;
        this.cart.deliveryMethodId = 2;
        this.cart.shippingPrice = 20;
        this.Total = this.TempTotal + 20;
        break;
      case 3:
        this.DeliveryValue = 10;
        this.DeliveryMethodId = 3;
        this.cart.deliveryMethodId = 3;
        this.cart.shippingPrice = 10;
        this.Total = this.TempTotal + 10;
        break;
      case 4:
        this.DeliveryValue = 0;
        this.DeliveryMethodId = 4;
        this.cart.deliveryMethodId = 4;
        this.cart.shippingPrice = 0;
        this.Total = this.TempTotal;
        break;
    }
  }

  // #endregion

  ToggleDropdown(): void {
    this.showDropDown = !this.showDropDown;
  }

  OpenCardInfo(): void {
    this.showCardForm = !this.showCardForm;
  }

  updateButton() {
    if (this.isAddressChanged) {
      this.buttonLabel = 'Use This Address';
      this.buttonColor = 'yellow';
    } else if (this.isPaymentChanged) {
      this.buttonLabel = 'Use This Payment Method';
      this.buttonColor = 'yellow';
    } else {
      this.buttonLabel = 'Use This Payment Method';
      this.buttonColor = '';
    }
  }

  SelectAddress(): void
  {
    this.currentShippingAddress = this.selectedShippingAddress;
    // if(this.selectedShippingAddress){
    console.log('Selected Address:', this.selectedShippingAddress);
    //this.selectedShippingAddress = address;
    // this.selectedShippingAddress = this.selectedShippingAddress;
    //this.order.UserAddress[0] = this.selectedShippingAddress;
    this.showAddressOptions = false;
    //}
  }

  // GetProduct()
  // {
  //   this.productService.getProductById(this.prop.id).subscribe({
  //     next: product=>{
  //       this.product =  product;
  //       console.log(this.product);
  //       if(this.product.discount != null)
  //       {
  //         this.prop.price = this.product.discount.priceAfterDiscount;
  //       }
  //       if(this.product.discount != null  && this.IsDiscountEnded())
  //       {
  //         this.product.discount.discountStarted = false;
  //       }
  //     }
  //   })
  // }

  CalculateTotal()
  {
    this.cart.items.forEach(i => {
      this.productService.getProductById(i.id).subscribe({
        next: res => {
          console.log(res);
          if(res.discount != null)
          {
            console.log(res.discount.priceAfterDiscount);
            this.TempTotal += res.discount.priceAfterDiscount * i.quantity;
            this.Total = this.TempTotal;
          }
          else
          {
            console.log(res.price);
            this.TempTotal += res.price * i.quantity;
            this.Total = this.TempTotal;
          }
        }
      })
    })
  }
}
