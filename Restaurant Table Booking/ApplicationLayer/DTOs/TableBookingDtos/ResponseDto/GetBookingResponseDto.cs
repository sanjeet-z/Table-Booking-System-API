using static Shared.Enums.BookingTimeEnum;
using static Shared.Enums.OccassionEnum;
using static Shared.Enums.PaymentModeEnum;
using static Shared.Enums.StatusEnum;

namespace ApplicationLayer.DTOs.TableBookingDtos.ResponseDto
{
    public class GetBookingResponseDto
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public string CustomerName { get; set; }
        public int NoOfMembers { get; set; }
        public string Email { get; set; }
        public long MobileNo { get; set; }
        public OccassionType Occassion { get; set; }
        public BookingTime BookingTime { get; set; }
        public PaymentMode PaymentMode { get; set; }
        public string? CouponCode { get; set; }
        public double? Discount_In_Percent { get; set; }
        public Status Status { get; set; }
        public int NoOfTables { get; set; }
        public string CreatedBy { get; set; }
    }
}
