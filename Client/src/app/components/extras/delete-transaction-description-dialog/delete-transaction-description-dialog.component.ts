import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { TransactionService } from '../../../services/transaction.service';

@Component({
  selector: 'app-transaction-description-dialog',
  templateUrl: './delete-transaction-description-dialog.component.html',
  styleUrl: './delete-transaction-description-dialog.component.css'
})
export class DeleteTransactionDescriptionDialogComponent {
  descriptionName: string = '';
  constructor(private dialogRef: MatDialogRef<DeleteTransactionDescriptionDialogComponent>, private transactionService: TransactionService) { }

  closeDialog() {
    this.dialogRef.close();
  }

  submitTransactionDescription() {

    this.transactionService.sendTransactionDescriptionData(this.descriptionName);

    this.dialogRef.close('submitted');
  }
}