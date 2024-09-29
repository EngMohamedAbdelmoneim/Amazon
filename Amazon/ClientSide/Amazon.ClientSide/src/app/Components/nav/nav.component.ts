import { CommonModule } from '@angular/common';
import { Component, signal, ViewEncapsulation } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';
import { sign } from 'crypto';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css',
  encapsulation: ViewEncapsulation.Emulated
})
export class NavComponent {
  constructor(private router:Router){}

  query: string;
  open:boolean = false;

  toggleMenu() 
  {
    const body = document.body;

    if(this.open)
    {
      body.style.position = '';
      body.style.top = '';
      body.style.height = '';
      body.style.overflowY = '';
    }
    else
    {
      body.style.height = '100vh';
      body.style.overflowY = 'hidden';
    }
    this.open = !this.open;
  }
  
  search = (q: string) => {
    console.log(q);
    this.router.navigateByUrl(`/search/${q}`);
    console.log(window.location.href)
  }
}