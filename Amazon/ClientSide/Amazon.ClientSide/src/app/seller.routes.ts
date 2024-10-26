import { Routes } from '@angular/router';
import { SellerProductListComponent } from './Pages/seller-product-list/seller-product-list.component';
import { SellerAddProductComponent } from './Pages/seller-add-product/seller-add-product.component';
import { SellerEditProductComponent } from './Pages/seller-edit-product/seller-edit-product.component';
import { SellerProductDetailsComponent } from './Pages/seller-product-details/seller-product-details.component';
import { sellerGuard } from './guards/seller.guard';
import { PageNotFoundComponent } from './Pages/page-not-found/page-not-found.component';

export const Seller_Routes: Routes = 
[
    { path: 'product-list', component: SellerProductListComponent , canActivate: [sellerGuard]},
    { path: 'add-product', component: SellerAddProductComponent },
    { path: 'edit-product/:product', component: SellerEditProductComponent },
    { path: 'product-details/:id', component: SellerProductDetailsComponent },
    { path: '**',pathMatch:'full', component:PageNotFoundComponent},
];