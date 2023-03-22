import { Component, OnInit } from '@angular/core';
import { LoginResponse, OidcSecurityService } from 'angular-auth-oidc-client';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'Angular Client';
  apiMsg = '';

  constructor(
    public oidcSecurityService: OidcSecurityService,
    public http: HttpClient
  ) {}

  ngOnInit(): void {
    this.oidcSecurityService
      .checkAuth()
      .subscribe((loginResponse: LoginResponse) => {
        const { isAuthenticated, userData, accessToken, idToken, configId } =
          loginResponse;
        console.log(loginResponse);
      });
  }

  login() {
    this.oidcSecurityService.authorize();
  }

  logout() {
    this.oidcSecurityService
      .logoff()
      .subscribe((result) => console.log(result));
  }

  callAPI() {
    this.oidcSecurityService.getAccessToken().subscribe((token) => {
      this.http
        .get('http://localhost:1427/secret', {
          headers: new HttpHeaders({
            Authorization: 'Bearer ' + token,
          }),
          responseType: 'text',
        })
        .subscribe(
          (res) => {
            console.log(res);
            this.apiMsg = res;
          },
          (err) => {
            console.error(err);
            // error because it is not in json format
            // this.apiMsg = err?.error?.text;
          }
        );
    });
  }
}
