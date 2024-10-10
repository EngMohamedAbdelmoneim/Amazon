import { Routes } from '@angular/router';
import { LoginComponent } from './Pages/login/login.component';
import { RegisterComponent } from './Pages/register/register.component';

export const Auth_Routes: Routes = 
[
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
];
