import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../../services/authentication.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {
  
  constructor(public authService: AuthenticationService) { }


  ngOnInit(): void { }

  logout(): void {
    this.authService.logout();
  }
}