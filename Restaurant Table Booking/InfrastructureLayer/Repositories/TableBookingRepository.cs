using ApplicationLayer.Services.ITableBookingService;
using DomainLayer.Entities.TableBookingDb;
using InfrastructureLayer.Data;
using InfrastructureLayer.GenericRepository;

namespace InfrastructureLayer.Repositories
{
    public class TableBookingRepository : GenericRepository<TableBookingDetails>, ITableBookingRepository
    {
        private readonly AuthDbContext _authDbContext;
        public TableBookingRepository(AuthDbContext authDbContext) : base(authDbContext)
        {
            _authDbContext = authDbContext;
        }
        public bool CheckCouponCode(string couponCode)
        {
            var isCoupon = _authDbContext.TableBookingDetails.Any(c => c.CouponCode == couponCode);
            return isCoupon;
        }
    }
}
