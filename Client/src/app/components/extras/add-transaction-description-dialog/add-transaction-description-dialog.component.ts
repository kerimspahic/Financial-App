import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { TransactionService } from '../../../services/transaction.service';

@Component({
  selector: 'app-transaction-description-dialog',
  templateUrl: './add-transaction-description-dialog.component.html',
  styleUrl: './add-transaction-description-dialog.component.css'
})
export class AddTransactionDescriptionDialogComponent {
  descriptionName: string = '';
  constructor(private dialogRef: MatDialogRef<AddTransactionDescriptionDialogComponent>, private transactionService: TransactionService) { }

  closeDialog() {
    this.dialogRef.close();
  }

  submitTransactionDescription() {

    this.transactionService.sendTransactionDescriptionData(this.descriptionName);

    this.dialogRef.close('submitted');
  }
}