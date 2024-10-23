import { HttpClient } from '@angular/common/http';
import { Component, model } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { CategoryService } from '../../Services/category.service';
import { BrandService } from '../../Services/brand.service';
import { ProductService } from '../../Services/product.service';
import { Discount } from '../../Models/Discount';
import { CommonModule } from '@angular/common';
import { ProductDto } from '../../Models/productDto';
@Component({
  selector: 'app-seller-add-product',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, RouterModule, CommonModule],
  templateUrl: './seller-add-product.component.html',
  styleUrl: './seller-add-product.component.css'
})
export class SellerAddProductComponent {

  productForm: FormGroup;
  product: ProductDto | null =  new ProductDto('',0,null,null,0,0,0,null,[]);
  discount: Discount | null = new Discount(0, 0, false, null, null);

  categories = [];  
  brands = [];     
  mainImage: File = null;
  additionalImages: File[] = [];

  constructor(private fb: FormBuilder,private router: Router, public productService: ProductService, private http: HttpClient, public categoryService: CategoryService, public brandService: BrandService) { }

  ngOnInit(): void {
    this.productForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      price: [0, Validators.required],
      quantityInStock: [0, Validators.required],
      brandId: ['', Validators.required],
      categoryId: ['', Validators.required],
      imageFile: [null, Validators.required],
      imagesFiles: [null],
      discount: null
    });

    this.loadCategories();
    this.loadBrands();
  }


  addDiscount() {
    if (this.discount.discountPercentage < 0 || this.discount.discountPercentage > 1) {
      alert('Discount percentage must be between 0 and 1.');
      return;
    }

    if (new Date(this.discount.startDate) > new Date(this.discount.endDate)) {
      alert('Start date cannot be later than end date.');
      return;
    }


    this.discount = this.productForm.get('discount')?.value || this.discount;

    console.log('Discount added:', this.discount);
  }

  // Method to reset the form values
  resetForm() {
    this.discount  = {
      priceAfterDiscount:0,
      discountPercentage: 0.0,
      startDate: null,
      endDate: null,
      discountStarted: false
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
      const formData = new FormData();
      
      formData.append('Name', this.productForm.get('name')?.value);
      formData.append('Description', this.productForm.get('description')?.value);
      formData.append('Price', this.productForm.get('price')?.value);
      formData.append('QuantityInStock', this.productForm.get('quantityInStock')?.value);
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
      this.discount = this.productForm.get('discount')?.value;
      if (this.discount) {
        formData.append('Discount.discountPercentage', this.discount.discountPercentage.toString());
        formData.append('Discount.discountStarted', this.discount.discountStarted.toString());
        formData.append('Discount.priceAfterDiscount', this.discount.priceAfterDiscount.toString());
        formData.append('Discount.startDate', this.discount.startDate.toString());
        formData.append('Discount.endDate', this.discount.endDate.toString());
      }     

    this.product.BrandId = Number(this.product.BrandId);
    this.product.CategoryId = Number(this.product.CategoryId);

      this.product.ImageFile = this.mainImage;
      this.product.ImagesFiles = this.additionalImages;
  
      console.log('Discount:', this.discount);
      console.log('FormData:', this.product);
  
      this.productService.AddProduct(formData).subscribe({
        next: data => {
          console.log('Product added successfully:', data);
        },
        error: error => {
          console.error('Error adding product:', error);
        }
      });
      this.router.navigate(['seller/product-list'])
    }
  
  
}
