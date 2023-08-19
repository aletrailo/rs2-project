using IdentityServer.Api.DTOs;
using IdentityServer.Api.Enitities;

namespace IdentityServer.Api.Services
{
    public interface IAuthenticationService
    {
        Task<User> ValidateUser(UserCredentialsDto userCredentials);
        Task<AuthenticationModel> CreateAuthenticationModel(User user);
        Task RemoveRefreshToken(User user, string refreshToken);
    }
}
