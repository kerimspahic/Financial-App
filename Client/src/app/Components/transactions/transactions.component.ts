import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { TransactionsClient } from '../../client/transactions.client';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { DataTableItem, DataTableDataSource } from '../../data-table/data-table-datasource';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.css'
})
export class TransactionsComponent implements AfterViewInit {
  public transaction: Observable<any> = this.transactionClient.getTransactionsData();
  displayedColumns = ['amount', 'type', 'date', 'description']
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<DataTableItem>;
  dataSource = new DataTableDataSource();

  constructor (public transactionClient: TransactionsClient) {}

  ngAfterViewInit(): void {
    
  }


}
