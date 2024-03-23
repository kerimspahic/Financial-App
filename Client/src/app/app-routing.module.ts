import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './Components/dashboard/dashboard.component';
import { authGuard } from './helpers/auth.guard';
import { LoginComponent } from './Components/login/login.component';
import { RegisterComponent } from './Components/register/register.component';
import { TransactionsComponent } from './Components/transactions/transactions.component';
import { BudgetingComponent } from './Components/budgeting/budgeting.component';
import { SummaryComponent } from './Components/summary/summary.component';
import { SettingsComponent } from './Components/settings/settings.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,
    canActivate: [authGuard],
  },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [authGuard],
    children: [
      {
        path: 'transactions',
        component: TransactionsComponent,
      },
      {
        path: 'budgeting',
        component: BudgetingComponent,
      },
      {
        path: 'summary',
        component: SummaryComponent,
      },
      {
        path: 'settings',
        component: SettingsComponent,
      },
    ],
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'register',
    component: RegisterComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
