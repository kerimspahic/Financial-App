import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/authentification/login/login.component';
import { RegisterComponent } from './components/authentification/register/register.component';
import { DashboardComponent } from './components/pages/dashboard/dashboard.component';
import { TransactionsComponent } from './components/pages/transactions/transactions.component';
import { BudgetingComponent } from './components/pages/budgeting/budgeting.component';
import { SummaryComponent } from './components/pages/summary/summary.component';
import { authGuard } from './helpers/auth.guard';
import { AdminComponent } from './components/pages/admin/admin.component';
import { adminGuard } from './helpers/admin.guard';
import { RegistrationSuccessComponent } from './components/authentification/registration-success/registration-success.component';
import { ConfirmEmailComponent } from './components/authentification/confirm-email/confirm-email.component';

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
        path: 'dashboard',
        component: DashboardComponent,
      },
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
        path: 'admin',
        component: AdminComponent,
        canActivate: [adminGuard],
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
  { 
    path: 'registration-success', 
    component: RegistrationSuccessComponent 
  },
  { 
    path: 'confirm-email', 
    component: ConfirmEmailComponent 
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
