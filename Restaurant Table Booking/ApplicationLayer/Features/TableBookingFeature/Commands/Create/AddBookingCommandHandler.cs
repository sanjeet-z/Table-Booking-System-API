using ApplicationLayer.IUnitOfWork;
using AutoMapper;
using DomainLayer.Entities.TableBookingDb;
using MediatR;
using Shared.Response;
using static Shared.Constants.UserConstants;

namespace ApplicationLayer.Features.TableBookingFeature.Commands.Create
{
    public class AddBookingCommandHandler : IRequestHandler<AddBookingCommand, ResponseModel>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        public AddBookingCommandHandler(IMapper mapper, IUnitOfWorkRepository unitOfWorkRepository)
        {

            _mapper = mapper;
            _unitOfWorkRepository = unitOfWorkRepository;
        }
        public async Task<ResponseModel> Handle(AddBookingCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseModel();

            double discount = 0;
            if (!string.IsNullOrEmpty(request.CouponCode))
            {
                if (request.PaymentMode == Shared.Enums.PaymentModeEnum.PaymentMode.Cash)
                {
                    discount = 10;
                }
                else if (request.PaymentMode == Shared.Enums.PaymentModeEnum.PaymentMode.GPay)
                {
                    discount = 5;
                }
                else if (request.PaymentMode == Shared.Enums.PaymentModeEnum.PaymentMode.Credit_Card)
                {
                    discount = 2;
                }
            }

            var tableBookingModel = _mapper.Map<TableBookingDetails>(request);
            tableBookingModel.Discount_In_Percent = discount;

            await _unitOfWorkRepository.TableBookingRepository.CreateAsync(tableBookingModel);
            await _unitOfWorkRepository.SaveAsync();

            response.IsSucceeded = true;
            response.Data = request;
            response.DescriptionMessage = DataAddedMessage;
            return response;
        }
    }
}
