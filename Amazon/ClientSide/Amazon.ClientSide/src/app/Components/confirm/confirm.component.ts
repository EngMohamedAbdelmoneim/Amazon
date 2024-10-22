import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-confirm',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './confirm.component.html',
  styleUrl: './confirm.component.css'
})
export class ConfirmComponent implements OnInit
{
  constructor(private route: ActivatedRoute, private http: HttpClient, private router: Router) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      const userid = params['userid'];
      const token = params['token'];

      this.http.get(`https://localhost:7283/api/Account/confirmemail?userid=${userid}&token=${token}`)
      .subscribe({
        next: res => {
          console.log(res);
        }
      })
    })
  }


}
