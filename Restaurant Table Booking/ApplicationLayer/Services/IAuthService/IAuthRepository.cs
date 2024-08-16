using ApplicationLayer.DTOs.AuthDto.ResponseDtos;
using DomainLayer.Entities.IdentityDbUser;
using Shared.Response;

namespace ApplicationLayer.Services.IAuthService
{
    public interface IAuthRepository
    {
        string CreateJWTToken(ApplicationUser user, List<string> roles);
        string GenerateRefreshToken();
        public Task<LoginResponseDto> LoginAsync(string username, string password);
        public Task<ResponseModel> LogoutAsync(string accessToken);
        public Task<LoginResponseDto> RefreshTokenResponse(string accessToken, string refreshToken);
        public Task<ResponseModel> ChangePasswordAsync(string userName, string password, string newPassword);
      
    }
}
