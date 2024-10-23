import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{
  constructor(private router: Router){}
  sub: Subscription;
  ngOnInit(): void 
  {
    try 
    {
      const currentUrl = this.router.url;
      const token = currentUrl.split('token=')[1];
      let decodedToken: { Role: string, Email: string, Name: string } = jwtDecode(token);
      localStorage.setItem('Name', decodedToken.Name);
      localStorage.setItem('Email', decodedToken.Email);
      localStorage.setItem('Role', decodedToken.Role);
      localStorage.setItem("token", token)
      localStorage.setItem("isAuthenticated", "true")
    }
    catch (error) 
    {
    }
  }
}
