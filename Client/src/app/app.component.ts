import { Component } from '@angular/core';
import { AuthenticationService } from './services/authentication.service';
import { FormBuilder } from '@angular/forms';
import { jwtDecode } from 'jwt-decode';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Client';

  constructor(public authService: AuthenticationService, private _formBuilder: FormBuilder) {

  }

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser() {
    if (typeof localStorage === 'undefined') return;
    const tokenString = localStorage.getItem('token');
    if (!tokenString) return;

    this.authService.setCurrentUser(jwtDecode(tokenString));
  }
}
