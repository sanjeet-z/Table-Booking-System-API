using System.ComponentModel.DataAnnotations;
using static Shared.Enums.BookingTimeEnum;
using static Shared.Enums.OccassionEnum;

namespace ApplicationLayer.DTOs.TableBookingDtos.RequestDto
{
    public class UpdateTableBookingRequestDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime BookingDate { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public int NoOfMembers { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string MobileNo { get; set; }
        public OccassionType Occassion { get; set; }
        public BookingTime BookingTime { get; set; }
    }
}
