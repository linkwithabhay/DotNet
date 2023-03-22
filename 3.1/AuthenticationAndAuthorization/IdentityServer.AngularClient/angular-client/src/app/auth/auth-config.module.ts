import { NgModule } from '@angular/core';
import { AuthModule, LogLevel } from 'angular-auth-oidc-client';

@NgModule({
  imports: [
    AuthModule.forRoot({
      config: {
        // authority: 'http://localhost:51854',
        authority: 'https://localhost:44378',
        clientId: 'client_id_angular',
        responseType: 'code',
        scope: 'openid profile ApiOne', // 'openid profile offline_access ' + your scopes
        redirectUrl: window.location.origin,
        postLogoutRedirectUri: window.location.origin,
        logLevel: LogLevel.Debug,
        // silentRenew: true,
        // useRefreshToken: true,
        // renewTimeBeforeTokenExpiresInSeconds: 30,
      },
    }),
  ],
  exports: [AuthModule],
})
export class AuthConfigModule {}
