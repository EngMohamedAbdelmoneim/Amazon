import { Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { SearchComponent } from './Pages/Search/search.component';
import { HomeComponent } from './Pages/Home/home.component';
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
        path: 'auth',
        loadChildren: () => import('./auth.routes').then(m => m.Auth_Routes), 
        resolve: { layout: setLayout(PageLayout.UnAuthorized)}
    },
];
