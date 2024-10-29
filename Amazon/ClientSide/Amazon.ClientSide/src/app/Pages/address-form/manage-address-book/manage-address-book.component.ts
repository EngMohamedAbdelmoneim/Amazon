import { Component,OnInit,Output,EventEmitter} from '@angular/core';
import { AddressService } from '../../../Services/address.service';
import { Address } from '../../../Models/address';
import { CommonModule } from '@angular/common';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-manage-address-book',
  standalone: true,
  imports: [CommonModule,FormsModule, ReactiveFormsModule],
  templateUrl: './manage-address-book.component.html',
  styleUrl: './manage-address-book.component.css'
})
export class ManageAddressBookComponent implements OnInit
{
  AddressForm: FormGroup;
  EditAddressForm: FormGroup | null = null;
  savedAddresses: Address[] = [];
  @Output() addressAdded = new EventEmitter<void>();

  constructor(private addressService: AddressService, private http: HttpClient, private router: Router, private toastr: ToastrService) {}
  address:Address = new Address('','','','','','','','','','','');

  showAddAddressForm: boolean = false;
  showEditAddressForm: boolean = false;

  isEditMode: boolean = false;
  selectedAddress: Address | null = null;
  countriesUrl = "/assets/Countries.json";
  countries: any;

  deleteSub: Subscription | null;

  ngOnInit(): void
  {
    this.fetchSavedAddresses();
    this.http.get(this.countriesUrl).subscribe({
      next: data => {
        this.countries = data;
      }
    });

    this.AddressForm = new FormGroup({
      'Country': new FormControl(null, Validators.required),
      'FirstName': new FormControl(null, Validators.required),
      'LastName': new FormControl(null, Validators.required),
      'Phonenumber': new FormControl(null, [Validators.required, Validators.minLength(11), Validators.maxLength(11), Validators.pattern('^[0-9]*$')]),
      'StreetAddress': new FormControl(null, Validators.required),
      'BuildingName': new FormControl(null, Validators.required),
      'City': new FormControl(null, Validators.required),
      'District': new FormControl(null, Validators.required),
      'Governorate': new FormControl(null, Validators.required),
      'NearestLandMark': new FormControl(null, Validators.required),
    })

    // this.EditAddressForm = new FormGroup({
    //   'Countrye': new FormControl(this.address.country, Validators.required),
    //   'FirstNamee': new FormControl(this.address.firstName, Validators.required),
    //   'LastNamee': new FormControl(this.address.lastName, Validators.required),
    //   'Phonenumbere': new FormControl(this.address.phoneNumber, [Validators.required, Validators.minLength(11), Validators.maxLength(11), Validators.pattern('^[0-9]*$')]),
    //   'StreetAddresse': new FormControl(this.address.streetAddress, Validators.required),
    //   'BuildingNamee': new FormControl(this.address.buildingName, Validators.required),
    //   'Citye': new FormControl(this.address.city, Validators.required),
    //   'Districte': new FormControl(this.address.district, Validators.required),
    //   'Governoratee': new FormControl(this.address.governorate, Validators.required),
    //   'NearestLandMarke': new FormControl(this.address.nearestLandMark, Validators.required),
    // })
  }

  onSelectAddress(address: Address): void
  {
    this.selectedAddress = address;
  }
  //Fetch address saved in database and display them in the boxes

  openAddForm()
  {
    // this.isEditMode = false;
    this.address = this.address;
    this.showAddAddressForm = true;
  }


  openEditForm(selectedAddress: Address)
  {
    console.log(selectedAddress.city)
    console.log("=========================")
    console.log(this.address);

    this.EditAddressForm = new FormGroup({
      'id': new FormControl(selectedAddress.id),
      'Country': new FormControl(selectedAddress.country, Validators.required),
      'FirstName': new FormControl(selectedAddress.firstName, Validators.required),
      'LastName': new FormControl(selectedAddress.lastName, Validators.required),
      'Phonenumber': new FormControl(selectedAddress.phoneNumber, [Validators.required, Validators.minLength(11), Validators.maxLength(11), Validators.pattern('^[0-9]*$')]),
      'StreetAddress': new FormControl(selectedAddress.streetAddress, Validators.required),
      'BuildingName': new FormControl(selectedAddress.buildingName, Validators.required),
      'City': new FormControl(selectedAddress.city, Validators.required),
      'District': new FormControl(selectedAddress.district, Validators.required),
      'Governorate': new FormControl(selectedAddress.governorate, Validators.required),
      'NearestLandMark': new FormControl(selectedAddress.nearestLandMark, Validators.required),
    })



    this.showEditAddressForm = true;
    // this.isEditMode = true;
    this.address = { ...selectedAddress };

    console.log(this.address);
    // this.showAddAddressForm = true;
  }

