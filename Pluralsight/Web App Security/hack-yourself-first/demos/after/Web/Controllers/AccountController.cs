using System;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using Web.Filters;
using Web.Models;

namespace Web.Controllers
{
  [Authorize]
  [InitialiseDatabase]
  public class AccountController : Controller
  {
    private SupercarModelContext db = new SupercarModelContext();

    //
    // GET: /Account/Login
    [AllowAnonymous]
    [RequireHttps]
    public ActionResult Login(string returnUrl)
    {
      ViewBag.ReturnUrl = returnUrl;
      return View();
    }

    //
    // POST: /Account/Login
    [HttpPost]
    [AllowAnonymous]
    public ActionResult Login(LoginModel model, string returnUrl)
    {
      if (WebSecurity.IsAccountLockedOut(model.Email, 5, 60))
      {
        return View("AccountLockout");
      }

      if (ModelState.IsValid && WebSecurity.Login(model.Email, model.Password, model.RememberMe))
      {
        return RedirectToLocal(returnUrl);
      }

      ModelState.AddModelError("", "The email or password provided is incorrect.");
      return View(model);
    }

    //
    // POST: /Account/LogOff
    [HttpPost]
    public ActionResult LogOff()
    {
      WebSecurity.Logout();
      return Redirect(Url.Action("Index", "Home", null, "http"));
    }

    //
    // GET: /Account/Register
    [AllowAnonymous]
    [RequireHttps]
    public ActionResult Register()
    {
      return View();
    }

    //
    // POST: /Account/Register
    [HttpPost]
    [AllowAnonymous]
    [ValidateInput(false)]
    [RequireHttps]
    public ActionResult Register(RegisterModel model)
    {
      if (ModelState.IsValid)
      {
        // Attempt to register the user
        try
        {
          WebSecurity.CreateUserAndAccount(model.Email, model.Password);
          WebSecurity.Login(model.Email, model.Password);

          var userProfile = db.UserProfiles.Single(u => u.Email == model.Email);
          userProfile.FirstName = model.FirstName;
          userProfile.LastName = model.LastName;
          userProfile.Password = model.Password;
          db.SaveChanges();

          var message = new MailMessage
            {
              Subject = "Your new account has been created",
              Body = string.Format("Welcome {0}!<br /><br />" +
                                   "Your new Supercar Showdown account has been created. Don't forget your password!",
                                   model.FirstName),
              IsBodyHtml = true
            };
          message.To.Add(new MailAddress(model.Email));
          var smtpClient = new SmtpClient();
          smtpClient.Send(message);

          return RedirectToLocal("");
        }
        catch (MembershipCreateUserException e)
        {
          ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
        }
      }

      return View(model);
    }

    //
    // GET: /Account/UserProfile
    public ActionResult UserProfile(ManageMessageId? message)
    {
      ViewBag.StatusMessage = message == ManageMessageId.ChangeProfileSuccess ? "Your profile has been updated." : "";
      var profile = db.UserProfiles.SingleOrDefault(u => u.Email == User.Identity.Name);
      return View(profile);
    }

    //
    // POST: /Account/UserProfile
    [HttpPost]
    public ActionResult UserProfile(UserProfile model)
    {
      if (ModelState.IsValid)
      {
        var profile = db.UserProfiles.SingleOrDefault(u => u.Email == User.Identity.Name);
        var includeInModel = new[] { "FirstName", "LastName" };
        if (TryUpdateModel(profile, includeInModel))
        {
          db.SaveChanges();
          return RedirectToAction("UserProfile", new { Message = ManageMessageId.ChangeProfileSuccess });
        }
      }

      return View(model);
    }

    //
    // GET: /Account/ChangePassword
    public ActionResult ChangePassword(ManageMessageId? message)
    {
      ViewBag.StatusMessage =
          message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
          : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
          : "";
      ViewBag.ReturnUrl = Url.Action("ChangePassword");

      return View();
    }

    //
    // POST: /Account/ChangePassword
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult ChangePassword(LocalPasswordModel model)
    {
      ViewBag.ReturnUrl = Url.Action("ChangePassword");

      if (ModelState.IsValid)
      {
        // ChangePassword will throw an exception rather than return false in certain failure scenarios.
        bool changePasswordSucceeded;
        try
        {
          changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.CurrentPassword, model.NewPassword);

          var userProfile = db.UserProfiles.SingleOrDefault(u => u.Email == User.Identity.Name);
          userProfile.Password = model.NewPassword;
          db.SaveChanges();
        }
        catch (Exception)
        {
          changePasswordSucceeded = false;
        }

        if (changePasswordSucceeded)
        {
          return RedirectToAction("ChangePassword", new { Message = ManageMessageId.ChangePasswordSuccess });
        }
        else
        {
          ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
        }
      }

      return View(model);
    }

    //
    // GET: /Account/ResetPassword
    [AllowAnonymous]
    public ActionResult ResetPassword()
    {
      return View();
    }

