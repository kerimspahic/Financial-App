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

  private tokenForHeader() {
    const adminToken = localStorage.getItem('token');
    const headers = new HttpHeaders({
      Authorization: `Bearer ${adminToken}`,
    });

    return headers;
  }

  public getExchangeData(): Observable<any> {
    const headers = this.tokenForHeader();

    return this.http.get(environment.exchangeUrl + 'GetUserExchanges', {
      headers,
    });
  }

  public setExchangeData(exchangeAmount: number, exchangeType: string, exchangeDate: string, exchangeDescription: string): Observable<Object> {
    const headers = this.tokenForHeader();

    return this.http.post(
      environment.exchangeUrl + 'Set',
      {
        exchangeAmount: exchangeAmount,
        exchangeType: exchangeType,
        exchangeDate: exchangeDate,
        exchangeDescription: exchangeDescription,
      },
      { headers }
    );
  }
}
