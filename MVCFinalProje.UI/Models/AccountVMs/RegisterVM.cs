using System.ComponentModel.DataAnnotations;

namespace MVCFinalProje.UI.Models.AccountVMs
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Please enter your first name.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter your last name.")]
        public string LastName { get; set; }              

        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Please choose a username.")]
        //public string Username { get; set; }

        //[Required(ErrorMessage = "Please enter your password.")]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }

        
        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Passwords do not match.")]
        //public string? ConfirmPassword { get; set; }

        //public bool? AcceptTerms { get; set; }
    }
}
