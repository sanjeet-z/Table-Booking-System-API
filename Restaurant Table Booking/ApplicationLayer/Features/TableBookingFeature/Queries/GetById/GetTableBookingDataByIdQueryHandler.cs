using ApplicationLayer.DTOs.TableBookingDtos.ResponseDto;
using ApplicationLayer.IUnitOfWork;
using AutoMapper;
using MediatR;

namespace ApplicationLayer.Features.TableBookingFeature.Queries.GetById
{
    public class GetTableBookingDataByIdQueryHandler : IRequestHandler<GetTableBookingDataByIdQuery, GetBookingResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;

        public GetTableBookingDataByIdQueryHandler(IMapper mapper, IUnitOfWorkRepository unitOfWorkRepository)
        {
            _mapper = mapper;
            _unitOfWorkRepository = unitOfWorkRepository;
        }
        public async Task<GetBookingResponseDto> Handle(GetTableBookingDataByIdQuery request, CancellationToken cancellationToken)
        {
            var bookingData = await _unitOfWorkRepository.TableBookingRepository.GetByIdAsync(request.Id);
            return _mapper.Map<GetBookingResponseDto>(bookingData);
        }
    }
}
