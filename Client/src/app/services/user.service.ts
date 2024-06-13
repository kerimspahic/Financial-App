import { Injectable } from '@angular/core';
import { UserClient } from '../client/user.client';
import { UserPassword } from '../models/userPassword';
import { UserInfo } from '../models/userInfo';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private userClient: UserClient) {}

  public editUserPassword(userPassword: UserPassword): void {
    this.userClient.updatePassword(userPassword).subscribe();
  }

  public editUserInfo(userInfo: UserInfo): void {
    this.userClient.updateUser(userInfo).subscribe();
  }
  
  public editUserEmail(newEmail: string | null){
    this.userClient.updateEmail(newEmail).subscribe();
  }

  
}
