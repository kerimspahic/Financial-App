import { Component } from '@angular/core';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css',
})
export class SidebarComponent {
  navLinks = [
    { title: 'Dashboard', url: '/dashboard' },
    { title: 'Transactions', url: '/transactions' },
    { title: 'Budgeting', url: '/budgeting' },
    { title: 'Summary', url: '/summary' },
    { title: 'Admin', url: '/admin' }
  ];
}
