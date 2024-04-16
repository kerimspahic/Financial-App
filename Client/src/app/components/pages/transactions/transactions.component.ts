import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Exchange } from '../../../models/exchange';
import { AuthenticationService } from '../../../services/authentication.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { ExchangeClient } from '../../../client/exchange.client';
import { ExchangeService } from '../../../services/exchange.service';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.css',
})
export class TransactionsComponent implements OnInit  {

  public exchngeForm!: FormGroup;

  displayedColumns = ['amount', 'type', 'date', 'description'];
  dataSource = new MatTableDataSource<Exchange>();

  constructor(
    public authService: AuthenticationService,
    public exchngeClient: ExchangeClient,
    public exchngeService: ExchangeService
  ) {}

  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngOnInit(): void {
    this.exchngeForm = new FormGroup({
      exchangeAmount: new FormControl('', [Validators.required]),
      exchangeType: new FormControl('', [Validators.required]),
      exchangeDate: new FormControl('', [Validators.required]),
      exchangeDescription: new FormControl('', [Validators.required])
    });

    this.loadExchangeData();
  }

  public onSubmit() {
    
    if (this.exchngeForm.invalid) {
      return;
    }

    this.exchngeService.setExchangeData(
      this.exchngeForm.get('exchangeAmount')!.value,
      this.exchngeForm.get('exchangeType')!.value,
      this.exchngeForm.get('exchangeDate')!.value,
      this.exchngeForm.get('exchangeDescription')!.value
    );

    this.loadExchangeData();
    this.exchngeForm.reset();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) 
      this.dataSource.paginator.firstPage();
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
          return this.compare(a.exchangeAmount, b.exchangeAmount, isAsc);
        case 'type':
          return this.compare(a.exchangeType, b.exchangeType, isAsc);
        case 'date':
          return this.compare(a.exchangeDate, b.exchangeDate, isAsc);
        case 'description':
          return this.compare(a.exchangeDescription, b.exchangeDescription, isAsc);
        default:
          return 0;
      }
    });
  }

  private compare(a: number | string | Date, b: number | string | Date, isAsc: boolean) {
    if (a === b) {
      return 0;
    }
    return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  }

  loadExchangeData() {
    this.exchngeClient.getExchangeData().subscribe((data) => {
      this.dataSource.data = data;
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
    });
  }
}


/* 
    this.authService.currentUser$.subscribe((user) => {
      if (user) {
        this.userId = user.nameid;
        this.exchngeForm.get('userName')!.setValue(this.userId);
      }
    });
*/