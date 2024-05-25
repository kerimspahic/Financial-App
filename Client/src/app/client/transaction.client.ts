import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { Transaction } from '../models/transaction';
import { TransactionDescriptions } from '../models/transactionDescriptions';
import { LoaderService } from '../services/loader.service';
import { DashboardValues } from '../models/dashboardValues';
import { DashboardCharts } from '../models/dashboardCharts';

@Injectable({
  providedIn: 'root',
})
export class TransactionClient {
  constructor(private http: HttpClient, private loaderService: LoaderService) {}

  public getTransactionData(
    pageNumber: number,
    pageSize: number
  ): Observable<any> {
    let transactionUrl = environment.transactionUrl + `GetUserTransactions?`;

    if (pageNumber !== null) transactionUrl += `PageNumber=${pageNumber}&`;

    if (pageSize !== null) transactionUrl += `PageSize=${pageSize}&`;

    return this.loaderService.wrapHttpRequest(this.http.get(transactionUrl));
  }

  public sendTransactionData(
    newUserTransaction: Transaction
  ): Observable<Object> {
    return this.loaderService.wrapHttpRequest(
      this.http.post(
        environment.transactionUrl + 'SetNewTransaction',
        newUserTransaction
      )
    );
  }

  //1

  public addTransactionDescription(descriptionName: string): Observable<any> {
    return this.loaderService.wrapHttpRequest(
      this.http.post(
        environment.adminTransactionUrl + 'SetTransactionDescription',
        { descriptionName }
      )
    );
  }

  public getTransactionDesciptionNames(): Observable<any> {
    return this.loaderService.wrapHttpRequest(
      this.http.get<any>(
        environment.adminTransactionUrl + 'GetTransactionDescriptions'
      )
    );
  }

  public deleteTransactionDescription(id: number): Observable<any> {
    return this.loaderService.wrapHttpRequest(
      this.http.delete(
        environment.adminTransactionUrl +
          `DeleteTransactionDescription?id=${id}`
      )
    );
  }

  public updateTransactionDescription(
    newTransactionDescription: TransactionDescriptions
  ): Observable<any> {
    const { id, descriptionName } = newTransactionDescription;
    return this.loaderService.wrapHttpRequest(
      this.http.put<any>(
        `${environment.adminTransactionUrl}UpdateTransactionDescription?id=${id}&descriptionName=${descriptionName}`,
        {}
      )
    );
  }

  //2

  public getDashboardValues(): Observable<DashboardValues> {
    return this.loaderService.wrapHttpRequest(
      this.http.get<DashboardValues>(
        environment.transactionUrl + 'GetDashboardValues'
      )
    );
  }

  public getCardValues(mode: string): Observable<any> {
    return this.loaderService.wrapHttpRequest(
      this.http.get<any>(
        `${environment.transactionCalculationsUrl}GetCardValues`,{params:{mode}}
      )
    )
  }

  public getChartData(type: string):Observable<any> {
    return this.loaderService.wrapHttpRequest(
      this.http.get<any>(
        `${environment.transactionCalculationsUrl}GetChartValues`,{params:{type}}
      )
    );
  }

  public getFinancialGoals(mode: string):Observable<any> {
    return this.loaderService.wrapHttpRequest(
      this.http.get<any>(
        environment.financialGoalUrl+'GetFinancialGoals',{params:{mode}}
      )
    );
  }
}
