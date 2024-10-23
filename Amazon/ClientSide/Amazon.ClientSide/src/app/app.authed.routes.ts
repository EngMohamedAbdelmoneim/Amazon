import { Routes } from '@angular/router';
import { SearchComponent } from './Pages/Search/search.component';
import { HomeComponent } from './Pages/Home/home.component';
import { CategoryComponent } from './Pages/category/category.component';
import { ProductComponent } from './Pages/Product/product.component';
import { CartComponent } from './Pages/cart/cart.component';
import { WishListComponent } from './Pages/wish-list/wish-list.component';
import { OrderComponent } from './Pages/order/order.component';
import { canloginGuard } from './guards/canlogin.guard';
import { AddReviewComponent } from './Pages/add-review/add-review.component';
import { ManageAddressBookComponent } from './Pages/address-form/manage-address-book/manage-address-book.component';
import { PageNotFoundComponent } from './Pages/page-not-found/page-not-found.component';


export const App_Routes: Routes = 
[
    { path: '', component: HomeComponent },
    { path: 'search/:productName', component: SearchComponent },
    { path: 'category/:ParentCategoryName', component: CategoryComponent },
    { path: 'category/:ParentCategoryName/:categoryName', component: CategoryComponent },
    { path: 'order', component: OrderComponent, canActivate:[canloginGuard]},
    { path: 'product/:id',component:ProductComponent},
    { path: 'cart/:cartId',component:CartComponent},
    { path: 'wishlist/:wishlistId',component:WishListComponent},
    { path: 'review/:id/:pname',component:AddReviewComponent},
    { path: 'review/:id/:pname/:edit',component:AddReviewComponent},
    { path: 'manage-address-book', component: ManageAddressBookComponent },
    // { path: '**',pathMatch:'full', component:PageNotFoundComponent},
];
