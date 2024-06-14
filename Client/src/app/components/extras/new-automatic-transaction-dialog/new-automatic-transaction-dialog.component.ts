import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { AutomaticTransaction } from '../../../models/automaticTransaction';

@Component({
  selector: 'app-new-automatic-transaction-dialog',
  templateUrl: './new-automatic-transaction-dialog.component.html',
  styleUrl: './new-automatic-transaction-dialog.component.css'
})
export class NewAutomaticTransactionDialogComponent {
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

  constructor(public dialogRef: MatDialogRef<NewAutomaticTransactionDialogComponent>) {}

  onCancel(): void {
    this.dialogRef.close();
  }

  onSubmit(): void {
    this.dialogRef.close(this.transaction);
  }
}
