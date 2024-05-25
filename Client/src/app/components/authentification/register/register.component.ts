import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthenticationService } from '../../../services/authentication.service';
import { passwordMatchValidator } from '../../../validator/passwordMatchValidator';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  public registerForm!: FormGroup;
  public hidePassword: boolean = true;
  public hideConfirmPassword: boolean = true;

  constructor(private authService: AuthenticationService) {}

  ngOnInit() {
    this.registerForm = new FormGroup(
      {
        username: new FormControl('', [Validators.required]),
        email: new FormControl('', [Validators.required, Validators.email]),
        firstname: new FormControl('', [Validators.required]),
        lastname: new FormControl('', [Validators.required]),
        password: new FormControl('', [Validators.required]),
        confirmPassword: new FormControl('', [Validators.required]),
      },
      { validators: passwordMatchValidator() }
    );
  }

  public onSubmit() {
    if (this.registerForm.invalid) {
      return;
    }

    this.authService.register(
      this.registerForm.get('username')!.value,
      this.registerForm.get('email')!.value,
      this.registerForm.get('firstname')!.value,
      this.registerForm.get('lastname')!.value,
      this.registerForm.get('password')!.value,
      this.registerForm.get('confirmPassword')!.value
    );
  }
}
