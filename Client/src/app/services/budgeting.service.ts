import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BudgetingClient } from '../client/budgeting.client';
import { FinancialGoals } from '../models/financialGoals';
import { AutomaticTransaction } from '../models/automaticTransaction';

@Injectable({
  providedIn: 'root',
})
export class BudgetingService {
  constructor(private budgetingClient: BudgetingClient) {}

  createFinancialGoal(newFinancialGoal: FinancialGoals): void {
    this.budgetingClient.createFinancialGoal(newFinancialGoal).subscribe();
  }

  getFinancialGoals(): Observable<any> {
    return this.budgetingClient.getFinancialGoal();
  }

  updateFinancialGoal(column: string, value: number): void {
    this.budgetingClient.updateFinancialGoal(column, value).subscribe();
  }

  deleteFinancialGoal(): Observable<any> {
    return this.budgetingClient.deleteFinancialGoal();
  }

  getAutomaticTransactions(): Observable<AutomaticTransaction[]> {
    return this.budgetingClient.getAutomaticTransactions();
  }

  deleteAutomaticTransaction(transactionId: number): Observable<any> {
    return this.budgetingClient.deleteAutomaticTransaction(transactionId);
  }

  createAutomaticTransaction(transaction: AutomaticTransaction): Observable<any> {
    return this.budgetingClient.createAutomaticTransaction(transaction);
  }
}

