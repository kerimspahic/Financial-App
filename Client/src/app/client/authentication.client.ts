import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Result } from '../models/result.model';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationClient {
  currentUser: any;

  baseUrl = 'https://localhost:5001';
  constructor(private http: HttpClient) {}

  public login(username: string, password: string): Observable<Result<string>> {
    return this.http.post<Result<string>>(this.baseUrl + '/User/login', {
      username: username,
      password: password,
    });
  }

  public register(
    username: string, email: string, password: string): Observable<Result<string>> {
    return this.http.post<Result<string>>(this.baseUrl + '/User/register', {
      username: username,
      email: email,
      password: password,
    });
  }
  public getUser() {
    this.http.get(this.baseUrl + '/User/user').subscribe({
      next: (response) => (this.currentUser = response),
      error: (error) => console.log(error),
    });
    return this.currentUser;
  }
}
