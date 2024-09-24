import { Injectable } from '@angular/core';
import { TransactionClient } from '../client/transaction.client';
import { Transaction } from '../models/transaction';
import { Observable } from 'rxjs';
import { TransactionDescriptions } from '../models/transactionDescriptions';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class TransactionService {
  constructor(
    private transactionClient: TransactionClient,
    private toastr: ToastrService
  ) {}

  public sendTransactionData(newUserTransaction: Transaction): void {
    this.transactionClient.sendTransactionData(newUserTransaction).subscribe({
      next: () => {
        this.toastr.success('Transaction data sent successfully!', 'Success');
      },
      error: (error) => {
        const errorMessage = error.error?.Error || 'Failed to send transaction data. Please try again.';
        this.toastr.error(errorMessage, 'Error');
      },
    });
  }

  public deleteAppUser(userId: string): Observable<void> {
    return this.transactionClient.deleteUser(userId);
  }

  public getAppUsersData(): Observable<any[]> {
    return this.transactionClient.getAppUsers(); // Assumes this method exists in your client service
  }

  public getTransactionDescriptionData(): Observable<TransactionDescriptions[]> {
    return this.transactionClient.getTransactionDescriptionNames();
  }

  public sendTransactionDescriptionData(descriptionName: string, descriptionType: boolean): void {
    this.transactionClient.addTransactionDescription(descriptionName, descriptionType).subscribe({
      next: () => {
        this.toastr.success('Transaction description added successfully!', 'Success');
      },
      error: (error) => {
        const errorMessage = error.error?.Error || 'Failed to add transaction description. Please try again.';
        this.toastr.error(errorMessage, 'Error');
      },
    });
  }

  public deleteTransactionDescriptionData(id: number): void {
    this.transactionClient.deleteTransactionDescription(id).subscribe({
      next: () => {
        this.toastr.success('Transaction description deleted successfully!', 'Success');
      },
      error: (error) => {
        const errorMessage = error.error?.Error || 'Failed to delete transaction description. Please try again.';
        this.toastr.error(errorMessage, 'Error');
      },
    });
  }

  public editTransactionDescriptionData(newTransactionDescription: TransactionDescriptions): void {
    this.transactionClient.updateTransactionDescription(newTransactionDescription).subscribe({
      next: () => {
        this.toastr.success('Transaction description updated successfully!', 'Success');
      },
      error: (error) => {
        const errorMessage = error.error?.Error || 'Failed to update transaction description. Please try again.';
        this.toastr.error(errorMessage, 'Error');
      },
    });
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

  public sendWeeklySummaryEmail(): Observable<void> {
    return this.transactionClient.sendWeeklySummary();
  }
}
