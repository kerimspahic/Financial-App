import { Injectable } from '@angular/core';
import { TransactionClient } from '../client/transaction.client';
import { Transaction } from '../models/transaction';
import { Observable } from 'rxjs';
import { TransactionDescriptions } from '../models/transactionDescriptions';
import { DashboardCharts } from '../models/dashboardCharts';

@Injectable({
  providedIn: 'root',
})
export class TransactionService {
  constructor(private transactionClient: TransactionClient) {}

  public sendTransactionData(newUserTransaction: Transaction): void {
    this.transactionClient.sendTransactionData(newUserTransaction).subscribe();
  }

  public getTransactionDescriptionData(): Observable<any> {
    return this.transactionClient.getTransactionDesciptionNames();
  }

  public sendTransactionDescriptionData(descriptionName: string): void {
    this.transactionClient.addTransactionDescription(descriptionName).subscribe();
  }

  public deleteTransactionDescriptionData(id : number):void {
    this.transactionClient.deleteTransactionDescription(id).subscribe();
  }

  public editTransactionDescriptionData(newTransactionDescription: TransactionDescriptions){
    this.transactionClient.updateTransactionDescription(newTransactionDescription).subscribe();
  }

  public getDashboardValues(): Observable<any>{
    return this.transactionClient.getDashboardValues();
  }

  public getDashboardCharts(): Observable<DashboardCharts> {
    return this.transactionClient.getDashboardCharts();
  }
}
