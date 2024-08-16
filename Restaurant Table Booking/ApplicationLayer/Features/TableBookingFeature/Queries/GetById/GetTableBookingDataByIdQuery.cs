using ApplicationLayer.DTOs.TableBookingDtos.ResponseDto;
using MediatR;

namespace ApplicationLayer.Features.TableBookingFeature.Queries.GetById
{
    public class GetTableBookingDataByIdQuery:IRequest<GetBookingResponseDto>
    {
        public int Id { get; set; }
    }
}
