import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RouterModule } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AccountService {

  constructor(public http: HttpClient) { }

  isAuthenticated: boolean = JSON.parse(localStorage.getItem("isAuthenticated"));

  // Role: string = JSON.parse(localStorage.getItem("Role"));

  login(email: string, password: string)
  {
    return this.http.post("https://localhost:7283/api/Account/Login/", { 'email': email, 'password': password })
  }
  
  register(email: string, displayName: string, phoneNumber: string, password: string, confirmPassword: string)
  {
    return this.http.post("https://localhost:7283/api/Account/register/", 
      { 
        'email': email,
        'displayName': displayName, 
        'phoneNumber': phoneNumber, 
        'password': password, 
        'confirmPassword': confirmPassword 
      }
    )
  }
  sellerRegister(sellerName:string,email: string, displayName: string, phoneNumber: string, password: string, confirmPassword: string)
  {
    return this.http.post(`https://localhost:7283/api/Account/SellerRegister?sellerName=${sellerName}`, 
      { 
        'email': email,
        'displayName': displayName, 
        'phoneNumber': phoneNumber, 
        'password': password, 
        'confirmPassword': confirmPassword 
      }
    )
  }


}
