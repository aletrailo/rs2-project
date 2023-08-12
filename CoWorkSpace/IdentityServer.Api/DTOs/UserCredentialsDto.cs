using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Api.DTOs
{
    public class UserCredentialsDto
    {
        [Required(ErrorMessage = "Korisnicko ime je obavezno")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lozinka je obavezna")]
        public string Password { get; set; }
    }
}
