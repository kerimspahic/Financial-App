import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, HttpClientModule, provideHttpClient } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FlexLayoutServerModule } from '@angular/flex-layout/server';
import { LoginComponent } from './components/authentification/login/login.component';
import { RegisterComponent } from './components/authentification/register/register.component';
import { SidebarComponent } from './components/navigation/sidebar/sidebar.component';
import { NavbarComponent } from './components/navigation/navbar/navbar.component';
import { BudgetingComponent } from './components/pages/budgeting/budgeting.component';
import { DashboardComponent } from './components/pages/dashboard/dashboard.component';
import { SummaryComponent } from './components/pages/summary/summary.component';
import { TransactionsComponent } from './components/pages/transactions/transactions.component';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatCommonModule, MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { DatePipe } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { HomeComponent } from './components/home/home.component';
import { TokenInterceptor } from './helpers/token.interceptor';
import { AdminComponent } from './components/pages/admin/admin.component';
import { LoaderComponent } from './components/extras/loader/loader.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatTabsModule } from '@angular/material/tabs';
import { AddTransactionDescriptionDialogComponent } from './components/extras/add-transaction-description-dialog/add-transaction-description-dialog.component';
import { EditTransactionDescriptionDialogComponent } from './components/extras/edit-transaction-description-dialog/edit-transaction-description-dialog.component';
import { EditUserInfoDialogComponent } from './components/extras/edit-user-info-dialog/edit-user-info-dialog.component';
import { BaseChartDirective, provideCharts, withDefaultRegisterables } from 'ng2-charts';
import { LineChartComponent } from './components/extras/line-chart/line-chart.component';
import { AddTransactionDialogComponent } from './components/extras/add-transaction-dialog/add-transaction-dialog.component';
import { PaginationComponent } from './components/extras/pagination/pagination.component';
import { ConfirmEmailComponent } from './components/authentification/confirm-email/confirm-email.component';
import { RegistrationSuccessComponent } from './components/authentification/registration-success/registration-success.component';
import { AddGoalsDialogComponent } from './components/extras/add-goals-dialog/add-goals-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    SidebarComponent,
    NavbarComponent,
    BudgetingComponent,
    DashboardComponent,
    SummaryComponent,
    TransactionsComponent,
    HomeComponent,
    AdminComponent,
    LoaderComponent,
    AddTransactionDescriptionDialogComponent,
    EditTransactionDescriptionDialogComponent,
    EditUserInfoDialogComponent,
    LineChartComponent,
    AddTransactionDialogComponent,
    PaginationComponent,
    ConfirmEmailComponent,
    RegistrationSuccessComponent,
    AddGoalsDialogComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    FlexLayoutServerModule,
    CommonModule,
    DatePipe,
    MatCommonModule,
    MatButtonModule,
    MatFormFieldModule,
    MatCardModule,
    MatInputModule,
    MatToolbarModule,
    MatMenuModule,
    MatSidenavModule,
    MatListModule,
    MatIconModule,
    MatRadioModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatGridListModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    MatPaginatorModule,
    MatDialogModule,
    MatTabsModule,
    BaseChartDirective,
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot(),
  ],
  providers: [
    provideHttpClient(),
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
    provideAnimationsAsync(),
    provideCharts(withDefaultRegisterables()),
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
