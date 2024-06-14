import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TransactionClient } from '../../../client/transaction.client';
import { Transaction } from '../../../models/transaction';

@Component({
  selector: 'app-edit-transaction-dialog',
  templateUrl: './edit-transaction-dialog.component.html',
  styleUrls: ['./edit-transaction-dialog.component.css']
})
export class EditTransactionDialogComponent implements OnInit {
  public editTransactionForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private transactionClient: TransactionClient,
    public dialogRef: MatDialogRef<EditTransactionDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  ngOnInit(): void {
    this.editTransactionForm = this.fb.group({
      transactionAmount: [this.data.transaction.transactionAmount, Validators.required],
      transactionType: [this.data.transaction.transactionType, Validators.required],
      transactionDate: [this.data.transaction.transactionDate, Validators.required],
      transactionDescription: [this.data.transaction.transactionDescription, Validators.required],
    });
  }

  public save(): void {
    if (this.editTransactionForm.valid) {
      const updatedTransaction: Transaction = {
        ...this.data.transaction,
        ...this.editTransactionForm.value
      };
      this.transactionClient.updateTransaction(updatedTransaction).subscribe(
        () => {
          this.dialogRef.close(true);
        },
        (error) => {
          console.error('Error updating transaction:', error);
        }
      );
    }
  }

  public cancel(): void {
    this.dialogRef.close();
  }
}
