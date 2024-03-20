import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Transactions } from "../models/transactions";

@Injectable({
    providedIn: 'root'
})
export class TransactionsClient {
    baseUrl = 'https://localhost:5001';
    constructor(private http: HttpClient) { }

    public getTransactionsData(): Observable<any> {
        return this.http.get(this.baseUrl + '/Transactions/extract');
    }

    public insertTransactionData(amount: number, type: boolean, date: string, description: string, userName: string): Observable<string> {
        console.log(amount, date, description, type, userName);
        return this.http.post(
            this.baseUrl + '/Transactions/insert',
            {
                amount: amount,
                type: type,
                date: date,
                description: description,
                userName: userName
            },
            { responseType: 'text' }
        );
    }
}