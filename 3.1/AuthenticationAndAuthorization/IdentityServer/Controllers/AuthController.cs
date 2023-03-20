using IdentityServer.ViewModels;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    public class AuthController : Controller
    {
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(
            IIdentityServerInteractionService interactionService,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _interactionService = interactionService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Login(string returnUrl)
        {
            var externalProviders = await _signInManager.GetExternalAuthenticationSchemesAsync();
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalProviders = externalProviders,
                Username="bob",
                Password="password"
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            // check if model is valid

            var result = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, false, false);

            if (result.Succeeded)
            {
                return Redirect(vm.ReturnUrl);
            }

            return View(vm);
        }

        public async Task<IActionResult> ExternalLogin(string provider, string returnUrl)
        {
            var redirectUri = Url.Action(nameof(ExternalLoginCallback), "Auth", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUri);
            return Challenge(properties, provider);
        }

        public async Task<IActionResult> ExternalLoginCallback(string returnUrl)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null) return RedirectToAction(nameof(Login));

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

            if (result.Succeeded) return Redirect(returnUrl);

            // otherwise register user for first time
            var username = info.Principal.FindFirst(ClaimTypes.Name).Value.Replace(" ", "_");
            return View(nameof(ExternalRegister), new ExternalRegisterViewModel
            {
                Username = username,
                ReturnUrl = returnUrl
            });
        }

        public IActionResult Register(string returnUrl)
        {
            return View(new RegisterViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            // check if model is valid
            if (!ModelState.IsValid) return View(vm);

            var user = new IdentityUser(vm.Username);

            var result = await _userManager.CreateAsync(user, vm.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);

                return Redirect(vm.ReturnUrl);
            }

            return View();
        }

        public async Task<IActionResult> ExternalRegister(ExternalRegisterViewModel vm)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null) return RedirectToAction(nameof(Login));

            var user = new IdentityUser(vm.Username);
            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded) return View(vm);

            result = await _userManager.AddLoginAsync(user, info);
            if (!result.Succeeded) return View(vm);

            await _signInManager.SignInAsync(user, false);

            return Redirect(vm.ReturnUrl);
        }

        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();

            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
            if (string.IsNullOrWhiteSpace(logoutRequest.PostLogoutRedirectUri))
            {
                return RedirectToAction("Index", "Home");
            }

            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }
    }
}
