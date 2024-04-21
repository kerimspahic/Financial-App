import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../../services/authentication.service';
import { EditUserInfoDialogComponent } from '../../extras/edit-user-info-dialog/edit-user-info-dialog.component';
import { HttpClient } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {

  constructor(public authService: AuthenticationService, private dialog: MatDialog, private http: HttpClient) { }

  ngOnInit(): void { }

  logout(): void {
    this.authService.logout();
  }

  editUserInformation() {
    const dialogRef = this.dialog.open(EditUserInfoDialogComponent, {
      width: '500px',
    });
  }
}
