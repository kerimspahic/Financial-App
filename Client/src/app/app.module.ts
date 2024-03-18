import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './Components/navbar/navbar.component';
import { DashboardComponent } from './Components/dashboard/dashboard.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { tokenInterceptor } from './helpers/token.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule} from '@angular/material/button';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import { LoginComponent } from './Components/login/login.component';
import { RegisterComponent } from './Components/register/register.component';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatMenuModule} from '@angular/material/menu';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';
import { SidebarComponent } from './Components/sidebar/sidebar.component';
import { FlexLayoutServerModule } from '@angular/flex-layout/server';
@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    DashboardComponent,
    LoginComponent,
    RegisterComponent,
    SidebarComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    MatButtonModule,
    MatFormFieldModule,
    MatCardModule,
    MatInputModule,
    MatToolbarModule,
    MatMenuModule,
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot(),
    FlexLayoutServerModule 
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: tokenInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
