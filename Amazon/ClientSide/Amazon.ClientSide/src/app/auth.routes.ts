import { Routes } from '@angular/router';
import { LoginComponent } from './Pages/login/login.component';
import { RegisterComponent } from './Pages/register/register.component';
import { ConfirmComponent } from './Components/confirm/confirm.component';

export const Auth_Routes: Routes = 
[
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'confirm', component: ConfirmComponent },
];