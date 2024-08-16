using ApplicationLayer.IUnitOfWork;
using ApplicationLayer.Services.ITableBookingService;
using InfrastructureLayer.Data;
using InfrastructureLayer.Repositories;

namespace InfrastructureLayer.UnitOfWork
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private readonly AuthDbContext _authDbContext;

        public UnitOfWorkRepository(AuthDbContext authDbContext)
        {
            _authDbContext = authDbContext;
            TableBookingRepository = new TableBookingRepository(authDbContext);
        }
        public ITableBookingRepository TableBookingRepository { get; private set; }


        public void Dispose()
        {
            _authDbContext.Dispose();
        }

        public async Task SaveAsync()
        {
            await _authDbContext.SaveChangesAsync();
        }
    }
}
