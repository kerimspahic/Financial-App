import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { BudgetingService } from '../../../services/budgeting.service';
import { AddGoalsDialogComponent } from '../../extras/add-goals-dialog/add-goals-dialog.component';
import { AutomaticTransaction } from '../../../models/automaticTransaction';
import { NewAutomaticTransactionDialogComponent } from '../../extras/new-automatic-transaction-dialog/new-automatic-transaction-dialog.component';
import { TransactionDescriptions } from '../../../models/transactionDescriptions';
import { TransactionClient } from '../../../client/transaction.client';

interface Goal {
  id: number;
  value: string;
  title: string;
}

@Component({
  selector: 'app-budgeting',
  templateUrl: './budgeting.component.html',
  styleUrls: ['./budgeting.component.css'],
})
export class BudgetingComponent implements OnInit {
  public transactionDescriptionNames: TransactionDescriptions[] = [];
  financialGoals: any = {};
  newValue!: number;
  goals: Goal[] = [
    { id: 1, value: 'totalProfitGoal', title: 'Total Profit Goal' },
    { id: 2, value: 'yearlyProfitGoal', title: 'Yearly Profit Goal' },
    { id: 3, value: 'yearlyGainGoal', title: 'Yearly Gain Goal' },
    { id: 4, value: 'yearlySpentLimit', title: 'Yearly Spent Limit' },
    { id: 5, value: 'monthlyProfitGoal', title: 'Monthly Profit Goal' },
    { id: 6, value: 'monthlyGainGoal', title: 'Monthly Gain Goal' },
    { id: 7, value: 'monthlySpentLimit', title: 'Monthly Spent Limit' },
  ];

  automaticTransactions: AutomaticTransaction[] = [];
  editingGoalId: number | null = null;

  constructor(
    private transactionClient: TransactionClient,
    private budgetingService: BudgetingService,
    public dialog: MatDialog
  ) {}

  ngOnInit() {
    this.loadTransactionDescriptionNames();
    this.loadFinancialGoals();
    this.loadAutomaticTransactions();
  }
  private loadTransactionDescriptionNames(): void {
    this.transactionClient.getTransactionDescriptionNames().subscribe(
      (descriptions: TransactionDescriptions[]) => {
        this.transactionDescriptionNames = descriptions;
      },
      (error) => {
        console.error('Error fetching transaction descriptions:', error);
      }
    );
  }

  public getDescriptionName(id: number): string {
    const description = this.transactionDescriptionNames.find(desc => desc.id === id);
    return description ? description.descriptionName : 'N/A';
  }

  startEditing(goalId: number): void {
    this.editingGoalId = goalId;
  }

  stopEditing(): void {
    this.editingGoalId = null;
  }

  submitEdit(column: string): void {
    console.log(column, this.newValue);
    this.budgetingService.updateFinancialGoal(column, this.newValue);
    this.loadFinancialGoals();
    this.stopEditing();
  }

  loadFinancialGoals() {
    this.budgetingService.getFinancialGoals().subscribe((goals) => {
      this.financialGoals = goals;
    });
  }

  loadAutomaticTransactions() {
    this.budgetingService.getAutomaticTransactions().subscribe((transactions) => {
      this.automaticTransactions = transactions;
    });
  }

  getDaysLeft(nextExecutionDate: Date): number {
    const today = new Date();
    const executionDate = new Date(nextExecutionDate);
    const timeDiff = executionDate.getTime() - today.getTime();
    const daysDiff = Math.ceil(timeDiff / (1000 * 3600 * 24));
    return daysDiff;
  }

  deleteTransaction(transactionId: number): void {
    this.budgetingService.deleteAutomaticTransaction(transactionId).subscribe(() => {
      this.loadAutomaticTransactions();
    });
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(AddGoalsDialogComponent, {
      width: '400px',
    });

    dialogRef.afterClosed().subscribe(() => {
      this.loadFinancialGoals();
    });
  }

  openInsertTransactionDialog(): void {
    const dialogRef = this.dialog.open(NewAutomaticTransactionDialogComponent, {
      width: '400px',
    });

    dialogRef.afterClosed().subscribe((result: AutomaticTransaction) => {
      if (result) {
        this.budgetingService.createAutomaticTransaction(result).subscribe(() => {
          this.loadAutomaticTransactions();
        });
      }
    });
  }

  confirmDelete(): void {}
}
