import { Component,OnInit,Output,EventEmitter} from '@angular/core';
import { AddressService } from '../../../Services/address.service';
import { Address } from '../../../Models/address';
import { CommonModule } from '@angular/common';
import { response } from 'express';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

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

  constructor(private addressService: AddressService) {}
  address:Address = new Address('','','','','','','','','','','');
  showAddAddressForm = false;
  isEditMode = false;

  ngOnInit(): void {
    this.fetchSavedAddresses();
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

  editAddress(address: Address): void {
    this.isEditMode = true;
    this.address = { ...address };
    this.showAddAddressForm = true;
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

deleteAddress(id) {
  if (confirm('Are you sure you want to delete this address?')) {
      this.addressService.deleteAddress(id).subscribe(() => {
          this.loadAddresses();
      });
  }
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
}
