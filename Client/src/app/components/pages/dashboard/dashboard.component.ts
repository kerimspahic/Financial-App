import { Component, OnInit } from '@angular/core';
import { TransactionService } from '../../../services/transaction.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {

  selectedChartView: string = 'monthly';
  
  totalProfit: number = 0;
  profit: number = 0;
  gain: number = 0;
  spent: number = 0;

  totalProfitGoal: number = 0;
  profitGoal: number = 0;
  gainGoal: number = 0;
  spentLimit: number = 0;

  constructor(public transactionService: TransactionService) { }


  ngOnInit(): void { 
    this.fetchCardValues('total');
    this.fetchCardValues(this.selectedChartView);
    this.fetchFinancialGoals(this.selectedChartView);
  }

  onChartViewChange(view: string): void {
    this.selectedChartView = view;
    this.fetchCardValues(view);
    this.fetchFinancialGoals(view);
  }

  fetchCardValues(mode: string): void {
    this.transactionService.getCardValues(mode).subscribe(data => {
      if (mode === 'total') {
        this.totalProfit = data.profit;
      } else {
        this.profit = data.profit;
        this.gain = data.gain;
        this.spent = data.spent;
      }
    });
  }

  fetchFinancialGoals(mode: string): void {
    this.transactionService.getFinancialGoals(mode).subscribe(data => {
      this.totalProfitGoal = data.totalProfit;
      this.profitGoal = data.profitGoal;
      this.gainGoal = data.gainGoal;
      this.spentLimit = data.spentLimit;
    });
  }
}