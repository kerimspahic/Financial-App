import { Injectable } from '@angular/core';
import { TransactionClient } from '../client/transaction.client';
import { Transaction } from '../models/transaction';
import { Observable } from 'rxjs';
import { TransactionDescriptions } from '../models/transactionDescriptions';

@Injectable({
  providedIn: 'root',
})
export class TransactionService {
  constructor(private transactionClient: TransactionClient) {}

  public sendTransactionData(newUserTransaction: Transaction): void {
    this.transactionClient.sendTransactionData(newUserTransaction).subscribe();
  }

  public getTransactionDescriptionData(): Observable<TransactionDescriptions[]> {
    return this.transactionClient.getTransactionDescriptionNames();
  }

  public sendTransactionDescriptionData(descriptionName: string, descriptionType: boolean): void {
    this.transactionClient.addTransactionDescription(descriptionName, descriptionType).subscribe();
  }

  public deleteTransactionDescriptionData(id: number): void {
    this.transactionClient.deleteTransactionDescription(id).subscribe();
  }

  public editTransactionDescriptionData(newTransactionDescription: TransactionDescriptions): void {
    this.transactionClient.updateTransactionDescription(newTransactionDescription).subscribe();
  }

  public getDashboardValues(): Observable<any> {
    return this.transactionClient.getDashboardValues();
  }

  public getCardValues(mode: string): Observable<any> {
    return this.transactionClient.getCardValues(mode);
  }

  public getChartData(type: string): Observable<any> {
    return this.transactionClient.getChartData(type);
  }

  public getFinancialGoals(mode: string): Observable<any> {
    return this.transactionClient.getFinancialGoals(mode);
  }
}
