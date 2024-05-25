import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-edit-transaction-description-dialog',
  templateUrl: './edit-transaction-description-dialog.component.html',
  styleUrl: './edit-transaction-description-dialog.component.css'
})
export class EditTransactionDescriptionDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<EditTransactionDescriptionDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { description: string }
  ) {}

  onSaveClick(newDescription: string): void {
    this.dialogRef.close(newDescription);
  }

  onCancelClick(): void {
    this.dialogRef.close();
  }
}