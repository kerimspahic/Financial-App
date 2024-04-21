import { Injectable } from '@angular/core';
import { UserClient } from '../client/user.client';
import { UserPassword } from '../models/userPassword';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private userClient: UserClient) { }

  public editUserPassword(userPassword: UserPassword): void{
    this.userClient.updatePassword(userPassword).subscribe();
  }
}
