namespace ApplicationLayer.DTOs.AuthDto.ResponseDtos
{
    public class LoginResponseDto
    {
        public bool IsLoggedIn { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public List<string> Role { get; set; }
    }
}
