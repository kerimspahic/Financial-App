import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";

@Injectable({
    providedIn: 'root'
})
export class WeatherClient {
    baseUrl = 'https://localhost:5001';
    constructor(private http: HttpClient) {}

    getWeatherData(): Observable<any> {
        return this.http.get(this.baseUrl + '/WeatherForecast');
    }
}