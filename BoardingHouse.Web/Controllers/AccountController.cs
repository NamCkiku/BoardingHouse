﻿using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using BoardingHouse.Web.Models;
using Newtonsoft.Json.Linq;

namespace BoardingHouse.Web.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> Login(LoginViewModel model, string returnUrl)
        {
            JsonResult jsonResult = new JsonResult();
            HttpRequestBase request = this.HttpContext.Request;
            if (ValidateRequestHeader(request))
            {
                var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        return jsonResult = Json(new { success = true }, JsonRequestBehavior.AllowGet);
                    case SignInStatus.LockedOut:
                        return jsonResult = Json(new { success = false, message = "Tài khoản của bạn đang bị khóa" }, JsonRequestBehavior.AllowGet);
                    case SignInStatus.RequiresVerification:
                        return jsonResult = Json(new { success = false, message = "Tài khoản của bạn đang bị khóa" }, JsonRequestBehavior.AllowGet);
                    case SignInStatus.Failure:
                    default:
                        return jsonResult = Json(new { success = false, message = "Tài khoản hoặc mật khẩu không chính xác" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                jsonResult = Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            return jsonResult;
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> Register(RegisterViewModel model)
        {
            JsonResult jsonResult = new JsonResult();
            HttpRequestBase request = this.HttpContext.Request;
            if (ValidateRequestHeader(request))
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        FullName = model.FullName,
                        Avatar = model.Avatar
                    };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                        jsonResult = Json(new { success = true }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonResult = Json(new { success = false, message = "Đăng ký tài khoản không thành công" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    jsonResult = Json(new { success = false, message = "Đăng ký tài khoản không thành công" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                jsonResult = Json(new { success = false, message = "Đăng ký tài khoản không thành công" }, JsonRequestBehavior.AllowGet);
            }
            // If we got this far, something failed, redisplay form
            return jsonResult;
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> ResetPassword(ResetPasswordViewModel model)
        {
            JsonResult jsonResult = new JsonResult();
            HttpRequestBase request = this.HttpContext.Request;
            if (ValidateRequestHeader(request))
            {
                if (!ModelState.IsValid)
                {
                    jsonResult = Json(new { success = false, message = "Không thành công" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var user = await UserManager.FindByNameAsync(model.Email);
                    string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                    if (user == null)
                    {
                        jsonResult = Json(new { success = false, message = "Tài khoản của bạn không tồn tại" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var result = await UserManager.ResetPasswordAsync(user.Id, code, model.Password);
                        if (result.Succeeded)
                        {
                            jsonResult = Json(new { success = true, message = "Không thành công" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }

            return jsonResult;
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account"));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }
            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }
        //[AllowAnonymous]
        //public async Task<JsonResult> ExternalLoginCallback(string provider, string returnUrl)
        //{
        //    JsonResult jsonResult = new JsonResult();
        //    HttpRequestBase request = this.HttpContext.Request;
        //    if (ValidateRequestHeader(request))
        //    {
        //        var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
        //        if (loginInfo == null)
        //        {
        //            return jsonResult = Json(new { success = false }, JsonRequestBehavior.AllowGet);
        //        }
        //        var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
        //        switch (result)
        //        {

        //            case SignInStatus.Success:
        //                return jsonResult = Json(new { success = true }, JsonRequestBehavior.AllowGet);
        //            case SignInStatus.LockedOut:
        //                return jsonResult = Json(new { success = false, message = "Tài khoản của bạn đang bị khóa" }, JsonRequestBehavior.AllowGet); ;
        //            case SignInStatus.RequiresVerification:
        //                return jsonResult = Json(new { success = false, message = "Tài khoản của bạn đang bị khóa" }, JsonRequestBehavior.AllowGet);
        //            case SignInStatus.Failure:
        //            default:
        //                ViewBag.ReturnUrl = returnUrl;
        //                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
        //                return jsonResult = Json(new { success = true }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    return jsonResult;
        //}
        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser();
                if (info.Login.LoginProvider == "Google")
                {
                    var externalIdentity = AuthenticationManager.GetExternalIdentityAsync(DefaultAuthenticationTypes.ExternalCookie);
                    var emailClaim = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                    var lastNameClaim = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname);
                    var givenNameClaim = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName);
                    var givenImageClaim = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type.Equals("image"));
                    //var email = emailClaim.Value;
                    //var firstName = givenNameClaim.Value;
                    //var lastname = lastNameClaim.Value;
                    var obj = JObject.Parse(givenImageClaim.Value);
                    var image = obj.Value<string>("url");
                    user.Email = emailClaim.Value;
                    user.FullName = givenNameClaim.Value + lastNameClaim.Value;
                    user.Avatar = image;
                    user.UserName = emailClaim.Value;
                }
                //var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult LogOff()
        {
            JsonResult jsonResult = new JsonResult();
            HttpRequestBase request = this.HttpContext.Request;
            if (ValidateRequestHeader(request))
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                jsonResult = Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                jsonResult = Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            return jsonResult;
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}