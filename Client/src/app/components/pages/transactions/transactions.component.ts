import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Exchange } from '../../../models/exchange';
import { AuthenticationService } from '../../../services/authentication.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { ExchangeClient } from '../../../client/exchange.client';
import { ExchangeService } from '../../../services/exchange.service';
import { TransactionDescriptions } from '../../../models/transactionDescriptions';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.css',
})
export class TransactionsComponent implements OnInit {
  public exchngeForm!: FormGroup;
  public transactionDescriptiuonNames!: any; //fix this
  displayedColumns = ['amount', 'type', 'date', 'description'];
  dataSource = new MatTableDataSource<Exchange>();
  
  filterDates = (d: Date | null) => {
    const today = new Date();
    if (d == null) 
      return false;
    return d <= today;
  }

  constructor(
    public authService: AuthenticationService,
    public exchngeClient: ExchangeClient,
    public exchngeService: ExchangeService) {}

  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngOnInit(): void {
    this.exchngeForm = new FormGroup({
      exchangeAmount: new FormControl('', [Validators.required]),
      exchangeType: new FormControl('', [Validators.required]),
      exchangeDate: new FormControl('', [Validators.required]),
      exchangeDescription: new FormControl('', [Validators.required]),
    });

    this.loadTransactionDescriptionNames() ;

    this.loadExchangeData();
  }

  public onSubmit() {


    const newUserTransaction: Exchange = {
      exchangeAmount: this.exchngeForm.get('exchangeAmount')!.value,
      exchangeType: this.exchngeForm.get('exchangeType')!.value,
      exchangeDate: this.exchngeForm.get('exchangeDate')!.value,
      exchangeDescription: this.exchngeForm.get('exchangeDescription')!.value,
    };

    this.exchngeService.sendExchangeData(newUserTransaction);

    this.loadExchangeData();
    this.exchngeForm.reset();
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
          return this.compare(a.exchangeAmount, b.exchangeAmount, isAsc);
        case 'type':
          return this.compare(a.exchangeType, b.exchangeType, isAsc);
        case 'date':
          return this.compare(a.exchangeDate, b.exchangeDate, isAsc);
        case 'description':
          return this.compare(
            a.exchangeDescription,
            b.exchangeDescription,
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

  loadExchangeData() {
    this.exchngeClient.getExchangeData().subscribe((data) => {
      this.dataSource.data = data;
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
    });
  }
  loadTransactionDescriptionNames() {
    this.exchngeClient.getTransactionDesciptionNames().subscribe(
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
