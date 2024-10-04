import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from './core/nav/nav.component';
import { FooterComponent } from './core/footer/footer.component';
import { PageLayoutService } from './Services/page-layout.service';
import { PageLayout } from './Models/PageLayout';
import { AuthorizedLayoutComponent } from './core/authorized-layout/authorized-layout.component';
import { UnAuthorizedLayoutComponent } from './core/un-authorized-layout/un-authorized-layout.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, NavComponent, FooterComponent, AuthorizedLayoutComponent, UnAuthorizedLayoutComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  title = 'Amazon';
  readonly PageLayout = PageLayout;
  constructor(public pageLayoutService: PageLayoutService){}

  ngOnInit(): void 
  {
    // this.pageLayoutService.setLayout(PageLayout.Authorized)
  }

  
}
