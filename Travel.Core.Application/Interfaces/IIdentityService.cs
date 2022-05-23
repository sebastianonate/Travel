using System.Threading.Tasks;
using AspNetCoreHero.Results;
using Travel.Core.Application.DTOs.Identity;

namespace Travel.Core.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<Result<TokenResponse>> GetTokenAsync(TokenRequest request, string ipAddress);

        Task<Result<string>> RegisterAsync(RegisterRequest request, string origin);

        Task<Result<string>> ConfirmEmailAsync(string userId, string code);

        Task ForgotPassword(ForgotPasswordRequest model, string origin);

        Task<Result<string>> ResetPassword(ResetPasswordRequest model);
    }
}