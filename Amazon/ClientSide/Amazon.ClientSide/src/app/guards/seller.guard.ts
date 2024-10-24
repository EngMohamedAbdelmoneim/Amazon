import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../Services/account.service';
import { inject } from '@angular/core';

export const sellerGuard: CanActivateFn = (route, state) => {
  let accountService: AccountService = inject(AccountService);
  console.log('here')
  const role = localStorage.getItem('Role');
  console.log(role)
  let test;
  if(role.includes('Seller'))
    return true;

  let router:Router=inject(Router);
  router.navigateByUrl("/sellerRegister");
  return false;
};
