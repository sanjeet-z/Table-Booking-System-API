using ApplicationLayer.IUnitOfWork;
using MediatR;
using Shared.Response;
using static Shared.Constants.UserConstants;

namespace ApplicationLayer.Features.TableBookingFeature.Commands.Delete
{
    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, ResponseModel>
    {
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        public DeleteBookingCommandHandler(IUnitOfWorkRepository unitOfWorkRepository)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
        }
        public async Task<ResponseModel> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseModel();
            var existingBookings = await _unitOfWorkRepository.TableBookingRepository.GetByIdAsync(request.Id);
            if (existingBookings == null)
            {
                response.IsSucceeded = false;
                response.DescriptionMessage = NotFound;
                return response;
            }
           
            await _unitOfWorkRepository.TableBookingRepository.DeleteAsync(existingBookings);
            await _unitOfWorkRepository.SaveAsync();

            response.IsSucceeded = true;
            response.Data = existingBookings;
            response.DescriptionMessage = DataDeletedMessage;

            return response;
        }
    }
}
