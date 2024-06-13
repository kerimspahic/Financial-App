import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { TransactionDescriptions } from '../../../models/transactionDescriptions';
import { MatDialog } from '@angular/material/dialog';
import { AddTransactionDescriptionDialogComponent } from '../../extras/add-transaction-description-dialog/add-transaction-description-dialog.component';
import { TransactionService } from '../../../services/transaction.service';
import { EditTransactionDescriptionDialogComponent } from '../../extras/edit-transaction-description-dialog/edit-transaction-description-dialog.component';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css',
})
export class AdminComponent implements OnInit {
  transactionDescriptionDataSource =
    new MatTableDataSource<TransactionDescriptions>();

  transactionDescriptionDisplayedColumns = [
    'id',
    'descriptionName',
    'descriptionType',
    'actions',
  ];

  constructor(
    private dialog: MatDialog,
    public transactionService: TransactionService
  ) {}

  ngOnInit(): void {
    this.loadTransactionData();
  }

  loadTransactionData() {
    this.transactionService
      .getTransactionDescriptionData()
      .subscribe((data) => {
        this.transactionDescriptionDataSource.data = data;
      });
  }

  deleteDescription(id: number) {
    this.transactionService.deleteTransactionDescriptionData(id);
    this.loadTransactionData();
  }

  openAddTransactionDialog() {
    const dialogRef = this.dialog.open(
      AddTransactionDescriptionDialogComponent,
      {
        width: '300px',
      }
    );
    this.loadTransactionData();
  }

  editDescription(
    id: number,
    currentDescription: string,
    currentType: boolean
  ) {
    const dialogRef = this.dialog.open(
      EditTransactionDescriptionDialogComponent,
      {
        width: '300px',
        data: { description: currentDescription, type: currentType },
      }
    );

    dialogRef
      .afterClosed()
      .subscribe((result: { description: string; type: boolean }) => {
        if (result) {
          const transactionDescription: TransactionDescriptions = {
            id: id,
            descriptionName: result.description,
            descriptionType: result.type,
          };
          this.transactionService.editTransactionDescriptionData(
            transactionDescription
          );
          this.loadTransactionData();
        }
      });
  }
}
