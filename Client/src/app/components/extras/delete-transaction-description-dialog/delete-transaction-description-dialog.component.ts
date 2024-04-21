import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { ExchangeService } from '../../../services/exchange.service';

@Component({
  selector: 'app-transaction-description-dialog',
  templateUrl: './delete-transaction-description-dialog.component.html',
  styleUrl: './delete-transaction-description-dialog.component.css'
})
export class DeleteTransactionDescriptionDialogComponent {
  descriptionName: string = '';
  constructor(private dialogRef: MatDialogRef<DeleteTransactionDescriptionDialogComponent>, private exchangeService: ExchangeService) { }

  closeDialog() {
    this.dialogRef.close();
  }

  submitTransactionDescription() {

    this.exchangeService.sendExchangeDescriptionData(this.descriptionName);

    this.dialogRef.close('submitted');
  }
}