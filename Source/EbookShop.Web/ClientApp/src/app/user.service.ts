import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()

export class UserService {

  private loggedIn = false;

  constructor(private httpClient: HttpClient) {
   this.loggedIn = !!localStorage.getItem('auth_token');
   }

  register(email: string, password: string, confirmPass: string, firstName: string, lastName: string) {

    var model = {
      email: email,
      password: password,
      confirmPassword: confirmPass,
      firstName: firstName,
      lastName: lastName
    };


    return this.httpClient
      .post(window.location.protocol + "//" + window.location.host + "/api/register",
        model,
      { responseType: 'text' });
  }

  login(userName, password) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');

    var model = {
      userName: userName,
      password: password,
      };

    return this.httpClient
      .post(window.location.protocol + "//" + window.location.host + "/api/login",
        model,
      { responseType: 'text' })
      .toPromise()
      .then(x => {
        localStorage.setItem('auth_token', "TODO mock token");
        return x;
      });
  }

  logout() {
    localStorage.removeItem('auth_token');
    this.loggedIn = false;
  }

  isLoggedIn() {
    return !!localStorage.getItem('auth_token');
  }
}
