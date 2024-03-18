import { Component } from '@angular/core';
import { User } from './models/user';
import { AuthenticationService } from './services/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Client';

  constructor (public authService: AuthenticationService) {

  }

ngOnInit(): void {
    this.setCurrentUser();
 }

 setCurrentUser(){
  const userString = localStorage.getItem('user');
  if (!userString) return;
  const user: User = { userName: userString };
  this.authService.setCurrentUser(user);
 }
}
