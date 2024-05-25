import { Component, OnInit } from '@angular/core';
import { TransactionClient } from '../../../client/transaction.client';
import { TransactionService } from '../../../services/transaction.service';
import { TransactionDescriptions } from '../../../models/transactionDescriptions';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Transaction } from '../../../models/transaction';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-add-transaction-dialog',
  templateUrl: './add-transaction-dialog.component.html',
  styleUrl: './add-transaction-dialog.component.css',
})
export class AddTransactionDialogComponent implements OnInit {
  public transactionForm!: FormGroup;
  public transactionDescriptiuonNames!: any; //fix this

  filterDates = (d: Date | null) => {
    const today = new Date();
    if (d == null) return false;
    return d <= today;
  };

  constructor(
    public transactionClient: TransactionClient,
    public transactionService: TransactionService,
    public dialogRef: MatDialogRef<AddTransactionDialogComponent>
  ) {}

  ngOnInit(): void {
    this.transactionForm = new FormGroup({
      transactionAmount: new FormControl('', [Validators.required]),
      transactionType: new FormControl('', [Validators.required]),
      transactionDate: new FormControl('', [Validators.required]),
      transactionDescription: new FormControl('', [Validators.required]),
    });

    this.loadTransactionDescriptionNames();
  }

  public onSubmit() {
    const newUserTransaction: Transaction = {
      transactionAmount: this.transactionForm.get('transactionAmount')!.value,
      transactionType: this.transactionForm.get('transactionType')!.value,
      transactionDate: this.transactionForm.get('transactionDate')!.value,
      transactionDescription: this.transactionForm.get('transactionDescription')!.value,
    };

    this.transactionService.sendTransactionData(newUserTransaction);
    this.transactionForm.reset();
  }

  onCancelClick(): void {
    this.dialogRef.close();
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
}
