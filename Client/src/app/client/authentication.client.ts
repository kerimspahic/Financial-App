import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationClient {
  constructor(private http: HttpClient) {}

  public login(username: string, password: string): Observable<string> {
    return this.http.post(
      environment.accountUrl + 'Login',
      {
        username: username,
        password: password,
      },
      { responseType: 'text' }
    );
  }

  public register(username: string, email: string, firstName: string, lastName: string, password: string, confirmPassword: string): Observable<string> {
    return this.http.post(
      environment.accountUrl + 'Register',
      {
        username: username,
        email: email,
        firstName: firstName,
        lastName: lastName,
        password: password,
        confirmPassword: confirmPassword
      },
      { responseType: 'text' }
    );
  }
}
