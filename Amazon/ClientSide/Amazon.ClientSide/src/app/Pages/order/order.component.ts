import { Component,ElementRef,Input,OnInit, ViewChild } from '@angular/core';
import { Order } from '../../Models/order';
import { FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { OrderService } from '../../Services/order.service';
import { Subscription } from 'rxjs';
import { Address } from '../../Models/address';
import { CookieService } from 'ngx-cookie-service';
import { CartService } from '../../Services/cart.service';
import { Router } from '@angular/router';
import { loadStripe, Stripe, StripeCardCvcElement, StripeCardExpiryElement, StripeCardNumberElement } from '@stripe/stripe-js'
import { Cart } from '../../Models/cart';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-order',
  standalone: true,
  imports: [FormsModule,CommonModule, ReactiveFormsModule],
  templateUrl: './order.component.html',
  styleUrl: './order.component.css'
})
export class OrderComponent  implements OnInit {


  order: Order = new Order(1, 1, "", [], 1, "", 1, "", "");


  showCardForm:boolean = false;
  cardNumberr:string = '';
  nameOnCard:string = '';
  expirationDate:string = '';
  securityCode:string = '';
  countriesList: string[] = [];
  countiresWithCodes:any[]=[];
  selectedCountryCode:string='';
  showDropDown:boolean=false;
  selectedCountry:any=null;

  showAddressOptions:boolean = false;
  showAddressForm:boolean = false;
  // country:string = '';
  // phoneNumber:number = 0;
  // streetNumber:number = 0;
  // unit:string = '';
  // city:string = '';
  // state:string = '';
  // zipCode:number = 0;
  currentShippingAddress:Address;
  selectedShippingAddress:Address;
  // selectedShippingAddress:Address = this.order.UserAddress[0];
  isAddressChanged: boolean = false;
  isPaymentChanged: boolean = false;
  isPaymentBoxOpen: boolean = true;
  
  cartId: string;
  DeliveryMethods: any;
  PaymentMethods: any;

  buttonLabel: string = 'Use This Payment Method';
  buttonColor: string = '';

  Total: number;

  DeliveryTest: any;

  Addresssub: Subscription;
  DeliverySub: Subscription;
  cartSub: Subscription;
  deleteCartSub: Subscription;

  PaymentValue: number;
  DeliveryValue: number;
  DeliveryMethodId: number;

  cart: Cart;
  // OrderTest: {AddresId: string, PaymentId: string, DeliveryId: string, CartId} = {AddresId: "", PaymentId: "", DeliveryId: "", CartId:""}
  
  @Input() OnlinePaymentForm?: FormGroup;

  @ViewChild('cardNumber') cardNumberElement?: ElementRef;
  @ViewChild('cardExpiry') cardExpiryElement?: ElementRef;
  @ViewChild('cardCvc') cardCvcElement?: ElementRef;

  stripe: Stripe | null = null;

  cardNumber?: StripeCardNumberElement;
  cardExpiry?: StripeCardExpiryElement;
  cardCvc?: StripeCardCvcElement
  cardErrors: any;

  ClinetSecret: string;

  TempTotal: number;

constructor(private orderService:OrderService, private cookieService: CookieService, private cartService: CartService, private router: Router, private toastr: ToastrService) {}

ngOnInit() {

  this.cartId = this.cookieService.get('guid');
  // console.log(this.cartId)
  this.cartSub = this.cartService.getAllFromCart(`cart-${this.cartId}`).subscribe({
    next: d => {
      this.cart = d;
      console.log('this is the cart', this.cart);
      this.Total = d.items.reduce((sum, item) => sum + (item.price * item.quantity), 0)
      this.TempTotal = this.Total;
      console.log(this.Total)
    },
    error: e => {
      console.log(e)
    }
  })
  
  this.Addresssub = this.orderService.getAddresses().subscribe({
    next: d => {
      // console.log('adresses',d)
      this.order.UserAddress = d;
      this.currentShippingAddress = d[0];
      this.selectedShippingAddress = d[0];
      // this.OrderTest.AddresId = this.selectedShippingAddress.id;
    }
  })
  
  this.DeliverySub = this.orderService.getDeliveryMethods().subscribe({
    next: d => {
      console.log(d);
      this.DeliveryMethods = d;
      // this.OrderTest.DeliveryId = this.DeliveryMethods[0].id;
    }
  })  

  this.PaymentMethods = this.orderService.getPaymentMethods().subscribe({
    next: d => {
      this.PaymentMethods = d;
      // this.OrderTest.PaymentId = this.PaymentMethods[0].id;
    }
  })
   
}

  // #region Payment Methods

