import { Component, Output,EventEmitter, OnInit } from '@angular/core';
import { AddressService} from '../../Services/address.service';
import { Address } from '../../Models/address';
import { response } from 'express';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { error } from 'console';
import { Router } from '@angular/router';


@Component({
  selector: 'app-address-form',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './address-form.component.html',
  styleUrl: './address-form.component.css'
})
export class AddressFormComponent implements OnInit{
  @Output() addressAdded = new EventEmitter<void>();

  savedAddresses:Address[] = [];
  selectedAddress:Address|null=null;
  showAddressForm = false;
  showAddAddressForm = false;
  showConfirmation = false;

  address:Address = new Address('','','','','','','','','','','');

  constructor(private addressService:AddressService,private router: Router){}

  ngOnInit(): void {
      this.fetchSavedAddresses();
  }

  fetchSavedAddresses():void{
    this.addressService.getSavedAddresses().subscribe(
      (addresses) =>{
        this.savedAddresses = addresses;
      },
      (error)=>{
        console.error('Error fetching addresses:',error)
      }
    );
  }
  toggleAddAddressForm():void{
    this.showAddressForm = !this.showAddressForm;
    this.showAddAddressForm = !this.showAddAddressForm;
  }
  closeForm(){
    this.showAddressForm = false;
    this.showAddAddressForm = false;
    this.addressAdded.emit();
  }
  onSelectAddress(address:Address):void{
    this.selectedAddress = address;
    this.moveSelectedAddressToTop();
    this.showConfirmation = true;
  }
  moveSelectedAddressToTop():void{
    if(this.selectedAddress){
      const index = this.savedAddresses.indexOf(this.selectedAddress);
      if(index > -1){
        this.savedAddresses.splice(index,1);
        this.savedAddresses.unshift(this.selectedAddress);
      }
    }
  }
  onSubmit():void{
    if(this.selectedAddress){  
    this.moveSelectedAddressToTop();
    this.addressService.addAddresses(this.selectedAddress).subscribe(
      (response)=>{
        console.log('selected',response);
        this.fetchSavedAddresses();
        this.closeForm();
      },
      (error)=>{
          console.log('error',error);
        // this.closeForm();
      }
    );
  }
}
confirmAddress(): void {
  this.showConfirmation = false;
  console.log('Address confirmed');
}

  onSubmitNewAddress():void{
    this.addressService.addAddresses(this.address).subscribe(
    (response)=>{
      this.fetchSavedAddresses();
      this.toggleAddAddressForm();
      this.addressAdded.emit();
    },
    (error)=>{
      console.error('Error adding address:', error);
      }
    );
  }

  goToAddressBook(): void {
    this.closeForm();
    this.router.navigate(['/manage-address-book']);
  }
}
