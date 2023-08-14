using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Api.Enitities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set;}

        public string LastName { get; set;}

        public string Password { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

    }
}
