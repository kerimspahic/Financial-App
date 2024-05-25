import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { AuthenticationClient } from '../client/authentication.client';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { isPlatformBrowser } from '@angular/common';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  baseUrl = environment.baseUrl;

  private tokenKey = 'token';
  public decodedToken: string | null = null;
  private currentUserSource = new BehaviorSubject<any>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(
    private authClient: AuthenticationClient,
    private router: Router,
    @Inject(PLATFORM_ID) private platformId: object
  ) {}

  public login(username: string, password: string) {
    this.authClient.login(username, password).subscribe((x) => {
      if (isPlatformBrowser(this.platformId)) {
        localStorage.setItem(this.tokenKey, x);
        this.decodedToken = jwtDecode(x);
        this.currentUserSource.next(this.decodedToken);
      }
      this.router.navigate(['/dashboard']);
    });
  }

  public register(username: string, email: string, firstName: string, lastName: string, password: string, confirmPassword: string): void {
    this.authClient.register(username, email, firstName, lastName, password,confirmPassword).subscribe((x) => {

        this.router.navigate(['/registration-success']);
      });
  }

  public logout() {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.removeItem(this.tokenKey);
      this.currentUserSource.next(null);
    }
    this.router.navigate(['/login']);
  }

  public isLoggedIn(): boolean {
    if (isPlatformBrowser(this.platformId)) {
      let token = localStorage.getItem(this.tokenKey);
      return token != null && token.length > 0;
    }
    return false;
  }

  public getToken(): string | null {
    if (isPlatformBrowser(this.platformId)) {
      return localStorage.getItem(this.tokenKey);
    }
    return null;
  }
  
  public setCurrentUser(user: any) {
    this.currentUserSource.next(user);
  }
}
