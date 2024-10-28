import { Routes } from '@angular/router';
import { SellerProductListComponent } from './Pages/Seller/SellerProductList/seller-product-list/seller-product-list.component';
import { SellerAddProductComponent } from './Pages/Seller/seller-add-product/seller-add-product.component';
import { SellerEditProductComponent } from './Pages/Seller/seller-edit-product/seller-edit-product.component';
import { SellerProductDetailsComponent } from './Pages/Seller/seller-product-details/seller-product-details.component';
import { sellerGuard } from './guards/seller.guard';
import { PageNotFoundComponent } from './Pages/page-not-found/page-not-found.component';
import {
  VerifiedProductsComponent
} from "./Pages/Seller/SellerProductList/verified-products/verified-products.component";
import {
  UnverifiedProductsComponent
} from "./Pages/Seller/SellerProductList/unverified-products/unverified-products.component";
import { SellerProductsEarningsDetailsComponent } from './Pages/Seller/SellerProductList/seller-products-earnings-details/seller-products-earnings-details.component';

export const Seller_Routes: Routes =
[
    { path: 'product-list', component: SellerProductListComponent , canActivate: [sellerGuard]},
    { path: 'verified-product-list', component: VerifiedProductsComponent , canActivate: [sellerGuard]},
    { path: 'unverified-product-list', component: UnverifiedProductsComponent , canActivate: [sellerGuard]},
    { path: 'seller-products-earnings-details', component: SellerProductsEarningsDetailsComponent , canActivate: [sellerGuard]},
    { path: 'add-product', component: SellerAddProductComponent },
    { path: 'edit-product/:product', component: SellerEditProductComponent },
    { path: 'product-details/:id', component: SellerProductDetailsComponent },
    { path: '**',pathMatch:'full', component:PageNotFoundComponent},
];
