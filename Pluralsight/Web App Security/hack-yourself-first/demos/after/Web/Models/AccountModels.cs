using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Web.Models
{
  public class UserProfile
  {
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }
    public string Email { get; set; }

    [StringLength(100)]
    [Display(Name = "First name")]
    public string FirstName { get; set; }

    [StringLength(100)]
    [Display(Name = "Last name")]
    public string LastName { get; set; }

    public bool? IsAdmin { get; set; }

    // This attribute only exists to hack around the Simple Membership providers hash-only password
    // storage implementation. Never, ever, ever do this!!!
    [StringLength(200)]
    [AllowHtml]
    public string Password { get; set; }
  }

  public class LocalPasswordModel
  {
    [Required]
    [Display(Name = "Current password")]
    [DataType(DataType.Password)]
    [AllowHtml]
    public string CurrentPassword { get; set; }

    [Required]
    [StringLength(200, ErrorMessage = "The password cannot be longer than 200 characters.")]
    [DataType(DataType.Password)]
    [Display(Name = "New password")]
    [AllowHtml]
    public string NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm new password")]
    [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
    [AllowHtml]
    public string ConfirmPassword { get; set; }
  }

  public class LoginModel
  {
    [Required]
    [Display(Name = "Email")]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    [AllowHtml]
    public string Password { get; set; }

    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; }
  }

  public class PasswordResetModel
  {
    [Required]
    [Display(Name = "Email")]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Invalid email address")]
    public string Email { get; set; }
  }

  public class PasswordResetVerificationModel
  {
    public string Token { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "New password")]
    [AllowHtml]
    [StringLength(200, ErrorMessage = "The password cannot be longer than 200 characters.")]
    public string NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm new password")]
    [System.Web.Mvc.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
    [StringLength(200, ErrorMessage = "The password cannot be longer than 200 characters.")]
    [AllowHtml]
    public string ConfirmPassword { get; set; }
  }

  public class RegisterModel
  {
    [Required]
    [Display(Name = "Email")]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required]
    [StringLength(100)]
    [RegularExpression(@"^([a-zA-Z]+)$", ErrorMessage = "Invalid first name")]
    [Display(Name = "First name")]
    public string FirstName { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Last name")]
    [RegularExpression(@"^([a-zA-Z]+)$", ErrorMessage = "Invalid last name")]
    public string LastName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    [AllowHtml]
    [StringLength(200, ErrorMessage = "The password cannot be longer than 200 characters.")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    [AllowHtml]
    [StringLength(200, ErrorMessage = "The password cannot be longer than 200 characters.")]
    public string ConfirmPassword { get; set; }
  }
}
