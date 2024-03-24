import { Component } from '@angular/core';
import { User } from './models/user';
import { AuthenticationService } from './services/authentication.service';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  options = this._formBuilder.group({
    bottom: 0,
    fixed: false,
    top: 0,
  });
  title = 'Client';

  constructor(public authService: AuthenticationService, private _formBuilder: FormBuilder) {

  }

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser() {
    if (typeof localStorage === 'undefined') return;
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user: User = { userName: userString };
    this.authService.setCurrentUser(user);
  }
}
