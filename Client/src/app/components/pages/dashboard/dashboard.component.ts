import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../../services/authentication.service';
import { Observable } from 'rxjs';
import { TransactionService } from '../../../services/transaction.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {
  dashboardValues$!: Observable<any>;

  constructor(public authService: AuthenticationService, public transactionService: TransactionService) { }


  ngOnInit(): void { 
    this.dashboardValues$= this.transactionService.getDashboardValues();
  }

  logout(): void {
    this.authService.logout();
  }
}