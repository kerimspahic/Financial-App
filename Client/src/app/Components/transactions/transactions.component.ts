import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { TransactionsClient } from '../../client/transactions.client';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Transactions } from '../../models/transactions';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TransactionsService } from '../../services/transactions.service';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.css'
})
export class TransactionsComponent implements OnInit {
  public transForm!: FormGroup;
  displayedColumns = ['amount', 'type', 'date', 'description'];
  dataSource = new MatTableDataSource<Transactions>;

  constructor(public authService: AuthenticationService,public transactionClient: TransactionsClient, public transService: TransactionsService) { }

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<Transactions>;

  ngOnInit(): void {
    this.transForm = new FormGroup({
      amount: new FormControl('', [Validators.required]),
      type: new FormControl('', [Validators.required]),
      date: new FormControl('', [Validators.required]),
      description: new FormControl('', [Validators.required]),
      userName: new FormControl('', [Validators.required]),
    });

    this.transactionClient.getTransactionsData().subscribe(data => {
      this.dataSource.data = data;
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
    });
  }

  public onSubmit() {
    this.transService.insertTransactionData(
      this.transForm.get('amount')!.value,
      this.transForm.get('type')!.value,
      this.transForm.get('date')!.value,
      this.transForm.get('description')!.value,
      this.transForm.get('userName')!.value
    );
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator)
      this.dataSource.paginator.firstPage();
  }
}


/*
{
  "amount": 0,
  "type": true,
  "date": "2024-03-19T21:12:38.442Z",
  "description": "string",
  "userName": "string"
}
 */