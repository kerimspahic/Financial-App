import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { AuthenticationClient } from '../client/authentication.client';
import { Router } from '@angular/router';
import { isPlatformBrowser } from '@angular/common';
import { BehaviorSubject } from 'rxjs';
import { UserTemp } from '../models/user';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Result } from '../models/result.model';
import { User } from '../models/user.model';
import { jwtDecode } from 'jwt-decode';
import { ClaimsEnum } from '../enum/claims.enum';
import { HttpErrorResponse, HttpResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private tokenKey = 'token';
  private userKey = 'user';
  private currentUserSource = new BehaviorSubject<UserTemp | null>(null);
  curentUser$ = this.currentUserSource.asObservable();

  constructor(
    private authClient: AuthenticationClient,
    private router: Router,
    private snackBar: MatSnackBar,
    @Inject(PLATFORM_ID) private platformId: object
  ) {}

  public login(username: string, password: string) {
    this.authClient.login(username, password).subscribe({
      next: (x) => {
        this.handleSuccesAuthentication(x);
      },
      error: (err) => {
        this.handleFailedAuthenticatione(err);
      },
    });
  }

  public register(username: string, email: string, password: string): void {
    this.authClient.register(username, email, password).subscribe({
      next: (x) => {
        this.handleSuccesAuthentication(x);
      },
      error: (err) => {
        this.handleFailedAuthenticatione(err);
      },
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

  public getUser(): User | null {
    const userJson = localStorage.getItem(this.userKey);
    if (userJson) {
      let user: User = JSON.parse(userJson);
      return user;
    }
    return null;
  }

  public isLoggedIn(): boolean {
    const user = this.getUser();
    if (user) {
      return user.token != null && user.token.length > 0;
    }
    return false;
  }

  public getToken(): string | null {
    const user = this.getUser();
    if (user) {
      return user.token;
    }
    return null;
  }

  public setCurrentUser(user: UserTemp) {
    this.currentUserSource.next(user);
  }

  handleSuccesAuthentication(result: Result<string>): void {
    let message;

    if (result !== null && result.isSuccess && result.response.length > 1) {
      const decodedToken = jwtDecode<any>(result.response);
      const user = new User(
        decodedToken[ClaimsEnum.NameTokenKey],
        decodedToken[ClaimsEnum.EmailTokenKey],
        decodedToken[ClaimsEnum.RoleTokenKey],
        result.response
      );
      localStorage.setItem(this.userKey, JSON.stringify(user));

      this.router.navigate(['/']);
      message = 'User has been authenticated.';
    } else if (result !== null && !result.isSuccess) {
      message = result.errors.join(' ');
    } else {
      message = 'Something went wrong';
    }

    this.snackBar.open(message, 'Close');
  }

  handleFailedAuthenticatione(err: HttpErrorResponse): void {
    let errorMessage = [];

    let validationErrorDictionary = JSON.parse(
      JSON.stringify(err.error.errors)
    );
    for (let fieldName in validationErrorDictionary) {
      if (validationErrorDictionary.hasOnwProperty(fieldName)) {
        errorMessage.push(validationErrorDictionary[fieldName]);
      }
    }
    this.snackBar.open(errorMessage.join(' '), 'Close');
  }
}