import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {
  public user: string | null = null;
  
  constructor(
    public authService: AuthenticationService
  ) {}

  ngOnInit(): void {
  }

  logout(): void {
    this.authService.logout();
  }

  getUser() {

  }
}
