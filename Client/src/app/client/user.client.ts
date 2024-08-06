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
  constructor(private http: HttpClient) {}

  public updatePassword(userPassword: UserPassword): Observable<Object> {
    return this.http.put(environment.userUrl + 'UpdateUserPassword', userPassword);
  }

  public updateUser(userInfo: UserInfo): Observable<Object> {
    return this.http.put(environment.userUrl + 'UpdateUserInfo', userInfo);
  }
  updateEmail(newEmail: string | null): Observable<Object> {
    return this.http.put(environment.userUrl + 'UpdateUserEmail', newEmail);
  }

  getAllUsers(): Observable<Object> {
    return this.http.get(environment.userUrl + 'GetAppUsers');
  }
}
