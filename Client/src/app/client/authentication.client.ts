import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root',
})
export class AuthenticationClient {
  currentUser: any;

  baseUrl = 'https://localhost:5001';
  constructor(private http: HttpClient) { }

  public login(username: string, password: string): Observable<string> {
    return this.http.post(
      this.baseUrl + '/User/login',
      {
        username: username,
        password: password,
      },
      { responseType: 'text' }
    );
  }

  public register(username: string, email: string, password: string): Observable<string> {
    return this.http.post(
      this.baseUrl + '/User/register',
      {
        username: username,
        email: email,
        password: password
      },
      { responseType: 'text' }
    );
  }
  public getUser() {
    this.http.get(this.baseUrl + '/User/user').subscribe({
      next: response => this.currentUser = response,
      error: error => console.log(error),
    });
    return this.currentUser;
  }
}
