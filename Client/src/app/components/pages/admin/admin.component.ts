import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { TransactionDescriptions } from '../../../models/transactionDescriptions';
import { MatDialog } from '@angular/material/dialog';
import { DeleteTransactionDescriptionDialogComponent } from '../../extras/delete-transaction-description-dialog/delete-transaction-description-dialog.component';
import { ExchangeService } from '../../../services/exchange.service';
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

  constructor(private dialog: MatDialog, public exchangeService: ExchangeService ) {}

  ngOnInit(): void {
    this.loadExchangeData();
  }

  loadExchangeData() {
    this.exchangeService.getExchangeDescriptionData().subscribe((data) => {
      this.dataSource.data = data;
    });
  }

  deleteDescription(id: number) {
    this.exchangeService.deleteExchangeDescriptionData(id)
      this.loadExchangeData();
  }

  openAddTransactionDialog() {
    const dialogRef = this.dialog.open(DeleteTransactionDescriptionDialogComponent, {
      width: '300px',
    });
    this.loadExchangeData();
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
        this.exchangeService.editExchangeDescriptionData(transactionDescription);
      }
    });
  }
}
