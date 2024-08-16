using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DomainLayer.Entities.IdentityDbUser;
using static Shared.Enums.BookingTimeEnum;
using static Shared.Enums.OccassionEnum;
using static Shared.Enums.PaymentModeEnum;
using static Shared.Enums.StatusEnum;

namespace DomainLayer.Entities.TableBookingDb
{
    public class TableBookingDetails
    {
        [Key]
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public string CustomerName { get; set; }
        public int NoOfMembers { get; set; }
        public string Email { get; set; }
        public long MobileNo { get; set; }
        [Column(TypeName = "nvarchar(24)")]
        public OccassionType Occassion { get; set; }
        [Column(TypeName = "nvarchar(24)")]
        public BookingTime BookingTime { get; set; }
        [Column(TypeName = "nvarchar(24)")]
        public PaymentMode PaymentMode { get; set; }
        public string? CouponCode { get; set; }
        public double? Discount_In_Percent { get; set; }
        [Column(TypeName = "nvarchar(24)")]
        public Status Status { get; set; } = Status.Booked;
        public int NoOfTables { get; set; }
        [ForeignKey("ApplicationUser")]
        public string? CreatedBy { get; set; }
        //Navigation Property
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
