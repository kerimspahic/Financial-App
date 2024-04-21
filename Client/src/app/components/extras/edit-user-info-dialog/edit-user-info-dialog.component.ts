import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserPassword } from '../../../models/userPassword';
import { UserService } from '../../../services/user.service';

@Component({
  selector: 'app-edit-user-info-dialog',
  templateUrl: './edit-user-info-dialog.component.html',
  styleUrl: './edit-user-info-dialog.component.css',
})
export class EditUserInfoDialogComponent {
  oldPassword!: string;
  newPassword!: string;
  confirmPassword!: string;

  constructor(private dialogRef: MatDialogRef<EditUserInfoDialogComponent>, private http: HttpClient,private userService: UserService) { }

  updatePassword() {
    if (this.newPassword !== this.confirmPassword) {
      // Handle password mismatch error
      return;
    }
    
    const passwordDto: UserPassword = {
      oldPassword: this.oldPassword,
      newPassword: this.newPassword,
      confirmPassword: this.confirmPassword
    };

    this.userService.editUserPassword(passwordDto);
    this.dialogRef.close();
  }
  onCancelClick(): void {
    this.dialogRef.close();
  }
}