import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FinancialGoals } from '../models/financialGoals';
import { AutomaticTransaction } from '../models/automaticTransaction';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class BudgetingClient {
  constructor(private http: HttpClient) {}

  public createFinancialGoal(newFinancialGoal: FinancialGoals): Observable<Object> {
    return this.http.post(environment.financialGoalUrl + 'CreateFinancialGoal', newFinancialGoal);
  }

  public getFinancialGoal(): Observable<any> {
    return this.http.get<any>(environment.financialGoalUrl + 'GetFinancialGoals');
  }

  public updateFinancialGoal(column: string, value: number): Observable<any> {
    return this.http.patch<any>(`${environment.financialGoalUrl}UpdateFinancialGoalColumn?column=${column}&newValue=${value}`, {});
  }

  public deleteFinancialGoal(): Observable<any> {
    return this.http.delete(environment.financialGoalUrl + 'DeleteFinancialGoal');
  }

  public getAutomaticTransactions(): Observable<AutomaticTransaction[]> {
    return this.http.get<AutomaticTransaction[]>(environment.automaticTransactionUrl + 'GetAutomaticTransactions');
  }

  public deleteAutomaticTransaction(transactionId: number): Observable<any> {
    return this.http.delete(`${environment.automaticTransactionUrl}DeleteAutomaticTransaction?id=${transactionId}`);
  }

  public createAutomaticTransaction(transaction: AutomaticTransaction): Observable<any> {
    return this.http.post(environment.automaticTransactionUrl + 'CreateAutomaticTransaction', transaction);
  }
}