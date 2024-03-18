import { AfterViewInit, Component } from '@angular/core';
import { Observable } from 'rxjs';
import { TransactionsClient } from '../../client/transactions.client';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.css'
})
export class TransactionsComponent implements AfterViewInit {
  public transaction: Observable<any> = this.transactionClient.getTransactionsData();

  constructor (public transactionClient: TransactionsClient) {}

  ngAfterViewInit(): void {
  }


}
