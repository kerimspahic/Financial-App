export class User {
  public userName: string;
  public email: string;
  public roles: string[];
  public token: string;

  constructor(userName: string, email: string, roles: string[], token: string) {
    this.userName = userName;
    this.email = email;
    this.roles = roles;
    this.token = token;
  }
}
