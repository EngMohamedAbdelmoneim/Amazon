import { Component,OnInit,Output,EventEmitter} from '@angular/core';
import { AddressService } from '../../../Services/address.service';
import { Address } from '../../../Models/address';
import { CommonModule } from '@angular/common';
import { response } from 'express';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { error } from 'console';

@Component({
  selector: 'app-manage-address-book',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './manage-address-book.component.html',
  styleUrl: './manage-address-book.component.css'
})
export class ManageAddressBookComponent implements OnInit{
  savedAddresses: Address[] = [];
  @Output() addressAdded = new EventEmitter<void>();

  constructor(private addressService: AddressService, private router: Router) {}
  address:Address = new Address('','','','','','','','','','','');
  showAddAddressForm = false;
  isEditMode = false;
  selectedAddress: Address | null = null;

  ngOnInit(): void {
    this.fetchSavedAddresses();
  }

  onSelectAddress(address: Address): void {
    this.selectedAddress = address;
  }
  //Fetch address saved in database and display them in the boxes
  fetchSavedAddresses(): void {
    this.addressService.getSavedAddresses().subscribe(
      (addresses) => {
        this.savedAddresses = addresses;
      },
      (error) => {
        console.error('Error fetching addresses:', error);
      }
    );
  }

  editAddress(selectedAddress: Address){
    if(this.selectedAddress){
      this.address = selectedAddress;
      this.addressService.updateAddress(this.address).subscribe({
        next:(response) =>{
          this.router.navigate(['/manage-address-book'])
        },
        error:(error) => {
          console.log('Error updating address',error);
        }
      });
    }
  }

loadAddresses() {
  this.addressService.getSavedAddresses().subscribe(
    (data) => {
      this.savedAddresses = data;
    },
    (error) => {
      console.error('Error loading addresses', error);
    }
  );
}

deleteAddress(addressId: string) {
  this.addressService.deleteAddress(addressId).subscribe({
    next:(response) =>{
      this.router.navigate(['/manage-address-book'])
    },
    error:(error) =>{
      console.error('Error deleting address', error);
    }
  });
}
//attached with cancel button to close the form
closeForm(){
  this.showAddAddressForm = false;
  this.addressAdded.emit();
}
toggleAddAddressForm():void{
  this.showAddAddressForm = !this.showAddAddressForm;
}

//binded to the submit button that add the input of form into the database
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

onSetDefaultAddress(addressId: string):void{
  if(addressId){
    this.addressService.setDefaultAddress(addressId)
      .subscribe({
        next:(response) =>{
          this.router.navigate(['/manage-address-book'])
        },
        error:(error) =>{
          console.log('Error setting default address',error);
      }
    });
  }
}

}
