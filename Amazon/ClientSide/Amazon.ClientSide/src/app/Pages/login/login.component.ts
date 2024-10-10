import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AccountService } from '../../Services/account.service';
import { CategoryService } from '../../Services/category.service';
import { Subscription } from 'rxjs';

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
      next: r => {
        console.log(r);
        this.router.navigateByUrl('');
      },
      error: e => {
        console.log("You've Entered Something Wrong")
      }
    });
  }
}
