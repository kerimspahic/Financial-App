import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { TransactionService } from '../../../services/transaction.service';
import moment from 'moment';

@Component({
  selector: 'app-line-chart',
  templateUrl: './line-chart.component.html',
  styleUrl: './line-chart.component.css',
})
export class LineChartComponent implements OnInit {
  @Input() view: string = 'monthly';

  transactions: any;

  public lineChartData: Array<any> = [];
  public lineChartLabels!: any[];
  public lineChartLegend = true;
  public lineChartOptions: any = {
    responsive: true,
  };

  constructor(private transactionService: TransactionService) {}

  ngOnInit(): void {
    this.loadChartData();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['view']) {
      this.loadChartData();
    }
  }

  loadChartData(): void {
    let chartTypes: string[];
    let chartLabels: string[];

    if (this.view === 'monthly') {
      chartTypes = ['monthlygain', 'monthlyspent', 'monthlyprofit'];
      chartLabels = ['Monthly Gain', 'Monthly Spent', 'Monthly Profit'];
    } else if (this.view === 'yearly') {
      chartTypes = ['yearlygain', 'yearlyspent', 'yearlyprofit'];
      chartLabels = ['Yearly Gain', 'Yearly Spent', 'Yearly Profit'];
    } else {
      chartTypes = ['totalgain', 'totalspent', 'totalprofit'];
      chartLabels = ['Total Gain', 'Total Spent', 'Total Profit'];
    }

    Promise.all(
      chartTypes.map((type) =>
        this.transactionService.getChartData(type).toPromise()
      )
    ).then((responses) => {
      const formattedData = responses.map((res) => this.getData(res));
      this.lineChartLabels = formattedData[0].map((data: any) => data[0]); // assuming all responses have the same dates

      this.lineChartData = formattedData.map((data, index) => ({
        data: data.map((d: any) => d[1]),
        label: chartLabels[index],
      }));
    });
  }

  private getData(res: Response) {
    this.transactions = res;

    const formatedValues = this.transactions.reduce(
      (r: any[][], e: { date: moment.MomentInput; value: any }) => {
        r.push([moment(e.date).format('YY-MM-DD'), e.value]);
        return r;
      },
      []
    );

    const summedValues = formatedValues.reduce((acc: any, curr: any) => {
      const date = curr[0];
      const value = curr[1];
      if (!acc[date]) {
        acc[date] = 0;
      }
      acc[date] += value;
      return acc;
    }, []);

    return Object.entries(summedValues).map(([date, value]) => [date, value]);
  }
}
