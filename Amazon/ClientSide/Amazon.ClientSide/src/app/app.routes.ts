import { Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { SearchComponent } from './Pages/Search/search.component';
import { HomeComponent } from './Pages/Home/home.component';
import { ProductComponent } from './Pages/Product/product.component';
import { setLayout } from './Resolvers/set-layout.resolver';
import { PageLayout } from './Models/PageLayout';

export const routes: Routes = 
[
    { 
        path: '',
        loadChildren: () => import('./app.authed.routes').then(m => m.App_Routes),
        resolve: { layout: setLayout(PageLayout.Authorized)}
    },
    { 
        path: '',
        loadChildren: () => import('./auth.routes').then(m => m.Auth_Routes), 
        resolve: { layout: setLayout(PageLayout.UnAuthorized)}
    },
    { 
        path: 'seller',
        loadChildren: () => import('./seller.routes').then(m => m.Seller_Routes), 
        resolve: { layout: setLayout(PageLayout.Seller)}
    },
];
