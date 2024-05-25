import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { BudgetingService } from '../../../services/budgeting.service';
import { FinancialGoals } from '../../../models/financialGoals';

@Component({
  selector: 'app-add-goals-dialog',
  templateUrl: './add-goals-dialog.component.html',
  styleUrl: './add-goals-dialog.component.css',
})
export class AddGoalsDialogComponent implements OnInit {
  public financialGoalForm!: FormGroup;


  constructor(
    public dialogRef: MatDialogRef<AddGoalsDialogComponent>,
    public budgetingService: BudgetingService,
  ) {}

  ngOnInit(): void {
    this.financialGoalForm = new FormGroup({
      yearlyProfitGoal: new FormControl('', [Validators.required]),
      yearlyGainGoal: new FormControl('', [Validators.required]),
      yearlySpentLimit: new FormControl('', [Validators.required]),
      monthlyProfitGoal: new FormControl('', [Validators.required]),
      monthlyGainGoal: new FormControl('', [Validators.required]),
      monthlySpentLimit: new FormControl('', [Validators.required]),
    });

  }

  public onSubmit(){
    const newFinancialGoal: FinancialGoals = {
      yearlyProfitGoal: this.financialGoalForm.get('yearlyProfitGoal')!.value,
      yearlyGainGoal: this.financialGoalForm.get('yearlyGainGoal')!.value,
      yearlySpentLimit: this.financialGoalForm.get('yearlySpentLimit')!.value,
      monthlyProfitGoal: this.financialGoalForm.get('monthlyProfitGoal')!.value,
      monthlyGainGoal: this.financialGoalForm.get('monthlyGainGoal')!.value,
      monthlySpentLimit: this.financialGoalForm.get('monthlySpentLimit')!.value,
    };
    this.budgetingService.createFinancialGoal(newFinancialGoal);
    this.dialogRef.close();
  }

  onCancelClick(): void {
    this.dialogRef.close();
  }
}
