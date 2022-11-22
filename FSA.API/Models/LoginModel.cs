using System.ComponentModel.DataAnnotations;

namespace FSA.API.Models
{
    public class LoginModel
    {
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
