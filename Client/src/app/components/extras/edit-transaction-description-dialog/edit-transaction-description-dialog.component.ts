import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-edit-transaction-description-dialog',
  templateUrl: './edit-transaction-description-dialog.component.html',
  styleUrl: './edit-transaction-description-dialog.component.css',
})
export class EditTransactionDescriptionDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<EditTransactionDescriptionDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { description: string; type: boolean }
  ) {}

  onSaveClick(): void {
    this.dialogRef.close({
      description: this.data.description,
      type: this.data.type,
    });
  }

  onCancelClick(): void {
    this.dialogRef.close();
  }
}
