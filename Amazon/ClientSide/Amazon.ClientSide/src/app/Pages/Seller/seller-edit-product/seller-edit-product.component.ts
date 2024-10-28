import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CategoryService } from '../../../Services/category.service';
import { BrandService } from '../../../Services/brand.service';
import { Product } from '../../../Models/product';
import { Subscription } from 'rxjs';
import { ProductService } from '../../../Services/product.service';
import { Discount } from '../../../Models/Discount';
import { CommonModule } from '@angular/common';
import { SellerService } from '../../../Services/seller.service';

@Component({
  selector: 'app-seller-add-product',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, RouterModule, CommonModule],
  templateUrl: './seller-edit-product.component.html',
  styleUrl: './seller-edit-product.component.css',
})
export class SellerEditProductComponent implements OnInit {
  productForm: FormGroup;
  product: Product;
  categories = [];
  brands = [];
  mainImage: any = null;
  additionalImages: any[] = [];
  isDisabled = false;
  discount: Discount | null = new Discount(0, 0, false, null, null);
  sub: Subscription | null = null;
  constructor(
    public route: ActivatedRoute,
    private router: Router,
    public sellerService: SellerService,
    private fb: FormBuilder,
    private http: HttpClient,
    public categoryService: CategoryService,
    public brandService: BrandService
  ) {}

  ngOnInit(): void {
    this.sub = this.route.params.subscribe((params) => {
      if (params['product']) {
        this.product = JSON.parse(decodeURIComponent(params['product']));
        if (this.product.discount) {
          this.discount = this.product.discount;
        }
      }
    });
    console.log('Product Form Initialized:', this.productForm);
    this.productForm = this.fb.group({
      id: this.product.id,
      name: ['', Validators.required],
      description: ['', Validators.required],
      price: [1, [Validators.required, Validators.min(1), this.priceValidator]],
      quantityInStock: [1, [Validators.required, Validators.min(1)]],
      brandId: ['', Validators.required],
      categoryId: ['', Validators.required],
      imageFile: [null],
      imagesFiles: [null],
      discount: null,
    });
    this.loadCategories();
    this.loadBrands();
  }
  priceValidator(control: AbstractControl): { [key: string]: any } | null {
    const value = control.value;
    const validPricePattern = /^\d+(\.\d{1,2})?$/;
    return validPricePattern.test(value) ? null : { invalidPrice: true };
  }
  restrictInput(event: KeyboardEvent) {
    const invalidKeys = ['+', '-', '*', '/', 'e', 'E'];
    const isAlphabet = /^[a-zA-Z]$/.test(event.key);

    if (invalidKeys.includes(event.key) || isAlphabet) {
      event.preventDefault();
    }
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

  addDiscount() {
    if (
      this.discount.discountPercentage <= 0 ||
      this.discount.discountPercentage >= 1
    ) {
      alert('Discount percentage must be between 0 and 1.');
      return;
    }
    if (new Date(this.discount.startDate) > new Date(this.discount.endDate)) {
      alert('Start date cannot be later than end date.');
      return;
    }
    console.log('Discount added:', this.discount);
  }

  resetForm() {
    this.discount = new Discount(0, 0, false, null, null);
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
      } else {
        this.discount =
          this.productForm.get('discount')?.value || this.discount;
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
      console.log('FormData:', formData);

      this.sellerService.UpdateProduct(formData, this.product.id).subscribe({
        next: (data) => {
          console.log('Product added successfully:', data);
          this.goBack();
        },
        error: (error) => {
          console.error('Error adding product:', error);
        },
      });
      // this.router.navigate(['seller/product-list'])
    }
  }
  goBack(): void {
    this.isDisabled = true;
    setTimeout(() => {
      this.router.navigate(['seller/product-list']);
    }, 1000);
  }
}
