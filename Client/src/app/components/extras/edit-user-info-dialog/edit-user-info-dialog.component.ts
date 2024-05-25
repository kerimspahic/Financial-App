import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { UserService } from '../../../services/user.service';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from '../../../services/authentication.service';


@Component({
  selector: 'app-edit-user-info-dialog',
  templateUrl: './edit-user-info-dialog.component.html',
  styleUrl: './edit-user-info-dialog.component.css',
})
export class EditUserInfoDialogComponent implements OnInit {
  userInfoForm!: FormGroup;
  userPasswordForm!: FormGroup;
  emailFormControl = new FormControl('', [Validators.required, Validators.email]);
  

  constructor(
    public authService: AuthenticationService,
    private userService: UserService,
    private dialogRef: MatDialogRef<EditUserInfoDialogComponent>,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.userInfoForm = this.formBuilder.group({
      username: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
    });

    this.userPasswordForm = this.formBuilder.group({
      oldPassword: ['', Validators.required],
      newPassword: ['', Validators.required],
      confirmPassword: ['', Validators.required],
    }, { validator: this.passwordMatchValidator });
  }

  updateInfo() {
    if (this.userInfoForm.valid) {
      this.userService.editUserInfo(this.userInfoForm.value);
      this.dialogRef.close();
      this.authService.logout();
    } else {
      console.log('Form is invalid.');
    }
  }

  updatePassword() {
    if (this.userPasswordForm.valid){
      this.userService.editUserPassword(this.userPasswordForm.value);
      this.dialogRef.close();
    } else {
      console.log('Form is invalid.');
    }
  }

  updateEmail() {
    if (this.emailFormControl.valid) {
      this.userService.editUserEmail(this.emailFormControl.value)
      this.dialogRef.close();
      console.log(this.emailFormControl.value);
    } else {
      console.log('Please enter a valid email address.');
    }
  }

  onCancelClick(): void {
    this.dialogRef.close();
  }

  passwordMatchValidator(control: AbstractControl) {
    const newPassword = control.get('newPassword')?.value;
    const confirmPassword = control.get('confirmPassword')?.value;

    if (newPassword !== confirmPassword) {
      control.get('confirmPassword')?.setErrors({ passwordMismatch: true });
      return { passwordMismatch: true };
    } else {
      return null;
    }
  }
}
