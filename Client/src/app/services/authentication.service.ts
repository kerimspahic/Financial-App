import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { AuthenticationClient } from '../client/authentication.client';
import { Router } from '@angular/router';
import { isPlatformBrowser } from '@angular/common';
import { BehaviorSubject } from 'rxjs';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private tokenKey = 'token';
  private userKey = 'user';
  private currentUserSource = new BehaviorSubject<User | null>(null);
  curentUser$ = this.currentUserSource.asObservable();

  constructor(private authClient: AuthenticationClient, private router: Router, @Inject(PLATFORM_ID) private platformId: object) { }


  public login(username: string, password: string): void {
    this.authClient.login(username, password).subscribe(x => {
      if (isPlatformBrowser(this.platformId)) {
        localStorage.setItem(this.tokenKey, x);
        localStorage.setItem(this.userKey, username);
        const user: User = { userName: username };
        this.currentUserSource.next(user);
      }
      this.router.navigate(['/']);
    });
  }

  public register(username: string, email: string, password: string): void {
    this.authClient.register(username, email, password).subscribe(x => {
      if (isPlatformBrowser(this.platformId)) {
        localStorage.setItem(this.tokenKey, x);
        localStorage.setItem(this.userKey, username);
        const user: User = { userName: username };
        this.currentUserSource.next(user);
      }
      this.router.navigate(['/']);
    });
  }

  public logout() {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.removeItem(this.tokenKey);
      localStorage.removeItem(this.userKey);
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

  public getUser() {
    return this.authClient.getUser();
  }

  public setCurrentUser(user: User) {
    this.currentUserSource.next(user);
  }
}
