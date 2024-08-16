using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.DTOs.AuthDto.RequestDtos
{
    public class ChangePasswordRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
