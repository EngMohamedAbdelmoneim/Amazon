import { Routes } from '@angular/router';
import { LoginComponent } from './Pages/login/login.component';
import { RegisterComponent } from './Pages/register/register.component';
import { ConfirmComponent } from './Components/confirm/confirm.component';
import { SellerRegisterComponent } from './Pages/register-seller/register-seller.component';

export const Auth_Routes: Routes = 
[
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'sellerRegister', component: SellerRegisterComponent },
    { path: 'confirm', component: ConfirmComponent },
];