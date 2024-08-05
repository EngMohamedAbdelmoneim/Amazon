import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  constructor(private router:Router){}

  query: string;

  search = () => {
    this.query = (<HTMLInputElement>document.getElementById("search"))?.value;
    this.router.navigateByUrl(`/search/${this.query}`);
    console.log(window.location.href)
  }
}
