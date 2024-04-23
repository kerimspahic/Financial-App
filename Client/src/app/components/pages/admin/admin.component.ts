import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { TransactionDescriptions } from '../../../models/transactionDescriptions';
import { MatDialog } from '@angular/material/dialog';
import { DeleteTransactionDescriptionDialogComponent } from '../../extras/delete-transaction-description-dialog/delete-transaction-description-dialog.component';
import { TransactionService } from '../../../services/transaction.service';
import { EditTransactionDescriptionDialogComponent } from '../../extras/edit-transaction-description-dialog/edit-transaction-description-dialog.component';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css',
})
export class AdminComponent implements OnInit {
  public transactionDescriptionNames!: any;
  dataSource = new MatTableDataSource<TransactionDescriptions>();
  displayedColumns = ['id', 'descriptionName', 'actions'];

  constructor(private dialog: MatDialog, public transactionService: TransactionService ) {}

  ngOnInit(): void {
    this.loadTransactionData();
  }

  loadTransactionData() {
    this.transactionService.getTransactionDescriptionData().subscribe((data) => {
      this.dataSource.data = data;
    });
  }

  deleteDescription(id: number) {
    this.transactionService.deleteTransactionDescriptionData(id)
      this.loadTransactionData();
  }

  openAddTransactionDialog() {
    const dialogRef = this.dialog.open(DeleteTransactionDescriptionDialogComponent, {
      width: '300px',
    });
    this.loadTransactionData();
  }

  editDescription(id: number, currentDescription: string) {
    const dialogRef = this.dialog.open(EditTransactionDescriptionDialogComponent, {
      width: '300px',
      data: { description: currentDescription },
    });
  
    dialogRef.afterClosed().subscribe((newDescription: string) => {
      if (newDescription) {
        const transactionDescription: TransactionDescriptions = {
          id: id,
          descriptionName: newDescription
        }
        this.transactionService.editTransactionDescriptionData(transactionDescription);
      }
    });
  }
}
