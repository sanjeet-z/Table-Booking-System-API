using AutoMapper.Execution;
using MediatR;
using Shared.Response;
using static Shared.Enums.BookingTimeEnum;
using static Shared.Enums.OccassionEnum;

namespace ApplicationLayer.Features.TableBookingFeature.Commands.Update
{
    public class UpdateTableBookingCommand:IRequest<ResponseModel>
    {
        private int _members;
        private int _tables;
        public int Id { get; set; }
        public string BookingDate { get; set; }
        public string CustomerName { get; set; }
        public int NoOfMembers
        {
            get => _members;
            set => _members = value;
        }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public OccassionType Occassion { get; set; }
        public BookingTime BookingTime { get; set; }
        public int NoOfTables
        {
            get
            {
                if (_members < 6) _tables = 1;
                else if (_members % 6 == 0) _tables = _members / 6;
                else _tables = (_members / 6) + 1;
                return _tables;
            }
            set { _tables = value; }
        }
    }
}
