import { Component } from '@angular/core';
import { AuthenticationService } from '../../../services/authentication.service';
import { jsPDF } from 'jspdf';  // Import jsPDF
import html2canvas from 'html2canvas';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {
  navLinks = [
    { title: 'Dashboard', url: '/dashboard' },
    { title: 'Transactions', url: '/transactions' },
    { title: 'Budgeting', url: '/budgeting' },
    { title: 'Summary', url: '/summary' },
    { title: 'Admin', url: '/admin' }
  ];

  isAdmin = false;

  constructor(private authService: AuthenticationService) { }

  ngOnInit(): void {
    this.authService.currentUser$.subscribe(user => {
      if (user && user.role === 'Admin') {
        this.isAdmin = true;
      }
    });
  }

 // Method to generate and download the PDF

}