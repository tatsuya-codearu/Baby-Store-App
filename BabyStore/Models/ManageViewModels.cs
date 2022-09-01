using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace BabyStore.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {/// setting error messages if the user inputs do not meet the required standard
        [Required]
        [StringLength(100, ErrorMessage = "The {0} has to be {2} characters long or more.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }
        // setting error messages if the user inputs do not meet the required standard
        [DataType(DataType.Password)]
        [Display(Name = "Confirm the new password")]
        [Compare("NewPassword", ErrorMessage = "Passwords did not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {/// setting error messages if the user inputs do not meet the required standard
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }
        // setting error messages if the user inputs do not meet the required standard
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }
        // setting error messages if the user inputs do not meet the required standard
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        //setting the number
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        
        [Required]
        [Display(Name = "Code")]
        //setting the code
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}