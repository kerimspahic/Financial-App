import { Component, Input } from '@angular/core';
import { DashboardCharts } from '../../../models/dashboardCharts';
import { TransactionService } from '../../../services/transaction.service';

@Component({
  selector: 'app-line-chart',
  templateUrl: './line-chart.component.html',
  styleUrl: './line-chart.component.css'
})
export class LineChartComponent {
  @Input() dashboardData!: DashboardCharts;
  // lineChart
  public lineChartData:Array<any> = [
    { data: [], label: 'Deposits' },
    { data: [], label: 'Withdrawals' },
    { data: [], label: 'All Transactions' }
  ];
  public lineChartLabels:Array<any> = [];

  public lineChartOptions:any = {
    responsive: true
  };
  
  public lineChartLegend:boolean = true;

  constructor(private transactionService: TransactionService) {}

  ngOnInit(): void { 
    this.transactionService.getDashboardCharts().subscribe(data => {
      if (data) {
        this.dashboardData = data;
        this.updateChartData();
      }
    });
  }

  private updateChartData(): void {
    if (this.dashboardData) {
      this.lineChartData[0].data = this.dashboardData.transactionDeposits;
      this.lineChartData[1].data = this.dashboardData.transactionWithdrawals;
      this.lineChartData[2].data = this.dashboardData.allTransactions;
      
      this.lineChartLabels = ['Label1', 'Label2', 'Label3', 'Label4', 'Label5', 'Label6', 'Label7'];
    }
  }
}