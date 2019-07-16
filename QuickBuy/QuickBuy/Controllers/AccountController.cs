using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Domain.IDTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace QuickBuy.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AccountIDTO> _userManager;
        private readonly IUserClaimsPrincipalFactory<AccountIDTO> _userClaimsPrincipalFactory;

        public AccountController(UserManager<AccountIDTO> userManager, IUserClaimsPrincipalFactory<AccountIDTO> userClaimsPrincipalFactory)
        {
            this._userManager = userManager;
            this._userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        }

        // GET: Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/SignIn
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginIDTO loginIModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginIModel.Email);
                var paswordCheck = await _userManager.CheckPasswordAsync(user, loginIModel.Password);
                var emailCheck = await _userManager.IsEmailConfirmedAsync(user);
                var lockCheck = await _userManager.IsLockedOutAsync(user);

                if (lockCheck == false)
                {
                    if (user != null)
                    {
                        if (paswordCheck == true)
                        {
                            if (emailCheck == true)
                            {
                                await _userManager.ResetAccessFailedCountAsync(user);

                                var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

                                await HttpContext.SignInAsync("Identity.Application", principal);

                                return RedirectToAction("About");
                            }
                            else
                            {
                                ModelState.AddModelError("", "O Email não foi confirmado!");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Senha inválida!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email inválido!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Usuário bloqueado, aguarde...");

                    await _userManager.AccessFailedAsync(user);

                    // send email here
                }
            }

            return View();
        }

        // GET: Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterIDTO registerIModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(registerIModel.Email);

                if (user == null)
                {
                    user = new AccountIDTO()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = registerIModel.Email
                    };

                    var result = await _userManager.CreateAsync(user, registerIModel.Password);

                    if (result.Succeeded)
                    {
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                        var confimationEmail = Url.Action("ConfirmEmailAddress", "Home",
                            new
                            {
                                token = token,
                                email = user.Email
                            }, Request.Scheme);

                        System.IO.File.WriteAllText("confirmationEmail.txt", confimationEmail);
                    }
                    else
                    {
                        foreach (var erro in result.Errors)
                        {
                            ModelState.AddModelError("", erro.Description);
                        }

                        return View();
                    }
                }

                return View("Success");
            }

            return View();
        }

        // GET: Account/ConfirmationEmailAsync
        [HttpGet]
        public async Task<IActionResult> ConfirmationEmailAsync(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);

                if (result.Succeeded)
                {
                    return View("Success");
                }
            }

            return View("Error");
        }

        // GET: Account/ForgotPassword
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        // POST: Account/ForgotPassword
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordIDTO forgotPasswordIModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forgotPasswordIModel.Email);

                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var resetUrl = Url.Action("ResetPassword", "Home",
                        new
                        {
                            token = token,
                            email = forgotPasswordIModel.Email
                        }, Request.Scheme);

                    System.IO.File.WriteAllText("resetLink.txt", resetUrl);
                }
                else
                {
                    ModelState.AddModelError("", "E-mail inválido!");
                }
            }

            return View();
        }

        // GET: Account/ResetPassword
        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var resetPassword = new ResetPasswordIDTO();
            resetPassword.Token = token;
            resetPassword.Email = email;

            return View(resetPassword);
        }

        // POST: Account/ResetPassword
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordIDTO resetPasswordIModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(resetPasswordIModel.Email);

                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, resetPasswordIModel.Token, resetPasswordIModel.Password);

                    if (!result.Succeeded)
                    {
                        foreach (var erro in result.Errors)
                        {
                            ModelState.AddModelError("", erro.Description);
                        }

                        return View();
                    }

                    return View("Success");
                }

                ModelState.AddModelError("", "Invalid Request");
            }

            return View();
        }
    }
}