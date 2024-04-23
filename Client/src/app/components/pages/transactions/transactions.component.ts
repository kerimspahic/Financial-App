import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Transaction } from '../../../models/transaction';
import { AuthenticationService } from '../../../services/authentication.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { TransactionClient } from '../../../client/transaction.client';
import { TransactionService } from '../../../services/transaction.service';
import { TransactionDescriptions } from '../../../models/transactionDescriptions';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.css',
})
export class TransactionsComponent implements OnInit {
  public transactionForm!: FormGroup;
  public transactionDescriptiuonNames!: any; //fix this
  displayedColumns = ['amount', 'type', 'date', 'description'];
  dataSource = new MatTableDataSource<Transaction>();
  
  filterDates = (d: Date | null) => {
    const today = new Date();
    if (d == null) 
      return false;
    return d <= today;
  }

  constructor(
    public authService: AuthenticationService,
    public transactionClient: TransactionClient,
    public transactionService: TransactionService) {}

  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngOnInit(): void {
    this.transactionForm = new FormGroup({
      transactionAmount: new FormControl('', [Validators.required]),
      transactionType: new FormControl('', [Validators.required]),
      transactionDate: new FormControl('', [Validators.required]),
      transactionDescription: new FormControl('', [Validators.required]),
    });

    this.loadTransactionDescriptionNames() ;

    this.loadTransactionData();
  }

  public onSubmit() {


    const newUserTransaction: Transaction = {
      transactionAmount: this.transactionForm.get('transactionAmount')!.value,
      transactionType: this.transactionForm.get('transactionType')!.value,
      transactionDate: this.transactionForm.get('transactionDate')!.value,
      transactionDescription: this.transactionForm.get('transactionDescription')!.value,
    };

    this.transactionService.sendTransactionData(newUserTransaction);

    this.loadTransactionData();
    this.transactionForm.reset();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) this.dataSource.paginator.firstPage();
  }

  sortData(sortState: Sort) {
    const data = this.dataSource.data.slice();
    if (!sortState.active || sortState.direction === '') {
      this.dataSource.data = data;
      return;
    }

    this.dataSource.data = data.sort((a, b) => {
      const isAsc = sortState.direction === 'asc';
      switch (sortState.active) {
        case 'amount':
          return this.compare(a.transactionAmount, b.transactionAmount, isAsc);
        case 'type':
          return this.compare(a.transactionType, b.transactionType, isAsc);
        case 'date':
          return this.compare(a.transactionDate, b.transactionDate, isAsc);
        case 'description':
          return this.compare(
            a.transactionDescription,
            b.transactionDescription,
            isAsc
          );
        default:
          return 0;
      }
    });
  }

  private compare(
    a: number | string | Date | boolean,
    b: number | string | Date | boolean,
    isAsc: boolean
  ) {
    if (a === b) {
      return 0;
    }
    return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  }

  loadTransactionData() {
    this.transactionClient.getTransactionData().subscribe((data) => {
      this.dataSource.data = data;
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
    });
  }
  loadTransactionDescriptionNames() {
    this.transactionClient.getTransactionDesciptionNames().subscribe(
      (descriptions: TransactionDescriptions) => {
        this.transactionDescriptiuonNames = descriptions;
      },
      (error) => {
        console.error('Error fetching transaction descriptions:', error);
      }
    );
  }
  getDescriptionName(id: number): string {
    const description = this.transactionDescriptiuonNames.find((x: { id: number; }) => x.id === id);
    return description ? description.descriptionName : 'N/A';
  }
}
