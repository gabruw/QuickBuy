using System;
using System.Threading.Tasks;
using Auxiliary.Email;
using Domain.IDTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace QuickBuy.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AccountIDTO> _accountManager;
        private readonly IUserClaimsPrincipalFactory<AccountIDTO> _userClaimsPrincipalFactory;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<AccountIDTO> accountManager, IUserClaimsPrincipalFactory<AccountIDTO> userClaimsPrincipalFactory, IEmailSender emailSender)
        {
            this._accountManager = accountManager;
            this._userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            this._emailSender = emailSender;
        }

        // GET: Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/SignIn
        [HttpPost]
        public async Task<IActionResult> SignIn(IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                var account = await _accountManager.FindByEmailAsync(collection["email"]);
                var paswordCheck = await _accountManager.CheckPasswordAsync(account, collection["password"]);
                var emailCheck = await _accountManager.IsEmailConfirmedAsync(account);
                var lockCheck = await _accountManager.IsLockedOutAsync(account);

                if (lockCheck == false)
                {
                    if (account != null)
                    {
                        if (paswordCheck == true)
                        {
                            if (emailCheck == true)
                            {
                                await _accountManager.ResetAccessFailedCountAsync(account);

                                var principal = await _userClaimsPrincipalFactory.CreateAsync(account);

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

                    await _accountManager.AccessFailedAsync(account);

                    // Send Email for Recovery Password
                    try
                    {
                        var emailMessage = new EmailMessage();
                        await emailMessage.RecoveryPasswordAsync(account.Email, _emailSender);

                        return RedirectToAction("SuccessToSendEmail");
                    }
                    catch (Exception ex)
                    {
                        return RedirectToAction("FailToSendEmail", ex);
                    }   
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
                var user = await _accountManager.FindByNameAsync(registerIModel.Email);

                if (user == null)
                {
                    user = new AccountIDTO()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = registerIModel.Email
                    };

                    var result = await _accountManager.CreateAsync(user, registerIModel.Password);

                    if (result.Succeeded)
                    {
                        var token = await _accountManager.GenerateEmailConfirmationTokenAsync(user);

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
            var user = await _accountManager.FindByEmailAsync(email);

            if (user != null)
            {
                var result = await _accountManager.ConfirmEmailAsync(user, token);

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
                var user = await _accountManager.FindByEmailAsync(forgotPasswordIModel.Email);

                if (user != null)
                {
                    var token = await _accountManager.GeneratePasswordResetTokenAsync(user);

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
                var user = await _accountManager.FindByEmailAsync(resetPasswordIModel.Email);

                if (user != null)
                {
                    var result = await _accountManager.ResetPasswordAsync(user, resetPasswordIModel.Token, resetPasswordIModel.Password);

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