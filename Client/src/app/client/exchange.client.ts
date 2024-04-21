import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, finalize } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { Exchange } from '../models/exchange';
import { LoaderService } from '../services/loader.service';
import { TransactionDescriptions } from '../models/transactionDescriptions';

@Injectable({
  providedIn: 'root',
})
export class ExchangeClient {
  constructor(private http: HttpClient, private loaderService: LoaderService) {}

  public getExchangeData(): Observable<any> {
    this.loaderService.show();
    return this.http.get(environment.exchangeUrl + 'GetUserExchanges').pipe(
      finalize(() => {
        this.loaderService.hide();
      })
    );
  }

  public sendExchangeData(newUserTransaction: Exchange): Observable<Object> {
    this.loaderService.show();
    return this.http
      .post(environment.exchangeUrl + 'Set', newUserTransaction)
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
