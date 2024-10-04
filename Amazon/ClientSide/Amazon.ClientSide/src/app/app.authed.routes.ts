import { Routes } from '@angular/router';
import { SearchComponent } from './Pages/Search/search.component';
import { HomeComponent } from './Pages/Home/home.component';

export const App_Routes: Routes = 
[
    { path: '', component: HomeComponent },
    { path: 'search/:productName', component: SearchComponent },
];
