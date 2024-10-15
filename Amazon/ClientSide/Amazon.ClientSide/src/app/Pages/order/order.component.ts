import { Component,OnInit } from '@angular/core';
import { Order } from '../../Models/order';
import { FormsModule } from '@angular/forms';
// import { library } from '@fortawesome/fontawesome-svg-core'
// import { faCcVisa, faCcMastercard, faPaypal, faCcAmex} from '@fortawesome/free-brands-svg-icons';
import { CommonModule } from '@angular/common';
// import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import {HttpClient} from '@angular/common/http';
import { response } from 'express';
import { OrderService } from '../../Services/order.service';

@Component({
  selector: 'app-order',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './order.component.html',
  styleUrl: './order.component.css'
})
export class OrderComponent  implements OnInit {
order:Order|null= new Order(0,1,"ahmed",["1,Nasr City,Egypt","2,Tahrir,Egypt"],2,"10/9/2024",1500,"Confirmed","Shipped");


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
currentShippingAddress:string = this.order.UserAddress[0];
selectedShippingAddress:string = this.order.UserAddress[0];
isAddressChanged: boolean = false;
isPaymentChanged: boolean = false;
isPaymentBoxOpen: boolean = true;


buttonLabel: string = 'Use This Payment Method';
buttonColor: string = '';

constructor(private orderService:OrderService) {}

ngOnInit() {
  // Call the API to get the list of countries when the component initializes
  this.fetchCountries();
  this.getCountriesWithCodes();
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
  if(this.country && this.city && this.streetNumber && this.unit){
    const fullAddress = `${this.country},${this.city},${this.state},${this.streetNumber},${this.unit}`
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