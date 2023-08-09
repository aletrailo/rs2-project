using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Api.Enitities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set;}
        public string LastNamev { get; set; }



    }
}
