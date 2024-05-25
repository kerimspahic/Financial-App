import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BudgetingClient } from '../client/budgeting.client';
import { FinancialGoals } from '../models/financialGoals';

@Injectable({
  providedIn: 'root',
})
export class BudgetingService {
  constructor(private budgetingCilent: BudgetingClient) {}

  createFinancialGoal(newFinancialGoal: FinancialGoals): void {
    this.budgetingCilent.createFinancialGoal(newFinancialGoal).subscribe();
  }

  getFinancialGoals(): Observable<any> {
    return this.budgetingCilent.getFinancialGoal();
  }

  updateFinancialGoal(column: string, value: number): void {
    this.budgetingCilent.updateFinancialGoal(column, value).subscribe();
  }

  deleteFinancialGoal(): Observable<any> {
    return this.budgetingCilent.deleteFinancialGoal();
  }
}
