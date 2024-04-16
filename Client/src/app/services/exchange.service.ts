import { Injectable } from '@angular/core';
import { ExchangeClient } from '../client/exchange.client';

@Injectable({
  providedIn: 'root'
})
export class ExchangeService {

  constructor(private exchangeClient: ExchangeClient) { }
  
    public setExchangeData(exchangeAmount: number,exchangeType: string,exchangeDate: string,exchangeDescription: string): void {
      this.exchangeClient.setExchangeData(exchangeAmount,exchangeType,exchangeDate,exchangeDescription).subscribe();
    }
  
}
