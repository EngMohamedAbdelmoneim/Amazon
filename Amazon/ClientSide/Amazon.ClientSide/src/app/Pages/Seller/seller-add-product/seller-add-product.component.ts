import { HttpClient } from '@angular/common/http';
import { Component, model, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { CategoryService } from '../../../Services/category.service';
import { BrandService } from '../../../Services/brand.service';
import { ProductService } from '../../../Services/product.service';
import { Discount } from '../../../Models/Discount';
import { CommonModule } from '@angular/common';
import { Product } from '../../../Models/product';
import { SellerService } from '../../../Services/seller.service';
@Component({
  selector: 'app-seller-add-product',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, RouterModule, CommonModule],
  templateUrl: './seller-add-product.component.html',
  styleUrl: './seller-add-product.component.css',
})
export class SellerAddProductComponent implements OnInit {
  productForm: FormGroup;
  product: Product | null = new Product(
    0,
    '',
    1,
    '',
    '',
    [],
    0,
    '',
    0,
    '',
    1,
    null
  );
  discount: Discount | null = new Discount(0, 0, false, null, null);
  isDisabled = false;
  categories = [];
  brands = [];
  mainImage: File = null;
  additionalImages: File[] = [];

  constructor(
    private fb: FormBuilder,
    private router: Router,
    public sellerService: SellerService,
    private http: HttpClient,
    public categoryService: CategoryService,
    public brandService: BrandService
  ) {
    console.log('Product Form Initialized:', this.productForm);
  }

  ngOnInit(): void {
    console.log('Product Form Initialized:', this.productForm);
    this.productForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      price: [1, [Validators.required, this.priceValidator, Validators.min(1)]],
      quantityInStock: [1, [Validators.required, Validators.min(1)]],
      brandId: ['', Validators.required],
      categoryId: ['', Validators.required],
      imageFile: [null, Validators.required],
      imagesFiles: [null],
      discount: null,
    });
    this.loadCategories();
    this.loadBrands();
  }

  priceValidator(control: AbstractControl): { [key: string]: any } | null {
    const value = control.value;
    const validPricePattern = /^(?!.*[a-zA-Z])\d+(\.\d{1,2})?$/;
    return validPricePattern.test(value) ? null : { invalidPrice: true };
  }
  restrictInput(event: KeyboardEvent) {
    const invalidKeys = ['+', '-', '*', '/', 'e', 'E'];
    const isAlphabet = /^[a-zA-Z]$/.test(event.key);

    if (invalidKeys.includes(event.key) || isAlphabet) {
      event.preventDefault();
    }
  }
  addDiscount() {
    if (
      this.discount.discountPercentage < 1 ||
      this.discount.discountPercentage > 99
    ) {
      alert('Discount percentage must be between 1 and 99.');
      return;
    }

    if (new Date(this.discount.startDate) > new Date(this.discount.endDate)) {
      alert('Start date cannot be later than end date.');
      return;
    }
    this.discount.discountPercentage = this.discount.discountPercentage / 100;
    console.log('Discount added:', this.discount);
  }

  resetForm() {
    this.discount = {
      priceAfterDiscount: 0,
      discountPercentage: 0.0,
      startDate: null,
      endDate: null,
      discountStarted: false,
    };
  }

  loadCategories() {
    this.categoryService.getCategories().subscribe((data: any) => {
      this.categories = data;
    });
  }

  loadBrands() {
    this.brandService.getBrands().subscribe((data: any) => {
      this.brands = data;
    });
  }

  onFileSelect(event: any, fileType: string) {
    if (fileType === 'mainImage') {
      this.mainImage = event.target.files[0];
    } else if (fileType === 'additionalImages') {
      this.additionalImages = event.target.files;
    }
  }

  onSubmit() {
    if (this.productForm.valid) {
      const formData = new FormData();
      formData.append('Name', this.productForm.get('name')?.value);
      formData.append(
        'Description',
        this.productForm.get('description')?.value
      );
      formData.append('Price', this.productForm.get('price')?.value);
      formData.append(
        'QuantityInStock',
        this.productForm.get('quantityInStock')?.value
      );
      formData.append('CategoryId', this.productForm.get('categoryId')?.value);
      formData.append('BrandId', this.productForm.get('brandId')?.value);

      if (this.mainImage) {
        formData.append('ImageFile', this.mainImage);
      }

      if (this.additionalImages.length > 0) {
        for (let i = 0; i < this.additionalImages.length; i++) {
          formData.append('ImagesFiles', this.additionalImages[i]);
        }
      }
      if (this.discount.discountPercentage == 0) {
        this.discount = null;
      }
      if (this.discount) {
        formData.append(
          'Discount.discountPercentage',
          this.discount.discountPercentage.toString()
        );
        formData.append(
          'Discount.discountStarted',
          this.discount.discountStarted.toString()
        );
        formData.append(
          'Discount.priceAfterDiscount',
          this.discount.priceAfterDiscount.toString()
        );
        formData.append(
          'Discount.startDate',
          this.discount.startDate.toString()
        );
        formData.append('Discount.endDate', this.discount.endDate.toString());
      }

      console.log('Discount:', this.discount);
      console.log('FormData:', this.product);

      this.sellerService.AddProduct(formData).subscribe({
        next: (data) => {
          console.log('Product added successfully:', data);
          this.goBack();
        },
        error: (error) => {
          console.error('Error adding product:', error);
        },
      });
    }
  }
  goBack(): void {
    this.isDisabled = true;
    setTimeout(() => {
      this.router.navigate(['seller/product-list']);
    }, 1000);
  }
}
