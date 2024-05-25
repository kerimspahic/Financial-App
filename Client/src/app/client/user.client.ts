import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoaderService } from '../services/loader.service';
import { UserPassword } from '../models/userPassword';
import { Observable, finalize } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { UserInfo } from '../models/userInfo';

@Injectable({
  providedIn: 'root',
})
export class UserClient {
  constructor(private http: HttpClient, private loaderService: LoaderService) {}
  
  public updatePassword(userPassword: UserPassword): Observable<Object> {
    this.loaderService.show();
    return this.http.put(environment.userUrl + 'UpdateUserPassword', userPassword).pipe(
      finalize(() => {
        this.loaderService.hide();
      })
    );
  }

  public updateUser(userInfo: UserInfo): Observable<Object> {
    this.loaderService.show();
    return this.http.put(environment.userUrl + 'UpdateUserInfo', userInfo).pipe(
      finalize(() => {
        this.loaderService.hide();
      })
    );
  }
  updateEmail(newEmail: string | null): Observable<Object> {
    this.loaderService.show();
    return this.http.put(environment.userUrl + 'UpdateUserEmail', newEmail).pipe(
      finalize(() => {
        this.loaderService.hide();
      })
    );
  }
}
