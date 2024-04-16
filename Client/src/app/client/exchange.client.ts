import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { Exchange } from '../models/exchange';

@Injectable({
  providedIn: 'root',
})
export class ExchangeClient {
  constructor(private http: HttpClient) {}



  public getExchangeData(): Observable<any> {

    return this.http.get(environment.exchangeUrl + 'GetUserExchanges');
  }

  public setExchangeData(exchangeAmount: number, exchangeType: string, exchangeDate: string, exchangeDescription: string): Observable<Object> {

    return this.http.post(
      environment.exchangeUrl + 'Set',
      {
        exchangeAmount: exchangeAmount,
        exchangeType: exchangeType,
        exchangeDate: exchangeDate,
        exchangeDescription: exchangeDescription,
      }
    );
  }
}
