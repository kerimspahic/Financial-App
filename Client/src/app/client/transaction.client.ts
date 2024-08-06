import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { Transaction } from '../models/transaction';
import { TransactionDescriptions } from '../models/transactionDescriptions';
import { LoaderService } from '../services/loader.service';
import { DashboardValues } from '../models/dashboardValues';

@Injectable({
  providedIn: 'root',
})
export class TransactionClient {
  constructor(private http: HttpClient) {}

  public getTransactionData(
    pageNumber: number,
    pageSize: number,
    sortBy: string,
    isDescending: boolean,
    filters: any
  ): Observable<any> {
    const params: any = {
      pageNumber,
      pageSize,
      sortBy,
      isDescending: isDescending.toString(),
    };
    if (filters.transactionAmount !== null) {
      params.transactionAmount = filters.transactionAmount;
    }
    if (filters.transactionType !== null) {
      params.transactionType = filters.transactionType;
    }
    if (filters.transactionDescription !== null) {
      params.transactionDescription = filters.transactionDescription;
    }
    return this.http.get(environment.transactionUrl + `GetUserTransactions`, {
      params,
    });
  }

  public sendTransactionData(
    newUserTransaction: Transaction
  ): Observable<Object> {
    return this.http.post(
      environment.transactionUrl + 'SetNewTransaction',
      newUserTransaction
    );
  }

  public addTransactionDescription(
    descriptionName: string,
    descriptionType: boolean
  ): Observable<any> {
    return this.http.post(
      environment.adminTransactionUrl + 'SetTransactionDescription',
      { descriptionName, descriptionType }
    );
  }

  public getTransactionDescriptionNames(): Observable<any> {
    return this.http.get<any>(
      environment.adminTransactionUrl + 'GetTransactionDescriptions'
    );
  }

  public deleteTransactionDescription(id: number): Observable<any> {
    return this.http.delete(
      `${environment.adminTransactionUrl}DeleteTransactionDescription?id=${id}`
    );
  }

  public updateTransactionDescription(
    newTransactionDescription: TransactionDescriptions
  ): Observable<any> {
    return this.http.put(
      `${environment.adminTransactionUrl}UpdateTransactionDescription?id=${newTransactionDescription.id}&descriptionName=${newTransactionDescription.descriptionName}&descriptionType=${newTransactionDescription.descriptionType}`,
      {}
    );
  }

  public removeTransaction(id: number): Observable<any> {
    return this.http.delete(`${environment.transactionUrl}Remove?id=${id}`);
  }

  public updateTransaction(transaction: Transaction): Observable<any> {
    return this.http.put(
      environment.transactionUrl + 'UpdateTransaction',
      transaction
    );
  }

  public getDashboardValues(): Observable<DashboardValues> {
    return this.http.get<DashboardValues>(
      environment.transactionUrl + 'GetDashboardValues'
    );
  }

  public getCardValues(mode: string): Observable<any> {
    return this.http.get<any>(
      `${environment.transactionCalculationsUrl}GetCardValues`,
      { params: { mode } }
    );
  }

  public getChartData(type: string): Observable<any> {
    return this.http.get<any>(
      `${environment.transactionCalculationsUrl}GetChartValues`,
      { params: { type } }
    );
  }

  public getFinancialGoals(mode: string): Observable<any> {
    return this.http.get<any>(
      environment.financialGoalUrl + 'GetFinancialGoals',
      {
        params: { mode },
      }
    );
  }
}
