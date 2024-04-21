import { Injectable } from '@angular/core';
import { ExchangeClient } from '../client/exchange.client';
import { Exchange } from '../models/exchange';
import { Observable } from 'rxjs';
import { TransactionDescriptions } from '../models/transactionDescriptions';

@Injectable({
  providedIn: 'root',
})
export class ExchangeService {
  constructor(private exchangeClient: ExchangeClient) {}

  public sendExchangeData(newUserTransaction: Exchange): void {
    this.exchangeClient.sendExchangeData(newUserTransaction).subscribe();
  }

  public getExchangeDescriptionData(): Observable<any> {
    return this.exchangeClient.getTransactionDesciptionNames();
  }

  public sendExchangeDescriptionData(descriptionName: string): void {
    this.exchangeClient.addTransactionDescription(descriptionName).subscribe();
  }

  public deleteExchangeDescriptionData(id : number):void {
    this.exchangeClient.deleteTransactionDescription(id).subscribe();
  }

  public editExchangeDescriptionData(newTransactionDescription: TransactionDescriptions){
    this.exchangeClient.updateTransactionDescription(newTransactionDescription).subscribe();
  }
}
