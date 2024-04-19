import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { TransactionDescriptions } from '../../../models/transactionDescriptions';
import { ExchangeClient } from '../../../client/exchange.client';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css',
})
export class AdminComponent implements OnInit{
  public transactionDescriptiuonNames!: any;
  dataSource = new MatTableDataSource<TransactionDescriptions>();
  displayedColumns = ['id','descriptionName'];

  constructor(public exchngeClient: ExchangeClient) {}

  ngOnInit(): void {
    this.loadExchangeData();
    
  }

  loadExchangeData() {
    this.exchngeClient.getTransactionDesciptionNames().subscribe((data) => {
      this.dataSource.data = data;
    });
  }
}
