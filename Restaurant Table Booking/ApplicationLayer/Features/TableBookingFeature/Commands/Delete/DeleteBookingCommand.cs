using MediatR;
using Shared.Response;

namespace ApplicationLayer.Features.TableBookingFeature.Commands.Delete
{
    public class DeleteBookingCommand:IRequest<ResponseModel>
    {
        public int Id { get; set; }
    }
}
