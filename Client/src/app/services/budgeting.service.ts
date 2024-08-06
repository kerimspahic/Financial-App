import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BudgetingClient } from '../client/budgeting.client';
import { FinancialGoals } from '../models/financialGoals';
import { AutomaticTransaction } from '../models/automaticTransaction';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class BudgetingService {
  constructor(
    private budgetingClient: BudgetingClient,
    private toastr: ToastrService
  ) {}

  createFinancialGoal(newFinancialGoal: FinancialGoals): void {
    this.budgetingClient.createFinancialGoal(newFinancialGoal).subscribe({
      next: () => {
        this.toastr.success('Financial goal created successfully!', 'Success');
      },
      error: (error) => {
        const errorMessage = error.error?.Error || 'Failed to create financial goal. Please try again.';
        this.toastr.error(errorMessage, 'Error');
      },
    });
  }

  getFinancialGoals(): Observable<any> {
    return this.budgetingClient.getFinancialGoal();
  }

  updateFinancialGoal(column: string, value: number): void {
    this.budgetingClient.updateFinancialGoal(column, value).subscribe({
      next: () => {
        this.toastr.success('Financial goal updated successfully!', 'Success');
      },
      error: (error) => {
        const errorMessage = error.error?.Error || 'Failed to update financial goal. Please try again.';
        this.toastr.error(errorMessage, 'Error');
      },
    });
  }

  deleteFinancialGoal(): void {
    this.budgetingClient.deleteFinancialGoal().subscribe({
      next: () => {
        this.toastr.success('Financial goal deleted successfully!', 'Success');
      },
      error: (error) => {
        const errorMessage = error.error?.Error || 'Failed to delete financial goal. Please try again.';
        this.toastr.error(errorMessage, 'Error');
      },
    });
  }

  getAutomaticTransactions(): Observable<AutomaticTransaction[]> {
    return this.budgetingClient.getAutomaticTransactions();
  }

  deleteAutomaticTransaction(transactionId: number): any {
    this.budgetingClient.deleteAutomaticTransaction(transactionId).subscribe({
      next: () => {
        this.toastr.success('Automatic transaction deleted successfully!', 'Success');
      },
      error: (error) => {
        const errorMessage = error.error?.Error || 'Failed to delete automatic transaction. Please try again.';
        this.toastr.error(errorMessage, 'Error');
      },
    });
  }
  createAutomaticTransaction(transaction: AutomaticTransaction): any {
    this.budgetingClient.createAutomaticTransaction(transaction).subscribe({
      next: () => {
        this.toastr.success('Automatic transaction created successfully!', 'Success');
      },
      error: (error) => {
        const errorMessage = error.error?.Error || 'Failed to create automatic transaction. Please try again.';
        this.toastr.error(errorMessage, 'Error');
      },
    });
  }
}