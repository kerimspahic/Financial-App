import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoaderService } from '../services/loader.service';
import { UserPassword } from '../models/userPassword';
import { Observable, finalize } from 'rxjs';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})

export class UserClient {
  constructor(private http: HttpClient, private loaderService: LoaderService) {}

  public updatePassword(userPassword: UserPassword): Observable<Object> {
    this.loaderService.show();
    return this.http
      .put(environment.userUrl + 'UpdatePassword', userPassword)
      .pipe(
        finalize(() => {
          this.loaderService.hide();
        })
      );
  }
}
