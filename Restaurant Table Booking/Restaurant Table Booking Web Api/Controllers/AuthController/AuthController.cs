using ApplicationLayer.DTOs.AuthDto.RequestDtos;
using ApplicationLayer.Services.IAuthService;
using ApplicationLayer.Services.IMailSendService;
using Microsoft.AspNetCore.Mvc;
using static Shared.Constants.UserConstants;

namespace Restaurant_Table_Booking_Web_Api.Controllers.AuthController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMailSender _mailSender;
        public AuthController(IAuthRepository authRepository, IMailSender mailSender)
        {
            _authRepository = authRepository;
            _mailSender = mailSender;
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login( LoginRequestDto loginRequest)
        {
            
            var user = await _authRepository.LoginAsync(loginRequest.Username, loginRequest.Password);
            if (user == null)
            {
                return BadRequest(UnauthourizedMessage);
            }
           return Ok(user);
          
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken( RefreshTokenRequestDto refreshTokenRequest)
        {
          
            var loginResult = await _authRepository.RefreshTokenResponse(refreshTokenRequest.AccessToken, refreshTokenRequest.RefreshToken);

            if (loginResult.IsLoggedIn)
            {
                return Ok(loginResult);
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout( LogoutRequestDto logoutRequest)
        {
            var logOut = await _authRepository.LogoutAsync(logoutRequest.AccessToken);

            if (logOut.IsSucceeded == false)
            {
                return BadRequest(logOut);
            }

            return Ok(logOut);
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequestDto changePasswordRequest)
        {

            var result = await _authRepository.ChangePasswordAsync(changePasswordRequest.Username, changePasswordRequest.Password, changePasswordRequest.NewPassword);
           
            if(result.IsSucceeded == false)
            {
                return BadRequest(result);  
            }

            _mailSender.SendMailToUser(changePasswordRequest.Username);
            return Ok(result);  
        }
    }
}
