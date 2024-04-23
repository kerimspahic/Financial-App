import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, finalize } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { Transaction } from '../models/transaction';
import { LoaderService } from '../services/loader.service';
import { TransactionDescriptions } from '../models/transactionDescriptions';

@Injectable({
  providedIn: 'root',
})
export class TransactionClient {
  constructor(private http: HttpClient, private loaderService: LoaderService) {}

  public getTransactionData(): Observable<any> {
    this.loaderService.show();
    return this.http.get(environment.transactionUrl + 'GetUserTransactions').pipe(
      finalize(() => {
        this.loaderService.hide();
      })
    );
  }

  public sendTransactionData(newUserTransaction: Transaction): Observable<Object> {
    this.loaderService.show();
    return this.http
      .post(environment.transactionUrl + 'Set', newUserTransaction)
      .pipe(
        finalize(() => {
          this.loaderService.hide();
        })
      );
  }

  addTransactionDescription(descriptionName: string) {
    this.loaderService.show();
    return this.http
      .post(environment.adminTransactionUrl + 'SetTransactionDescription', {
        descriptionName,
      })
      .pipe(
        finalize(() => {
          this.loaderService.hide();
        })
      );
  }

  public getTransactionDesciptionNames(): Observable<any> {
    this.loaderService.show();
    return this.http
      .get<any>(environment.adminTransactionUrl + 'GetTransactionDescriptions')
      .pipe(
        finalize(() => {
          this.loaderService.hide();
        })
      );
  }

  public deleteTransactionDescription(id: number) {
    this.loaderService.show();
    return this.http
      .delete(
        environment.adminTransactionUrl +
          `DeleteTransactionDescription?id=${id}`
      )
      .pipe(
        finalize(() => {
          this.loaderService.hide();
        })
      );
  }

  public updateTransactionDescription(
    newTransactionDescription: TransactionDescriptions
  ) {
    this.loaderService.show();
    return this.http
      .put<any>(
        environment.adminTransactionUrl + `UpdateTransactionDescription?id=${newTransactionDescription.id}&descriptionName=${newTransactionDescription.descriptionName}`,
        {}
      )
      .pipe(
        finalize(() => {
          this.loaderService.hide();
        })
      );
  }
}
