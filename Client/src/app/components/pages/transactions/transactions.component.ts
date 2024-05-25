import { Component, OnInit } from '@angular/core';
import { Transaction } from '../../../models/transaction';
import { TransactionClient } from '../../../client/transaction.client';
import { TransactionDescriptions } from '../../../models/transactionDescriptions';
import { AddTransactionDialogComponent } from '../../extras/add-transaction-dialog/add-transaction-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.css',
})
export class TransactionsComponent implements OnInit {
  public transactionDescriptiuonNames!: any;

  constructor(
    public transactionClient: TransactionClient,
    public dialog: MatDialog
  ) {}

  transactions!: Transaction[];
  total = 0;
  pageNumber = 1;
  pageSize = 5;

  ngOnInit(): void {
    this.loadTransactionDescriptionNames();
    this.getTransactions();
  }

  openNewTransactionDialog(): void {
    const dialogRef = this.dialog.open(AddTransactionDialogComponent, {
      width: '400px',
    });

    dialogRef.afterClosed().subscribe((result) => {
      console.log('The dialog was closed');
      this.getTransactions();
    });
  }

  loadTransactionDescriptionNames() {
    this.transactionClient.getTransactionDesciptionNames().subscribe(
      (descriptions: TransactionDescriptions) => {
        this.transactionDescriptiuonNames = descriptions;
      },
      (error) => {
        console.error('Error fetching transaction descriptions:', error);
      }
    );
  }
  //new ones

  getTransactions(): void {
    this.transactionClient
      .getTransactionData(this.pageNumber, this.pageSize)
      .subscribe((res) => {
        this.transactions = res['page']['data'];
        this.total = res['page'].total;
      });
  }

  getDescriptionName(id: number): string {
    const description = this.transactionDescriptiuonNames.find(
      (x: { id: number }) => x.id === id
    );
    return description ? description.descriptionName : 'N/A';
  }

  goToPrevious(): void {
    this.pageNumber--;
    this.getTransactions();
  }

  goToNext(): void {
    this.pageNumber++;
    this.getTransactions();
  }

  goToPage(n: number): void {
    this.pageNumber = n;
    this.getTransactions();
  }
  
}
