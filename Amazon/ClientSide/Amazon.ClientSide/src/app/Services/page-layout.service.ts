import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { PageLayout } from '../Models/PageLayout';

@Injectable({
  providedIn: 'root'
})
export class PageLayoutService {

  constructor() { }

  private layoutSubject = new Subject<PageLayout>();

  public layout$ = this.layoutSubject.asObservable();

  setLayout(value: PageLayout)
  {
    this.layoutSubject.next(value);
  }
}
