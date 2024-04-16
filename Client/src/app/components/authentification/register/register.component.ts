import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthenticationService } from '../../../services/authentication.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {
  public registerForm!: FormGroup;

  constructor(private authService: AuthenticationService) { }

  ngOnInit() {
    this.registerForm = new FormGroup({
      username: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      firstname: new FormControl('', [Validators.required]),
      lastname: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required])
    });
  }

  public onSubmit() {
    this.authService.register(
      this.registerForm.get('username')!.value,
      this.registerForm.get('email')!.value,
      this.registerForm.get('firstname')!.value,
      this.registerForm.get('lastname')!.value,
      this.registerForm.get('password')!.value
    );
  }
}
