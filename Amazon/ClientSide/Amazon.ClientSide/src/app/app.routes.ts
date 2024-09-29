import { Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { SearchComponent } from './Pages/Search/search.component';
import { HomeComponent } from './Pages/Home/home.component';

export const routes: Routes = 
[
    { path: '', component: HomeComponent },
    { path: 'search/:id', component: SearchComponent },
];
