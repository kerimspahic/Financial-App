import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { Transactions } from "../models/transactions";
import { Observable } from "rxjs";

export class TransactionsExtensions extends DataSource<Transactions> {
    override connect(collectionViewer: CollectionViewer): Observable<readonly Transactions[]> {
        throw new Error("Method not implemented.");
    }
    override disconnect(collectionViewer: CollectionViewer): void {
        throw new Error("Method not implemented.");
    }


}