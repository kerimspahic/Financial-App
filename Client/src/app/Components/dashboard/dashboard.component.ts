import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../../services/authentication.service';
import { WeatherClient } from '../../client/weather.client';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {
  public user: string | null = null;
  public weather: Observable<any> = this.weatherClient.getWeatherData();
  constructor( public authService: AuthenticationService, private weatherClient: WeatherClient ) {}

  
  ngOnInit(): void {}

  logout(): void {
    this.authService.logout();
  }
}