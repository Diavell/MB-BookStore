using System.ComponentModel.DataAnnotations;

namespace MB.Web.Models
{
    public class RegisterInput
    {
        [Required]
        [Display(Name = "Kullanıcı adınız")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email adresiniz")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Şifreniz")]
        public string Password { get; set; }

        //[Required]
        //[Display(Name = "Şifreniz tekrar")]
        //public string RePassword { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }
    }
}
