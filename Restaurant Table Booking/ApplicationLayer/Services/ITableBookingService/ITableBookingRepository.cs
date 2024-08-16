using ApplicationLayer.IGeneric;
using DomainLayer.Entities.TableBookingDb;

namespace ApplicationLayer.Services.ITableBookingService
{
    public interface ITableBookingRepository : IGenericRepository<TableBookingDetails>
    {
        bool CheckCouponCode(string couponCode);
    }
}
