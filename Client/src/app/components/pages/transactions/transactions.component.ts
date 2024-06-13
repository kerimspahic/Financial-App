import { Component, OnInit } from '@angular/core';
import { Transaction } from '../../../models/transaction';
import { TransactionClient } from '../../../client/transaction.client';
import { TransactionDescriptions } from '../../../models/transactionDescriptions';
import { AddTransactionDialogComponent } from '../../extras/add-transaction-dialog/add-transaction-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrls: ['./transactions.component.css'],
})
export class TransactionsComponent implements OnInit {
  public transactionDescriptionNames: TransactionDescriptions[] = [];
  public transactions: Transaction[] = [];
  public total = 0;
  public pageNumber = 1;
  public pageSize = 5;
  public sortBy = 'transactionDate'; // Default sort field
  public isDescending = true; // Default sort order
  public filters = {
    transactionAmount: null,
    transactionType: null,
    transactionDescription: null,
  };

  constructor(
    private transactionClient: TransactionClient,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.loadTransactionDescriptionNames();
    this.getTransactions();
  }

  openNewTransactionDialog(): void {
    const dialogRef = this.dialog.open(AddTransactionDialogComponent, {
      width: '400px',
    });

    dialogRef.afterClosed().subscribe(() => {
      console.log('The dialog was closed');
      this.getTransactions();
    });
  }

  private loadTransactionDescriptionNames(): void {
    this.transactionClient.getTransactionDescriptionNames().subscribe(
      (descriptions: TransactionDescriptions[]) => {
        this.transactionDescriptionNames = descriptions;
      },
      (error) => {
        console.error('Error fetching transaction descriptions:', error);
      }
    );
  }

  private getTransactions(): void {
    this.transactionClient.getTransactionData(this.pageNumber, this.pageSize, this.sortBy, this.isDescending, this.filters).subscribe(
      (res) => {
        this.transactions = res.page.data;
        this.total = res.page.total;
      },
      (error) => {
        console.error('Error fetching transactions:', error);
      }
    );
  }

  public getDescriptionName(id: number): string {
    const description = this.transactionDescriptionNames.find(desc => desc.id === id);
    return description ? description.descriptionName : 'N/A';
  }

  public goToPrevious(): void {
    this.pageNumber--;
    this.getTransactions();
  }

  public goToNext(): void {
    this.pageNumber++;
    this.getTransactions();
  }

  public goToPage(n: number): void {
    this.pageNumber = n;
    this.getTransactions();
  }

  public changePageSize(size: number): void {
    this.pageSize = size;
    this.pageNumber = 1; // Reset to first page
    this.getTransactions();
  }

  public sortTransactions(field: string): void {
    if (this.sortBy === field) {
      this.isDescending = !this.isDescending;
    } else {
      this.sortBy = field;
      this.isDescending = true;
    }
    this.getTransactions();
  }

  public applyFilters(): void {
    this.pageNumber = 1; // Reset to first page when applying filters
    this.getTransactions();
  }
}
