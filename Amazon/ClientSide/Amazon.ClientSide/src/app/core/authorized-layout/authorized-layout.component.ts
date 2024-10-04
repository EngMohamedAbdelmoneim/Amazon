import { Component } from '@angular/core';
import { NavComponent } from '../nav/nav.component';
import { FooterComponent } from '../footer/footer.component';

@Component({
  selector: 'app-authorized-layout',
  standalone: true,
  imports: [NavComponent, FooterComponent],
  // templateUrl: './authorized-layout.component.html',
  template: `<app-nav/><ng-content /><app-footer/>`,
  styleUrl: './authorized-layout.component.css'
})
export class AuthorizedLayoutComponent {

}