  async onSubmit()
  {

    this.cartSub = this.cartService.getAllFromCart(`cart-${this.cartId}`).subscribe({
      next: d => {
        this.cart = d;
        this.cart.shippingPrice = this.DeliveryValue;
        this.cart.deliveryMethodId = this.DeliveryMethodId;
        
        console.log('this is the cart', this.cart);
        this.stripe?.confirmCardPayment(this.cart.clientSecret, {
          payment_method: {
                card: this.cardNumber,
                // billing_details: {
                //   name: this.OnlinePaymentForm?.get('paymentForm')?.get('nameOnCard')?.value
                // }
          }
        })
        .then(res => {
          if(res.paymentIntent)
              {
                this.orderService.placeOrder(`cart-${this.cookieService.get('guid')}`, 2, Number(this.PaymentValue), this.currentShippingAddress.id).subscribe({
                  next: d => {
                    console.log(d);
                    this.cartService.removeCart(`cart-${this.cookieService.get('guid')}`);
                    this.toastr.success("Payment Successful", "Success", {positionClass:'toast-bottom-right'})
                    window.location.href = 'http://localhost:4200';
                  },
                  error: e => {
                    console.log(e);
                    this.toastr.error("Pyament Failed", "Failed", {positionClass: 'toast-bottom-right'})
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

  paymentMethodChoice(paymentMethodId: number)
  {
   
    if(paymentMethodId == 2)
    {
      let cartId = this.cookieService.get('guid');

      this.cartService.createPaymentIntent(`cart-${cartId}`).subscribe({
        next: (res) => {
          console.log('success');
        },
        error: (e) => console.log(e)
      })
      
      loadStripe("pk_test_51QBxlsGxfUlD5tIRm7qqPS3KLHioihsPUsHSOxHy5pbXi4tdXhrdneN8z9epNWHNczPjc10Jyt20GIgQLJhjmg9X001nO7NxRt")
      .then(stripe => {
        this.stripe = stripe;
        const elements = stripe?.elements();
        if(elements)
        {
          this.cardNumber = elements.create('cardNumber');
          this.cardNumber.mount(this.cardNumberElement?.nativeElement);
          this.cardNumber.on('change', e => {
            if(e.error) this.cardErrors = e.error.message;
            else this.cardErrors = null;
          })

          this.cardExpiry = elements.create('cardExpiry');
          this.cardExpiry.mount(this.cardExpiryElement?.nativeElement);
          this.cardExpiry.on('change', e => {
            if(e.error) this.cardErrors = e.error.message;
            else this.cardErrors = null;
          })

          this.cardCvc = elements.create('cardCvc');
          this.cardCvc.mount(this.cardCvcElement?.nativeElement);
          this.cardCvc.on('change', e => {
            if(e.error) this.cardErrors = e.error.message;
            else this.cardErrors = null;
          })

        }
      })
    }
  }

  shippingMethodChoice(shippingMethodId)
  {
    // const TempTotal = this.Total;
    switch (shippingMethodId)
    {
      case 1:
        this.DeliveryValue = 30;
        this.DeliveryMethodId = 1;
        this.cart.deliveryMethodId = 1;
        this.cart.shippingPrice = 30;
        // console.log(this.cart);
        this.Total = this.TempTotal + 30;
        break;
      case 2:
        this.DeliveryValue = 20;
        this.DeliveryMethodId = 2;
        this.cart.shippingPrice = 20;
        this.Total = this.TempTotal + 20;
        break;
      case 3:
        this.DeliveryValue = 10;
        this.DeliveryMethodId = 3;
        this.cart.shippingPrice = 10;
        this.Total = this.TempTotal + 10;
        break;
      case 4:
        this.DeliveryValue = 0;
        this.DeliveryMethodId = 4;
        this.cart.shippingPrice = 0;
        this.Total = this.TempTotal + 0;
        break;
    }
  }

  // #endregion


  ToggleDropdown(): void
  {
    this.showDropDown = !this.showDropDown;
  }


  OpenCardInfo(): void 
  {
    this.showCardForm = !this.showCardForm;
  }

  updateButton() 
  {
    if (this.isAddressChanged) 
    {
      this.buttonLabel = 'Use This Address';
      this.buttonColor = 'yellow'; 
    }
    else if (this.isPaymentChanged) 
    {
      this.buttonLabel = 'Use This Payment Method';
      this.buttonColor = 'yellow'; 
    } 
    else 
    {
      this.buttonLabel = 'Use This Payment Method';
      this.buttonColor = ''; 
    }
  }


  OpenAddressInfo(): void
  {
    this.showAddressOptions = !this.showAddressOptions;

    // this.isAddressChanged = !this.isAddressChanged;
    // this.isPaymentChanged = false;
  }

  TogglePaymentChange(): void 
  {
    this.isPaymentChanged = !this.isPaymentChanged;
    this.isPaymentBoxOpen = !this.isPaymentBoxOpen;
    this.isAddressChanged = false;
  }

  SubmitCardInfo():void
  {
    console.log('Card Number:',this.cardNumberr)
    console.log('Name On Card:',this.nameOnCard)
    console.log('Expiration Date',this.expirationDate)
    console.log('Security Code:',this.securityCode)
    this.showCardForm = false;
  }

  SelectAddress(): void
  {
    this.currentShippingAddress = this.selectedShippingAddress;
    // if(this.selectedShippingAddress){
    console.log('Selected Address:',this.selectedShippingAddress)
    //this.selectedShippingAddress = address;
    // this.selectedShippingAddress = this.selectedShippingAddress;
    //this.order.UserAddress[0] = this.selectedShippingAddress;
    this.showAddressOptions = false;
    //}
  }

  openAddressForm(): void
  {
    this.showAddressForm = true;
    this.showAddressOptions = false;
  }

  CancelAddress(): void 
  {
    this.showAddressForm = false ;
  }

  CancelAddCard(): void 
  {
    this.showCardForm = false;
  }
}