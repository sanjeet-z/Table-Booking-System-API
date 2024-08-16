using DomainLayer.Entities.TableBookingDb;
using Microsoft.AspNetCore.Identity;

namespace DomainLayer.Entities.IdentityDbUser
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
        public virtual ICollection<TableBookingDetails>? TableBookingDetails { get; set; }

    }
}