  closeEditForm()
  {
    this.showEditAddressForm = false;
  }

  fetchSavedAddresses(): void
  {
    this.addressService.getSavedAddresses().subscribe(
      (addresses) => {
        console.log(addresses);
        this.savedAddresses = addresses;
      },
      (error) => {
        console.error('Error fetching addresses:', error);
      }
    );
  }

  // editAddress(selectedAddress: Address) {
  //   this.address = selectedAddress;
  //   this.showEditForm = true;
  // }

  editAddress()
  {
    //if(this.selectedAddress){
    //   this.address = selectedAddress;

      let editedAddress = this.EditAddressForm.value;

      console.log(editedAddress);

      this.addressService.updateAddress(editedAddress).subscribe({
        next:(response) =>{
          this.toastr.success('Address Modified');
          window.location.reload();
        },
        error:(error) => {
          console.log('Error updating address',error);
        }
      });
    //}
  }

  // editAddress(selectedAddress: Address)
  // {

  //   console.log('cond true')
  //   this.address = selectedAddress;
  //   this.addressService.updateAddress(this.address).subscribe({
  //   next:(response) =>{
  //     console.log(this.address);
  //     this.toastr.success('Address Modified');
  //     this.toggleAddAddressForm();
  //     this.fetchSavedAddresses();
  //   },
  //   error:(error) => {
  //     console.log('Error updating address',error);
  //   }
  // });

  // }

  loadAddresses()
  {
    this.addressService.getSavedAddresses().subscribe(
      (data) => {
        this.savedAddresses = data;
        window.location.reload();
      },
      (error) => {
        console.error('Error loading addresses', error);
      }
    );
  }

  deleteAddress(addressId: string)
  {
    this.addressService.deleteAddress(addressId).subscribe({
      next: (res) => {
        this.toastr.success('Address Deleted', "Success", {positionClass: 'toast-bottom-right'});
        this.fetchSavedAddresses();
      },
      error:(error) =>{
        this.toastr.error("Error Deleting the Address", "Error", {positionClass: 'toast-bottom-right'});
      }
    });
  }

  //attached with cancel button to close the form
  closeForm()
  {
    this.showAddAddressForm = false;
    this.address = this.address;
    this.addressAdded.emit();
  }

  toggleAddAddressForm(): void
  {
    this.showAddAddressForm = !this.showAddAddressForm;
  }

  loadAddressToEdit(selectedAddress: Address)
  {
    this.address = selectedAddress;
    this.isEditMode = true;
  }

  onSubmit()
  {
    if (this.isEditMode)
    {
      // this.editAddress(this.address);
    }
    else
    {
      this.onSubmitNewAddress();
    }
  }

  //binded to the submit button that add the input of form into the database
  onSubmitNewAddress():void
  {
    let { Country, FirstName, LastName, Phonenumber, StreetAddress, BuildingName, City, District, Governorate, NearestLandMark } = this.AddressForm.value;

    this.address.country = Country;
    this.address.firstName = FirstName;
    this.address.lastName = LastName;
    this.address.phoneNumber = Phonenumber;
    this.address.streetAddress = StreetAddress;
    this.address.buildingName = BuildingName;
    this.address.city = City;
    this.address.district = District;
    this.address.governorate = Governorate;
    this.address.nearestLandMark = NearestLandMark;

    console.log(this.address);


    this.addressService.addAddresses(this.address).subscribe(
    (response)=>{
      this.AddressForm.reset();
      this.fetchSavedAddresses();
      this.toggleAddAddressForm();
      this.addressAdded.emit();
      this.resetAddressForm();
      this.toastr.success("Added New Address", "Success", {positionClass : "toast-bottom-right"});
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
            this.toastr.success('Changed Default Address')
            // this.router.navigate(['/manage-address-book']);
          },
          error:(error) =>{
            console.log('Error setting default address',error);
        }
      });
    }
  }

  resetAddressForm(): void
  {
    this.address = {
      id:'',
      country: '',
      firstName: '',
      lastName: '',
      phoneNumber: '',
      streetAddress: '',
      buildingName: '',
      city: '',
      district: '',
      governorate: '',
      nearestLandMark: ''
    };
  }
}
