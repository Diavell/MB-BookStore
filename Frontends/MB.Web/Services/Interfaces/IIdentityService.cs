using IdentityModel.Client;
using MB.Shared.Dtos;
using MB.Web.Models;

namespace MB.Web.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<bool>> SignIn(SigninInput signinInput);
        Task<TokenResponse> GetAccessTokenByRefreshToken();
        Task RevokeRefreshToken();
        Task<Response<bool>> Register(RegisterInput registerInput);
    }
}
