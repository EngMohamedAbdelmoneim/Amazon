import { CanActivateFn } from '@angular/router';
import { AccountService } from '../Services/account.service';
import { inject } from '@angular/core';

export const canloginGuard: CanActivateFn = (route, state) => {
  let accountService: AccountService = inject(AccountService);
  if(accountService.isAuthenticated)
    return true;
  return false;
};
