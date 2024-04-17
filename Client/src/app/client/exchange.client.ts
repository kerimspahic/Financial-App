import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, finalize } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { Exchange } from '../models/exchange';
import { LoaderService } from '../services/loader.service';

@Injectable({
  providedIn: 'root',
})
export class ExchangeClient {
  constructor(private http: HttpClient, private loaderService: LoaderService) {}

  public getExchangeData(): Observable<any> {
    this.loaderService.show();
    return this.http.get(environment.exchangeUrl + 'GetUserExchanges')
    .pipe(
      finalize(() => {
        this.loaderService.hide();
      })
    );
  }

  public sendExchangeData(newUserTransaction: Exchange): Observable<Object> {
    this.loaderService.show();
    return this.http.post(environment.exchangeUrl + 'Set', newUserTransaction)
    .pipe(
      finalize(() => {
        this.loaderService.hide();
      })
    );
  }
}
