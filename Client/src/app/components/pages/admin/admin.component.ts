import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { TransactionDescriptions } from '../../../models/transactionDescriptions';
import { MatDialog } from '@angular/material/dialog';
import { AddTransactionDescriptionDialogComponent } from '../../extras/add-transaction-description-dialog/add-transaction-description-dialog.component';
import { TransactionService } from '../../../services/transaction.service';
import { EditTransactionDescriptionDialogComponent } from '../../extras/edit-transaction-description-dialog/edit-transaction-description-dialog.component';
import { ToastrService } from 'ngx-toastr';

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

  appUsersDataSource = new MatTableDataSource<any>();
  appUsersDisplayedColumns = ['firstName', 'lastName', 'email', 'actions'];
  transactionsDisplayedColumns = ['transactionId', 'transactionAmount', 'transactionDate'];

  expandedUser: string | null = null;

  constructor(
    private dialog: MatDialog,
    public transactionService: TransactionService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.loadTransactionData();
    this.loadAppUsersData();
  }

  loadTransactionData() {
    this.transactionService
      .getTransactionDescriptionData()
      .subscribe((data) => {
        this.transactionDescriptionDataSource.data = data;
      });
  }

  loadAppUsersData() {
    this.transactionService.getAppUsersData().subscribe((data) => {
      this.appUsersDataSource.data = data;
    });
  }

  toggleTransactions(userId: string) {
    this.expandedUser = this.expandedUser === userId ? null : userId;
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
  sendWeeklySummary() {
    this.transactionService.sendWeeklySummaryEmail().subscribe({
      next: () => {
        this.toastr.success('Weekly summary email sent successfully!', 'Success');
      }
    });
  }
  deleteUser(userId: string) {
    const confirmDelete = confirm('Are you sure you want to delete this user?');
    if (confirmDelete) {
      this.transactionService.deleteAppUser(userId).subscribe({
        next: () => {
          this.toastr.success('User deleted successfully!', 'Success');
          this.loadAppUsersData(); // Reload the data after deletion
        },
        error: (error) => {
          const errorMessage = error.error?.Error || 'Failed to delete the user. Please try again.';
          this.toastr.error(errorMessage, 'Error');
        }
      });
    }
  }
}