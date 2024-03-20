import { Injectable } from '@angular/core';
import { TransactionsClient } from '../client/transactions.client';

@Injectable({
  providedIn: 'root'
})
export class TransactionsService {

  constructor(private transClient: TransactionsClient) { }

  public insertTransactionData(amount: number, type: boolean, date: string, description: string, userName: string): void {

    this.transClient.insertTransactionData(amount, type, date, description, userName).subscribe();
  }
}
