using System.ComponentModel.DataAnnotations;
using static Shared.Enums.BookingTimeEnum;
using static Shared.Enums.OccassionEnum;
using static Shared.Enums.PaymentModeEnum;
using static Shared.Enums.StatusEnum;

namespace ApplicationLayer.DTOs.TableBookingDtos.RequestDto
{
    public class AddBookingRequestDto
    {
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
        public PaymentMode PaymentMode { get; set; }
        public string? CouponCode { get; set; }
        public Status Status { get; set; } = Status.Booked;
    }
}
