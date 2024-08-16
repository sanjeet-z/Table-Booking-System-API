using ApplicationLayer.DTOs.AuthDto.ResponseDtos;
using ApplicationLayer.Services.IAuthService;
using Azure;
using DomainLayer.Entities.IdentityDbUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static Shared.Constants.UserConstants;

namespace InfrastructureLayer.AuthService
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthRepository(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public string CreateJWTToken(ApplicationUser user, List<string> roles)
        {
            List<Claim> claims = new()
            {
                   new (ClaimTypes.Email, user.Email ?? ""),
                   new (ClaimTypes.Name, user.UserName ?? "")
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? ""));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(10),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            byte[] randonNumber = new byte[64];
            using (var numberGenerator = RandomNumberGenerator.Create())
            {
                numberGenerator.GetBytes(randonNumber);
            }
            return Convert.ToBase64String(randonNumber);
        }

        public async Task<LoginResponseDto> LoginAsync(string username, string password)
        {
            LoginResponseDto response = new();
            ApplicationUser? user = await _userManager.FindByEmailAsync(username);

            if (user != null)
            {
                bool checkPasswordResult = await _userManager.CheckPasswordAsync(user, password);
                if (checkPasswordResult)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        string jwtToken = CreateJWTToken(user, roles.ToList());

                        response.AccessToken = jwtToken;
                        response.IsLoggedIn = true;
                        response.RefreshToken = GenerateRefreshToken();
                        response.Role = roles.ToList();

                        user.RefreshToken = response.RefreshToken;
                        user.RefreshTokenExpiry = DateTime.Now.AddHours(12);
                        await _userManager.UpdateAsync(user);

                        return response;
                    }
                }
            }
            response.IsLoggedIn = false;

            return response;
        }

        public async Task<LoginResponseDto> RefreshTokenResponse(string accessToken, string refreshToken)
        {
            var principal = GetTokenPrincipal(accessToken);

            var response = new LoginResponseDto();

            if (principal is null)
            {
                return response;
            }
            ApplicationUser? identityUser = await _userManager.FindByNameAsync(principal.Identity?.Name ?? "");

            if (identityUser is null || identityUser.RefreshToken != refreshToken || identityUser.RefreshTokenExpiry < DateTime.Now)
            {
                return response;
            }
            var roles = await _userManager.GetRolesAsync(identityUser);
            response.IsLoggedIn = true;
            response.AccessToken = CreateJWTToken(identityUser, roles.ToList());
            response.RefreshToken = GenerateRefreshToken();

            identityUser.RefreshToken = response.RefreshToken;
            identityUser.RefreshTokenExpiry = DateTime.Now.AddHours(12);
            await _userManager.UpdateAsync(identityUser);

            return response;
        }

        public ClaimsPrincipal GetTokenPrincipal(string token)
        {
            var Key = Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value ?? "");

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                ClockSkew = TimeSpan.Zero
            }; 

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken? jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        public async Task<ResponseModel> LogoutAsync(string accessToken)
        {
            var response = new ResponseModel();
            var principal = GetTokenPrincipal(accessToken);

            if (principal?.Identity?.Name == null)
            {
                response.IsSucceeded = false;
                response.DescriptionMessage = "AccessToken is not valid!";
                return response;
            }

            var identityUser = await _userManager.FindByNameAsync(principal.Identity.Name);

            if (identityUser == null)
            {
                response.IsSucceeded = false;
                response.DescriptionMessage = "Usernmae not found";
                return response;
            }

            identityUser.RefreshToken = null;
            identityUser.RefreshTokenExpiry = DateTime.Now;
            await _userManager.UpdateAsync(identityUser);

            response.IsSucceeded = true;
            response.DescriptionMessage = LogoutMessage;
            return response;
        }

        public async Task<ResponseModel> ChangePasswordAsync(string userName, string password, string newPassword)
        {
            var response = new ResponseModel();
            ApplicationUser? identityUser = await _userManager.FindByEmailAsync(userName);
            if (identityUser == null)
            {
                response.IsSucceeded = false;
                response.DescriptionMessage = EmailNotFound;
                return response;
            }

            bool user = await _userManager.CheckPasswordAsync(identityUser, password);

            if (!user)
            {
                response.IsSucceeded = false;
                response.DescriptionMessage = InvalidPassword;
                return response;
            }
            PasswordHasher<ApplicationUser> hasher = new();

            string hasPassword = hasher.HashPassword(null, newPassword);


            identityUser.PasswordHash = hasPassword;
            await _userManager.UpdateAsync(identityUser);
            response.IsSucceeded = true;
            response.DescriptionMessage = PasswordChanged;
            return response;
        }

    }
}
