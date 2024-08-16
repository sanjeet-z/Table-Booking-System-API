using DomainLayer.Entities.TableBookingDb;

namespace Restaurant_Table_Booking.Infrastructure.Specification
{
    public class BookingByNameSpec:BaseSpecification<TableBookingDetails>
    {
        public BookingByNameSpec(string customerName) : base(x=>x.CustomerName.Contains(customerName))
        {

        }
    }
}
