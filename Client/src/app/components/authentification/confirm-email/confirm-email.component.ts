import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrl: './confirm-email.component.css',
})
export class ConfirmEmailComponent implements OnInit {
  isLoading = true;
  isConfirmed = false;

  constructor(
    private route: ActivatedRoute,
    private http: HttpClient,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      const userId = params['userId'];
      const token = params['token'];
      this.confirmEmail(userId, token);
    });
  }

  confirmEmail(userId: string, token: string): void {
    this.http
      .get(
        `https://localhost:5001/api/account/confirmemail?userId=${userId}&token=${encodeURIComponent(
          token
        )}`
      )
      .subscribe({
        next: () => {
          this.isLoading = false;
          this.isConfirmed = true;
        },
        error: () => {
          this.isLoading = false;
          this.isConfirmed = false;
        },
      });
  }

  goToDashboard(): void {
    this.router.navigate(['/dashboard']);
  }
}
