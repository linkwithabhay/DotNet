using Basics.CustomPolicyProvider;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Basics.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthorizationService _authorizationService;

        public HomeController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        [SecurityLevel(5)]
        public IActionResult SecretLevel()
        {
            return View();
        }

        [SecurityLevel(10)]
        public IActionResult SecretHigherLevel()
        {
            return View(nameof(SecretLevel));
        }

        [Authorize(Policy = "Claim.DoB")]
        public IActionResult SecretPolicy()
        {
            return View();
        }

        [Authorize(Roles = "CustomRole")]
        public IActionResult SecretRole()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(bool withPolicy = false, bool withRole = false, int securityLevel = 1)
        {
            var grandmaClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Bob"),
                new Claim(ClaimTypes.Email, "bob@mail.com"),
                new Claim("Grandma.Says", "Very noice boi."),
                new Claim(DynamicPolicies.SecurityLevel, securityLevel.ToString())
            };
            if (withPolicy) grandmaClaims.Add(new Claim(ClaimTypes.DateOfBirth, "01/01/2001"));
            if (withRole) grandmaClaims.Add(new Claim(ClaimTypes.Role, "CustomRole"));
            var licenseClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Bob Who"),
                new Claim("DrivingLicense", "A+")
            };

            var grandmaIdentity = new ClaimsIdentity(grandmaClaims, "Grandma Identity");
            var licenseIdentity = new ClaimsIdentity(licenseClaims, "Government");

            var userPrincipals = new ClaimsPrincipal(new[] { grandmaIdentity, licenseIdentity });

            await HttpContext.SignInAsync(userPrincipals);
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> DoStuff(
            [FromServices] IAuthorizationService authorizationService)
        {
            // *** Services required only in one method can be called directly into the method ***

            // things we are doing and now we want to authorize User

            // var authResult = await _authorizationService.AuthorizeAsync(User, "Claim.DoB");
            // OR
            var policyBuilder = new AuthorizationPolicyBuilder("Schema");
            var customPolicy = policyBuilder.RequireClaim("Claim.DoB").Build();
            var authResult = await _authorizationService.AuthorizeAsync(User, customPolicy);

            if (authResult.Succeeded)
            {
                // Do something with authorized user
            }

            return View("Index");
        }
    }
}
