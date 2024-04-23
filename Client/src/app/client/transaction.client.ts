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

  public getTransactionData(): Observable<any> {
    return this.loaderService.wrapHttpRequest(
      this.http.get(environment.transactionUrl + 'GetUserTransactions')
    );
  }

  public sendTransactionData(newUserTransaction: Transaction): Observable<Object> {
    return this.loaderService.wrapHttpRequest(
      this.http.post(environment.transactionUrl + 'SetNewTransaction', newUserTransaction)
    );
  }

  public addTransactionDescription(descriptionName: string): Observable<any> {
    return this.loaderService.wrapHttpRequest(
      this.http.post(environment.adminTransactionUrl + 'SetTransactionDescription', { descriptionName })
    );
  }

  public getTransactionDesciptionNames(): Observable<any> {
    return this.loaderService.wrapHttpRequest(
      this.http.get<any>(environment.adminTransactionUrl + 'GetTransactionDescriptions')
    );
  }

  public deleteTransactionDescription(id: number): Observable<any> {
    return this.loaderService.wrapHttpRequest(
      this.http.delete(environment.adminTransactionUrl + `DeleteTransactionDescription?id=${id}`)
    );
  }

  public updateTransactionDescription(newTransactionDescription: TransactionDescriptions): Observable<any> {
    const { id, descriptionName } = newTransactionDescription;
    return this.loaderService.wrapHttpRequest(
      this.http.put<any>(
        `${environment.adminTransactionUrl}UpdateTransactionDescription?id=${id}&descriptionName=${descriptionName}`,
        {}
      )
    );
  }

  public getDashboardValues(): Observable<DashboardValues> {
    return this.loaderService.wrapHttpRequest(
      this.http.get<DashboardValues>(environment.transactionUrl + 'GetDashboardValues')
    );
  }

  public getDashboardCharts(): Observable<DashboardCharts> {
    return this.loaderService.wrapHttpRequest(
      this.http.get<DashboardCharts>(environment.transactionUrl + 'GetDashboardChartValues')
    );
  }
}
