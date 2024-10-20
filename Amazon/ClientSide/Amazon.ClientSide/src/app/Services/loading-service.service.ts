import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {
  private loadingSubject = new BehaviorSubject<boolean>(false); // Start with false
  public loading$: Observable<boolean> = this.loadingSubject.asObservable(); // Expose as observable

  show() {
    this.loadingSubject.next(true); // Set loading to true
  }

  hide() {
    this.loadingSubject.next(false); // Set loading to false
  }
}
