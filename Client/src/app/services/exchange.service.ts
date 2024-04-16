import { Injectable } from '@angular/core';
import { ExchangeClient } from '../client/exchange.client';
import { Exchange } from '../models/exchange';

@Injectable({
  providedIn: 'root',
})
export class ExchangeService {
  constructor(private exchangeClient: ExchangeClient) {}

  public sendExchangeData(newUserTransaction: Exchange): void {
    this.exchangeClient.sendExchangeData(newUserTransaction).subscribe();
  }
}
