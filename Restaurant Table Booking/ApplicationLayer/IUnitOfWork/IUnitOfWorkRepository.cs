using ApplicationLayer.Services.ITableBookingService;

namespace ApplicationLayer.IUnitOfWork
{
    public interface IUnitOfWorkRepository : IDisposable
    {
        ITableBookingRepository TableBookingRepository { get; }

        Task SaveAsync();
    }
}
