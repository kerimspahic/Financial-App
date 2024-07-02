import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { AutomaticTransaction } from '../../../models/automaticTransaction';
import { TransactionClient } from '../../../client/transaction.client';
import { TransactionDescriptions } from '../../../models/transactionDescriptions';

@Component({
  selector: 'app-new-automatic-transaction-dialog',
  templateUrl: './new-automatic-transaction-dialog.component.html',
  styleUrls: ['./new-automatic-transaction-dialog.component.css']
})
export class NewAutomaticTransactionDialogComponent implements OnInit {
  transaction: AutomaticTransaction = {
    id: 0,
    transactionAmount: 0,
    transactionType: true,
    transactionDescription: 0,
    transactionDate: new Date(),
    frequency: 0,
    nextExecutionDate: new Date(),
    insertedDate: new Date()
  };

  transactionDescriptions: TransactionDescriptions[] = [];

  constructor(
    public dialogRef: MatDialogRef<NewAutomaticTransactionDialogComponent>,
    private transactionClient: TransactionClient
  ) {}

  ngOnInit(): void {
    this.loadTransactionDescriptions();
  }

  loadTransactionDescriptions(): void {
    this.transactionClient.getTransactionDescriptionNames().subscribe(
      (descriptions: TransactionDescriptions[]) => {
        this.transactionDescriptions = descriptions;
      },
      (error) => {
        console.error('Error fetching transaction descriptions:', error);
      }
    );
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSubmit(): void {
    this.dialogRef.close(this.transaction);
  }
}
