import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { Subscription } from 'rxjs';
import { AccountService } from '../../Services/account.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule, RouterModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit{

  constructor(public accountService: AccountService, public router: Router) {}

  sub: Subscription | null = null;
  isMatched: boolean = true;
  isValidated: boolean = false;

  registerForm: FormGroup;
  UserName = new FormControl('');

  ngOnInit(): void {
    this.registerForm = new FormGroup({
      'email': new FormControl(null, [Validators.required, Validators.email]),
      'username': new FormControl(null, Validators.required),
      'phonenumber': new FormControl(null, [Validators.required, Validators.minLength(11), Validators.maxLength(11), Validators.pattern('^[0-9]*$')]),
      'password': new FormControl(null, Validators.required),
      'confirmpassword': new FormControl(null, Validators.required)
    });
  }

  onSubmit()
  {
    let { email, username, phonenumber, password, confirmpassword } = this.registerForm.value;
    this.validate(password, confirmpassword);
    if(this.isValidated)
    {
      this.sub = this.accountService.register(email, username, phonenumber, password, confirmpassword).subscribe({
        next: r => {
          console.log(r);
        },
        error: e => {
          console.log(e);
        }
      })
    }
  }


  validate(password: string, confirmPassword: string)
  {
    if(password === confirmPassword)
    {
      this.isValidated = true;
    }
    else
    {
      this.isMatched = false;
    }
  }
  validateNumberInput(input)
  {

  }
}
