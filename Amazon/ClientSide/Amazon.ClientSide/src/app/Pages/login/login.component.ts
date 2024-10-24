import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AccountService } from '../../Services/account.service';
import { CategoryService } from '../../Services/category.service';
import { Subscription } from 'rxjs';
import { jwtDecode } from 'jwt-decode';
import { loginData } from '../../Models/data';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit{

  constructor(public accountService: AccountService, public router: Router) {}

  loginForm: FormGroup;
  UserName = new FormControl('');
  sub: Subscription | null = null;

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      'email': new FormControl(null, [Validators.required, Validators.email]),
      'password': new FormControl(null, Validators.required)
    });
  }

  async onSubmit()
  {
    let { email, password } = this.loginForm.value;
    this.sub = this.accountService.login(email, password).subscribe({
      next: (r: loginData) => {
        console.log(r);
        localStorage.setItem('userName', r.displayName);
        let data: {Email: string, Name: string, Role: string } = jwtDecode(r.token);
        console.log(data.Email);
        console.log(data.Name);
        console.log(data.Role);
        let test: any = jwtDecode(r.token);
        console.log(test);
        localStorage.setItem("token", r.token)
        localStorage.setItem("isAuthenticated", "true")
        localStorage.setItem("Role", data.Role)
        this.accountService.isAuthenticated = true;
        this.router.navigateByUrl('');
      },
      error: e => {
        console.log("You've Entered Something Wrong")
      }
    });
  }
}
