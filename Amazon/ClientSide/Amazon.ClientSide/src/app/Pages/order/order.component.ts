import { Component,ElementRef,OnInit, ViewChild } from '@angular/core';
import { Order } from '../../Models/order';
import { FormsModule } from '@angular/forms';
// import { library } from '@fortawesome/fontawesome-svg-core'
// import { faCcVisa, faCcMastercard, faPaypal, faCcAmex} from '@fortawesome/free-brands-svg-icons';
import { CommonModule } from '@angular/common';
// import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import {HttpClient} from '@angular/common/http';
import { response } from 'express';
import { OrderService } from '../../Services/order.service';
import { Subscription } from 'rxjs';
import { Address } from '../../Models/address';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-order',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './order.component.html',
  styleUrl: './order.component.css'
})
export class OrderComponent  implements OnInit {


  order: Order = new Order(1, 1, "", [], 1, "", 1, "", "");


  showCardForm:boolean = false;
  cardNumber:string = '';
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
  country:string = '';
  phoneNumber:number = 0;
  streetNumber:number = 0;
  unit:string = '';
  city:string = '';
  state:string = '';
  zipCode:number = 0;
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

  DeliveryTest: any;


  Addresssub: Subscription;
  DeliverySub: Subscription;

  PaymentValue: string = '';
  DeliveryValue: string = '';


  OrderTest: {AddresId: string, PaymentId: string, DeliveryId: string, CartId} = {AddresId: "", PaymentId: "", DeliveryId: "", CartId:""}

constructor(private orderService:OrderService, private cookieService: CookieService) {}

ngOnInit() {
  this.fetchCountries();
  this.getCountriesWithCodes();

  this.Addresssub = this.orderService.getAddresses().subscribe({
    next: d => {
      this.order.UserAddress = d;
      this.currentShippingAddress = d[0];
      this.selectedShippingAddress = d[0];
      this.OrderTest.AddresId = this.selectedShippingAddress.id;

    }
  })
  
  this.DeliverySub = this.orderService.getDeliveryMethods().subscribe({
    next: d => {
      this.DeliveryMethods = d;
      this.OrderTest.DeliveryId = this.DeliveryMethods[0].id;
    }
  })  

  this.PaymentMethods = this.orderService.getPaymentMethods().subscribe({
    next: d => {
      this.PaymentMethods = d;
      this.OrderTest.PaymentId = this.PaymentMethods[0].id;
    }
  })

  this.cartId = this.cookieService.get('guid');
  this.OrderTest.CartId = this.cartId;

   console.log(this.OrderTest)

}


onSubmit()
{
  // console.log(this.PaymentValue.nativeElement.value);
  // console.log(this.PaymentValue);
  // console.log(this.DeliveryValue);
  // console.log(this.currentShippingAddress.id);
  // console.log(this.cookieService.get('guid'));
  this.OrderTest = {AddresId: this.currentShippingAddress.id, CartId: `cart-${this.cookieService.get('guid')}`, DeliveryId: this.DeliveryValue, PaymentId: this.PaymentValue}
  console.log(this.OrderTest)
  this.orderService.placeOrder(`cart-${this.cookieService.get('guid')}`, Number(this.DeliveryValue), Number(this.PaymentValue), this.currentShippingAddress.id).subscribe({
    next: d => {
      console.log(d);
    },
    error: e => {
      console.log(e)
    }
  })
}

fetchCountries(): void {
  this.orderService.fetchCountries().subscribe(
    (response) => {
      this.countriesList = response.map(country => country.name.common);
    },
    (error) => {
      console.error('error fetching', error);
    }
  );
}

  getCountriesWithCodes(): void {
    // console.log('Fetching countries with codes...');
    this.orderService.getCountriesWithCodes().subscribe(
      (countries) => {
        // console.log('Countries fetched:', countries);
        this.countiresWithCodes = countries.map(country => ({
          name: country.name.common,
          flag: country.flags.png,
          callingCode: country.idd.root ? `${country.idd.root}${country.idd.suffixes ? country.idd.suffixes[0] : ''}` : ''
        })).filter(country => country.callingCode !== '');
        // console.log(this.countiresWithCodes);
      },
      (error) => {
        console.error('Error fetching countries:', error);
      }
    );
  }
  
ToggleDropdown():void{
  this.showDropDown = !this.showDropDown;
}

SelectCountry(country:any):void{
  this.selectedCountry = this.country;
  this.showDropDown=false;
}

OpenCardInfo():void{
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


OpenAddressInfo():void{
  this.showAddressOptions = !this.showAddressOptions;

  // this.isAddressChanged = !this.isAddressChanged;
  // this.isPaymentChanged = false;
}
TogglePaymentChange(): void {
  this.isPaymentChanged = !this.isPaymentChanged;
  this.isPaymentBoxOpen = !this.isPaymentBoxOpen;
  this.isAddressChanged = false;
}

// TogglePaymentBox(): void {
  
// }

SubmitCardInfo():void{
console.log('Card Number:',this.cardNumber)
console.log('Name On Card:',this.nameOnCard)
console.log('Expiration Date',this.expirationDate)
console.log('Security Code:',this.securityCode)
this.showCardForm = false;
}

SelectAddress():void{
  this.currentShippingAddress = this.selectedShippingAddress;
  // if(this.selectedShippingAddress){
  console.log('Selected Address:',this.selectedShippingAddress)
  //this.selectedShippingAddress = address;
  // this.selectedShippingAddress = this.selectedShippingAddress;
  //this.order.UserAddress[0] = this.selectedShippingAddress;
  this.showAddressOptions = false;
  //}
}

openAddressForm():void{
  this.showAddressForm = true;
  this.showAddressOptions = false;
}

AddNewAddress ():void{
  if(this.country && this.city && this.streetNumber && this.unit)
    {
    // const fullAddress = `${this.country},${this.city},${this.state},${this.streetNumber},${this.unit}`
    const fullAddress = new Address("", "", this.city, this.state, this.country, "", "", "", "", "", "")
    this.order?.UserAddress.push(fullAddress);
    this.currentShippingAddress = fullAddress;
    this.country = '';
    this.phoneNumber = 0;
    this.streetNumber = 0;
    this.unit = '';
    this.city = '';
    this.state = '';
    this.CancelAddress();
  }
}
CancelAddress():void{
  this.showAddressForm = false ;
}
CancelAddCard():void{
  this.showCardForm = false;
}
}