    //
    // GET: /Account/ResetPassword
    [HttpPost]
    [AllowAnonymous]
    public ActionResult ResetPassword(PasswordResetModel model)
    {
      var userProfile = db.UserProfiles.SingleOrDefault(u => u.Email == model.Email);
      var smtpClient = new SmtpClient();
      MailMessage message;

      if (userProfile != null)
      {
        var token = WebSecurity.GeneratePasswordResetToken(model.Email);
        //WebSecurity.ResetPassword(token, newPassword);

        //userProfile.Password = newPassword;
        //db.SaveChanges();

        message = new MailMessage
        {
          Subject = "Password reset",
          Body = string.Format("Dear {0}!<br /><br />" +
                               "You (or someone else) entered this email address to reset the password of your account.<br /><br />" +
                               "To continue the password reset process, please proceed to <a href=\"http://hackyourselffirst.troyhunt.com/Account/VerifyResetToken/?token={1}\">this link</a>.<br /><br />" +
                               "This action was performed from IP address 202.147.129.58. Find out more about this address <a href=\"http://www.ip-adress.com/ip_tracer/202.147.129.58\">here</a>.",
                               userProfile.FirstName, token),
          IsBodyHtml = true
        };
        message.To.Add(new MailAddress(userProfile.Email));
      }
      else
      {
        message = new MailMessage
        {
          Subject = "You do not have an account",
          Body = string.Format("You (or someone else) entered this email address to reset the password of your account.<br /><br />" +
                               "However, this email address does not have an account on the site.<br /><br />" +
                               "This action was performed from IP address 202.147.129.58. Find out more about this address <a href=\"http://www.ip-adress.com/ip_tracer/202.147.129.58\">here</a>."),
          IsBodyHtml = true
        };
        message.To.Add(new MailAddress(model.Email));
      }

      smtpClient.Send(message);

      return RedirectToAction("ResetPasswordComplete");
    }

    //
    // GET: /Account/VerifyResetToken
    [AllowAnonymous]
    public ActionResult VerifyResetToken(string token)
    {
      return View(new PasswordResetVerificationModel { Token = token });
    }

    //
    // GET: /Account/VerifyResetToken
    [AllowAnonymous]
    [HttpPost]
    public ActionResult VerifyResetToken(PasswordResetVerificationModel model)
    {
      var userId = WebSecurity.GetUserIdFromPasswordResetToken(model.Token);

      if (ModelState.IsValid)
      {
        // ChangePassword will throw an exception rather than return false in certain failure scenarios.
        bool changePasswordSucceeded;
        try
        {
          changePasswordSucceeded = WebSecurity.ResetPassword(model.Token, model.NewPassword);
        }
        catch (Exception)
        {
          changePasswordSucceeded = false;
        }

        if (changePasswordSucceeded)
        {
          var userProfile = db.UserProfiles.SingleOrDefault(u => u.UserId == userId);
          userProfile.Password = model.NewPassword;
          db.SaveChanges();

          return View("ResetComplete");
        }
        else
        {
          return View("InvalidToken");
        }
      }

      return View();
    }

    public static string CreateResetToken()
    {
      var rng = new RNGCryptoServiceProvider();
      var buff = new byte[32];
      rng.GetBytes(buff);
      var salt = Convert.ToBase64String(buff);

      var hasher = SHA256.Create();
      var data = hasher.ComputeHash(Encoding.Default.GetBytes(salt));
      var builder = new StringBuilder();

      for (var i = 0; i < data.Length; i++)
      {
        builder.Append(data[i].ToString("x2"));
      }

      return builder.ToString();
    }

    //
    // GET: /Account/ResetPasswordComplete
    [AllowAnonymous]
    public ActionResult ResetPasswordComplete()
    {
      return View();
    }

    #region Helpers

    private ActionResult RedirectToLocal(string returnUrl)
    {
      if (Url.IsLocalUrl(returnUrl))
      {
        return Redirect(string.Format("https://{0}{1}", Request.Url.Host, returnUrl));
      }
      else
      {
        return Redirect(Url.Action("Index", "Home", null, "https"));
      }
    }

    public enum ManageMessageId
    {
      ChangePasswordSuccess,
      SetPasswordSuccess,
      RemoveLoginSuccess,
      ChangeProfileSuccess
    }

    private static string ErrorCodeToString(MembershipCreateStatus createStatus)
    {
      switch (createStatus)
      {
        case MembershipCreateStatus.DuplicateUserName:
          return "Email already exists. Please enter a different address.";

        case MembershipCreateStatus.DuplicateEmail:
          return "That email address already exists. Please enter a different email address.";

        case MembershipCreateStatus.InvalidPassword:
          return "The password provided is invalid. Please enter a valid password value.";

        case MembershipCreateStatus.InvalidEmail:
          return "The email address provided is invalid. Please check the value and try again.";

        case MembershipCreateStatus.InvalidAnswer:
          return "The password retrieval answer provided is invalid. Please check the value and try again.";

        case MembershipCreateStatus.InvalidQuestion:
          return "The password retrieval question provided is invalid. Please check the value and try again.";

        case MembershipCreateStatus.InvalidUserName:
          return "The email provided is invalid. Please check the value and try again.";

        case MembershipCreateStatus.ProviderError:
          return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

        case MembershipCreateStatus.UserRejected:
          return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

        default:
          return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
      }
    }

    #endregion
  }
}
