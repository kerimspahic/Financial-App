import { Component, OnInit } from '@angular/core';
import { TransactionClient } from '../../../client/transaction.client';
import { TransactionService } from '../../../services/transaction.service';
import { TransactionDescriptions } from '../../../models/transactionDescriptions';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Transaction } from '../../../models/transaction';
import { MatDialogRef } from '@angular/material/dialog';
import { Observable, of } from 'rxjs';
import { startWith, map } from 'rxjs/operators';

@Component({
  selector: 'app-add-transaction-dialog',
  templateUrl: './add-transaction-dialog.component.html',
  styleUrls: ['./add-transaction-dialog.component.css'],
})
export class AddTransactionDialogComponent implements OnInit {
  public transactionForm!: FormGroup;
  public transactionDescriptionNames: TransactionDescriptions[] = [];
  public filteredTransactionDescriptions: Observable<TransactionDescriptions[]> = of([]);

  constructor(
    public transactionClient: TransactionClient,
    public transactionService: TransactionService,
    public dialogRef: MatDialogRef<AddTransactionDialogComponent>
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.loadTransactionDescriptionNames();
    this.setupFormValueChanges();
  }

  private initializeForm(): void {
    this.transactionForm = new FormGroup({
      transactionAmount: new FormControl('', [Validators.required]),
      transactionType: new FormControl('', [Validators.required]),
      transactionDate: new FormControl('', [Validators.required]),
      transactionDescription: new FormControl('', [Validators.required]),
    });
  }

  private setupFormValueChanges(): void {
    this.transactionForm.get('transactionType')?.valueChanges.subscribe(value => {
      this.filterTransactionDescriptions(value);
    });

    this.transactionForm.get('transactionDescription')?.valueChanges.subscribe(descriptionId => {
      const description = this.transactionDescriptionNames.find(desc => desc.id === descriptionId);
      if (description) {
        this.transactionForm.get('transactionType')?.setValue(description.descriptionType, { emitEvent: false });
      }
    });
    
    this.filteredTransactionDescriptions = this.transactionForm.get('transactionType')!.valueChanges.pipe(
      startWith(null),
      map(value => this.filterTransactionDescriptions(value))
    );
  }

  loadTransactionDescriptionNames() {
    this.transactionClient.getTransactionDescriptionNames().subscribe(
      (descriptions: TransactionDescriptions[]) => { 
        this.transactionDescriptionNames = descriptions;
      },
      (error) => {
        console.error('Error fetching transaction descriptions:', error);
      }
    );
  }
  
  private filterTransactionDescriptions(type: boolean | null): TransactionDescriptions[] {
    return this.transactionDescriptionNames.filter(desc => type === null || desc.descriptionType === type);
  }

  public onSubmit(): void {
    if (this.transactionForm.invalid) {
      return;
    }

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

  filterDates = (d: Date | null) => {
    const today = new Date();
    if (d == null) return false;
    return d <= today;
  };

}
