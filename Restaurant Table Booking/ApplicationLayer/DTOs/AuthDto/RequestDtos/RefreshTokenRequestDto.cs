using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.DTOs.AuthDto.RequestDtos
{
    public class RefreshTokenRequestDto
    {
        [Required]
        public string AccessToken { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
