import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserPassword } from '../../../models/userPassword';
import { UserService } from '../../../services/user.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-edit-user-info-dialog',
  templateUrl: './edit-user-info-dialog.component.html',
  styleUrl: './edit-user-info-dialog.component.css',
})
export class EditUserInfoDialogComponent implements OnInit {
  oldPassword!: string;
  newPassword!: string;
  confirmPassword!: string;
  userForm!: FormGroup;

  constructor(private dialogRef: MatDialogRef<EditUserInfoDialogComponent>, private http: HttpClient,private userService: UserService,private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.userForm = this.formBuilder.group({
      username: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.userForm.valid) {
      // Process the form data, e.g., send it to an API
      console.log(this.userForm.value);
    } else {
      // Handle form validation errors
      console.log("Form is invalid.");
    }
  }

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