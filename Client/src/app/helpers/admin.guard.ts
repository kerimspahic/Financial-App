import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';
import { map } from 'rxjs';

export const adminGuard: CanActivateFn = (route, state) => {
  const authenticationService = inject(AuthenticationService);

  return authenticationService.currentUser$.pipe(
    map((user) => {
      if (user && user.role === 'Admin') {
        return true;
      } else return false;
    })
  );
};
