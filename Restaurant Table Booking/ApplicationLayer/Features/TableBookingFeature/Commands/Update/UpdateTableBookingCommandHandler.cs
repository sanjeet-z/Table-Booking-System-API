using ApplicationLayer.IUnitOfWork;
using AutoMapper;
using MediatR;
using Shared.Response;
using static Shared.Constants.UserConstants;

namespace ApplicationLayer.Features.TableBookingFeature.Commands.Update
{
    public class UpdateTableBookingCommandHandler : IRequestHandler<UpdateTableBookingCommand, ResponseModel>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;

        public UpdateTableBookingCommandHandler(IMapper mapper, IUnitOfWorkRepository unitOfWorkRepository)
        {
            _mapper = mapper;
            _unitOfWorkRepository = unitOfWorkRepository;
        }
        public async Task<ResponseModel> Handle(UpdateTableBookingCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseModel();
            var existingBooking = await _unitOfWorkRepository.TableBookingRepository.GetByIdAsync(request.Id);
            if (existingBooking == null)
            {
                response.IsSucceeded = false;
                response.DescriptionMessage = NotFound;
                return response;
            }
           
            _mapper.Map(request, existingBooking);

            await _unitOfWorkRepository.TableBookingRepository.UpdateAsync(existingBooking);
            await _unitOfWorkRepository.SaveAsync();

            response.IsSucceeded = true;
            response.Data = request;
            response.DescriptionMessage = DataUpdatedMessage;

            return response;
        }
    }
}
