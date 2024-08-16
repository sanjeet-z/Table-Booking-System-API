using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.DTOs.AuthDto.RequestDtos
{
    public class LogoutRequestDto
    {
        [Required]
        public string AccessToken { get; set; }
    }
}
