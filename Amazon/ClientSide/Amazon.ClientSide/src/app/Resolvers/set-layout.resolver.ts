import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { PageLayout } from '../Models/PageLayout';
import { inject } from '@angular/core';
import { PageLayoutService } from '../Services/page-layout.service';

export const setLayout = (inputLayout: PageLayout): ResolveFn<void> => {
  return (_route: ActivatedRouteSnapshot, _state: RouterStateSnapshot) => {
    inject(PageLayoutService).setLayout(inputLayout);
  };
}