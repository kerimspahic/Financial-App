import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './Components/dashboard/dashboard.component';
import { authGuard } from './helpers/auth.guard';
import { LoginComponent } from './Components/login/login.component';
import { RegisterComponent } from './Components/register/register.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,
    canActivate: [authGuard]
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
