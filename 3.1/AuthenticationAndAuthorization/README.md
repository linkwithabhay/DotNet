###### .Net Core 3.1

# Authentication and Authorization

---

### Important Links

- [IdentityServer4 Documentation](https://identityserver4.readthedocs.io/en/latest/index.html)

- [OIDC Client NPM Package](https://www.npmjs.com/package/oidc-client)

- [Angular OIDC NPM Package](https://www.npmjs.com/package/angular-oauth2-oidc)

- [Working with Certificates in PowerShell](https://learn.microsoft.com/en-us/archive/blogs/kaevans/using-powershell-with-certificates)

---

## [Basics](./Basics/)

- Auth Token : `Cookie`

* Basic Authentication and Authorization using `Cookie` : [[Ep.1]](https://www.youtube.com/watch?v=Fhfvbl_KbWo&list=PLOeFnOV9YBa7dnrjpOG6lMpcyd7Wn7E8V&index=1)
  - Claims
  - ClaimsIdentity
  - ClaimsPrincipal

- Authorization by `Policy` : [[Ep.3]](https://www.youtube.com/watch?v=RBMO_hruKaI&list=PLOeFnOV9YBa7dnrjpOG6lMpcyd7Wn7E8V&index=4)
  - Default Policy for all routes
  - Default RequireClaim for a Policy
  - Custom RequireClaim for Policy :
    - CustomRequireClaim using `IAuthorizationRequirement`
    - CustomRequireClaimHandler using `AuthorizationHandler`
    - `AuthorizationPolicyBuilderExtensions`

* `IAuthorizationService` usage at different levels : [[Ep.4]](https://www.youtube.com/watch?v=n-g9O0dOV9A&list=PLOeFnOV9YBa7dnrjpOG6lMpcyd7Wn7E8V&index=5)
  - In Controller
  - In Razor View

- Service Injection at different places : [[Ep.4]](https://www.youtube.com/watch?v=n-g9O0dOV9A&list=PLOeFnOV9YBa7dnrjpOG6lMpcyd7Wn7E8V&index=5)
  - In class constructor
  - In a single Controller Method
  - In Razor Views

* Global Authorization Filter with custom default Policy : [[Ep.4]](https://www.youtube.com/watch?v=n-g9O0dOV9A&list=PLOeFnOV9YBa7dnrjpOG6lMpcyd7Wn7E8V&index=5)
  - Usage in [Startup Class](./Basics/Startup.cs)

- Operation Authorization Requirement : [[Ep.4]](https://www.youtube.com/watch?v=n-g9O0dOV9A&list=PLOeFnOV9YBa7dnrjpOG6lMpcyd7Wn7E8V&index=5)
  - Use of inbuilt `OperationAuthorizationRequirement` using `AuthorizationHandler`
  - Operations available for Authorization
  - Resource for Authorization
  - Usage in [OperationsController](./Basics/Controllers/OperationsController.cs)

* Claims Transformation using `IClaimsTransformation` : [[Ep.4]](https://www.youtube.com/watch?v=n-g9O0dOV9A&list=PLOeFnOV9YBa7dnrjpOG6lMpcyd7Wn7E8V&index=5)
  - Usage in [ClaimsTransformation](./Basics/Transformer/ClaimsTransformation.cs)

- CustomAuthorizationPolicyProvider retaining `DefaultAuthorizationPolicyProvider` : [[Ep.4]](https://www.youtube.com/watch?v=n-g9O0dOV9A&list=PLOeFnOV9YBa7dnrjpOG6lMpcyd7Wn7E8V&index=5)

  - Override `GetPolicyAsync` method
  - Defining `DynamicPolicies`
  - Creating `AuthorizationPolicy` in `DynamicAuthorizationPolicyFactory`
  - Implementing `SecurityLevelRequirement` using `IAuthorizationRequirement`
  - Implementing `SecurityLevelHandler` using `AuthorizationHandler`
  - Building `SecurityLevelAttribute` using `AuthorizeAttribute`
  - Implementation in [CustomAuthorizationPolicyProvider](./Basics/CustomPolicyProvider/CustomAuthorizationPolicyProvider.cs)
  - Usage in [HomeController](./Basics/Controllers/HomeController.cs)

* Razor Pages Authorization: [[Ep.4.1]](https://www.youtube.com/watch?v=yz0trzQ0KXY&list=PLOeFnOV9YBa7dnrjpOG6lMpcyd7Wn7E8V&index=6)
  - Authorize a Page
  - Authorize a Page with Policy
  - Authorize a Folder
  - Implementation in [Startup Class](./Basics/Startup.cs)

---

## [IdentityExample](./IdentityExample/)

- Auth Token : `Cookie`

* Basic Authentication and Authorization using `Microsoft.AspNetCore.Identity.EntityFrameworkCore` Package : [[Ep.2]](https://www.youtube.com/watch?v=IjbtWPXVJGw&list=PLOeFnOV9YBa7dnrjpOG6lMpcyd7Wn7E8V&index=2)
  - Identity DbContext
  - Identity UserManager
  - Identity SignInManager

- Email verification using `NETCore.MailKit` Package : [[Ep.2.1]](https://www.youtube.com/watch?v=Vj7iCb7wDs0&list=PLOeFnOV9YBa7dnrjpOG6lMpcyd7Wn7E8V&index=3)
  - Email Verification

---

## [OAuth.Server](./OAuth.Server/)

- Auth Token : `JWT`

* Basic Authentication and Authorization using `Json Web Token` or `JWT` : [[Ep.5]](https://www.youtube.com/watch?v=YC4ewe7Rbl4&list=PLOeFnOV9YBa7dnrjpOG6lMpcyd7Wn7E8V&index=7)
  - Configuration for JWT
  - Creating and Signing key
  - Implementation and Usage in [HomeController](./OAuth.Server/Controllers/HomeController.cs) and [Statup Class](./OAuth.Server/Startup.cs)

- Server OAuth Configuration : [[Ep.6]](https://www.youtube.com/watch?v=0oBIgPaFYOg&list=PLOeFnOV9YBa7dnrjpOG6lMpcyd7Wn7E8V&index=8)
  - Various Query Parameters required for OAuth
  - Authentication
  - Token Generation
  - Implementation in [OAuthController](./OAuth.Server/Controllers/OAuthController.cs)

* Token Validation endpoint : [[Ep.7]](https://www.youtube.com/watch?v=0A2HW6cRL5M&list=PLOeFnOV9YBa7dnrjpOG6lMpcyd7Wn7E8V&index=10)
  - Implemented in [OAuthController](./OAuth.Server/Controllers/OAuthController.cs), Action Name `Validate`

- Refresh Token Setup at Token endpoint : [[Ep.8]](https://www.youtube.com/watch?v=POGXYbqhL3M&list=PLOeFnOV9YBa7dnrjpOG6lMpcyd7Wn7E8V&index=10)
  - Implemented in [OAuthController](./OAuth.Server/Controllers/OAuthController.cs), Action Name `Token`

---

## [OAuth.Client](./OAuth.Client/)

- Auth Token : `Cookie`

* Client OAuth Configuration : [Ep.6](https://www.youtube.com/watch?v=0oBIgPaFYOg&list=PLOeFnOV9YBa7dnrjpOG6lMpcyd7Wn7E8V&index=8)
  - `DefaultAuthenticateScheme` and `DefaultSignInScheme` is `Cookie`
  - `DefaultChallengeScheme` is [`OAuth.Server`](./OAuth.Server/)
  - Endpoints Configuration for OAuth
  - Clients Id and Secret
  - Extracting User Claims from JWT Payload
  - Implementation in [Startup Class](./OAuth.Client/Startup.cs)

- Refresh Token : [[Ep.8]](https://www.youtube.com/watch?v=POGXYbqhL3M&list=PLOeFnOV9YBa7dnrjpOG6lMpcyd7Wn7E8V&index=10)
  - SecuredGetRequest : GET method with authorization token
  - RefreshAccessToken : refresh token method
  - AccessTokenRefreshWraper : calls RefreshAccessToken if token is expired
  - Implemented in [HomeController](./OAuth.Client/Controllers/HomeController.cs) and used at `Secret` Action Method.

---

## [OAuth.API](./OAuth.API/)

- Auth Token : `JWT`

* API setup for accessing authorized endpoint : [[Ep.7]](https://www.youtube.com/watch?v=0A2HW6cRL5M&list=PLOeFnOV9YBa7dnrjpOG6lMpcyd7Wn7E8V&index=10)
  - JWT Requirements Authorization Handler [JwtRequirement](./OAuth.API/Requirements/JwtRequirement.cs)
  - Authorization Configuration in [Startup Class](./OAuth.API/Startup.cs)
  - extracting token from the request
  - authorization done by [OAuth.Server](./OAuth.Server/)
  - endpoint accessed by [OAuth.Client](./OAuth.Client)

---

## [IdentityServer](./IdentityServer/)

- Auth Token : `JWT`

* SignOut Functionality : [[Ep.18]]()

- External OAuth Providers : [[Ep.19]]()

* PKCE Setup : [[Ep.20]]()

---

## [IdentityServer.ApiOne](./IdentityServer.ApiOne/)

- Auth Token : `JWT` (done by server)

---

## [IdentityServer.ApiTwo](./IdentityServer.ApiTwo/)

- Auth Token : `JWT` (done by server)

---

## [IdentityServer.MvcClient](./IdentityServer.MvcClient/)

- Auth Token : `Cookie` and `JWT`

* OIDC Setup : [[Ep.10]]()

- Identity Setup for OIDC : [[Ep.11]]()
  - Login or Register User with additional default User

* Cookies, `id_token`, `access_token`, `Claims` : [[Ep.12]]()

- Setup for `refresh_token` : [[Ep.13]]()

* SignOut Functionality : [[Ep.18]]()

- PKCE Setup : [[Ep.20]]()

---

## [IdentityServer.JavascriptClient](./IdentityServer.JavascriptClient/)

- Auth Token : `Cookie` and `JWT`

* Javascript Client Setup : [[Ep.14]]()
  - Redirect to login
  - Extract Tokens

- Using `oidc-client` Package : [[Ep.15]]()
  - call api through `axios`

* Silent SignIn if `access_token` is expired : [[Ep.16]]()
  - Silent SignIn using `oidc-client` Package

- Entity Framework Setup instead of InMemory : [[Ep.17]]()
  - Seeding Data

* SignOut Functionality : [[Ep.18]]()

- PKCE Setup : [[Ep.20]]()
