import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../../services/authentication.service';
import { EditUserInfoDialogComponent } from '../../extras/edit-user-info-dialog/edit-user-info-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {
  pageTitle: string = '';
  
  constructor(public authService: AuthenticationService, private dialog: MatDialog,private router: Router) { }

  ngOnInit(): void {
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe(() => {
      this.updateTitle();
    });
  }
  logout(): void {
    this.authService.logout();
  }

  editUserInformation() {
    const dialogRef = this.dialog.open(EditUserInfoDialogComponent, {
      width: '500px',
    });
  }

  updateTitle() {
    const url = this.router.url;
    const lastPart = url.substring(url.lastIndexOf('/') + 1);
    this.pageTitle = lastPart;
  }
}
