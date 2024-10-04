import { Component } from '@angular/core';

@Component({
  selector: 'app-un-authorized-layout',
  standalone: true,
  imports: [],
  // templateUrl: './un-authorized-layout.component.html',
  template: `<ng-content />`,
  styleUrl: './un-authorized-layout.component.css'
})
export class UnAuthorizedLayoutComponent {

}
