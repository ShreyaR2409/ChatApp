import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';

export const authGuard: CanActivateFn = (route, state) => {
  const accessToken = sessionStorage.getItem('Token');
  const router = inject(Router);

  if (accessToken) {
    try {
      const decodedToken: any = jwtDecode(accessToken);
      const currentTime = Math.floor(Date.now() / 1000);

      if (decodedToken.exp > currentTime) {
        return true; 
      } else {
        router.navigate(['/login']);
        return false;
      }
    } catch (error) {
      console.error('Error decoding token:', error);
      alert("Invalid Token")
      router.navigate(['/login']);
      return false;
    }
  } else {
    router.navigate(['/login']);
    return false;
  }
};
