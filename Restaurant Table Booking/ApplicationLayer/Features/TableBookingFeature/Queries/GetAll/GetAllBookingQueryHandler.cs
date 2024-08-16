using ApplicationLayer.DTOs.TableBookingDtos.ResponseDto;
using ApplicationLayer.IUnitOfWork;
using AutoMapper;
using MediatR;
using static Shared.Enums.BookingTimeEnum;
using static Shared.Enums.OccassionEnum;
using static Shared.Enums.PaymentModeEnum;
using static Shared.Enums.StatusEnum;

namespace ApplicationLayer.Features.TableBookingFeature.Queries.GetAll
{
    public class GetAllBookingQueryHandler : IRequestHandler<GetAllBookingQuery, List<GetBookingResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        public GetAllBookingQueryHandler(IMapper mapper, IUnitOfWorkRepository unitOfWorkRepository)
        {
            _mapper = mapper;
            _unitOfWorkRepository = unitOfWorkRepository;
        }
        public async Task<List<GetBookingResponseDto>> Handle(GetAllBookingQuery request, CancellationToken cancellationToken)
        {
            var bookings = _unitOfWorkRepository.TableBookingRepository.GetAllAsync();

            //Searching
            if(string.IsNullOrWhiteSpace(request.FilterQuery) == false)
            {
                var trimmedFilterQuery = request.FilterQuery.Trim();

                // Fetch all data first (be cautious with large data sets)
                var allBookings = bookings.ToList(); // Fetch data from the database

                bookings = allBookings.Where(x =>
                    x.Id.Equals(trimmedFilterQuery) ||
                    x.CustomerName.Contains(trimmedFilterQuery) ||
                    x.MobileNo.Equals(trimmedFilterQuery) ||
                    x.NoOfMembers.ToString().Contains(trimmedFilterQuery) ||
                    x.Email.Contains(trimmedFilterQuery) ||
                  /*  x.BookingDate.ToString("yyyy-MM-dd").Contains(trimmedFilterQuery) ||
                    x.BookingTime.ToString("HH:mm").Contains(trimmedFilterQuery) ||*/
                    x.CouponCode.Contains(trimmedFilterQuery) ||
                    x.CreatedBy.ToString().Contains(trimmedFilterQuery) ||
                    x.Discount_In_Percent.ToString().Contains(trimmedFilterQuery) ||
                    x.Occassion.Equals(trimmedFilterQuery) ||
                    x.PaymentMode.Equals(trimmedFilterQuery) ||
                    x.Status.Equals(trimmedFilterQuery)
                ).AsQueryable();

            }

           /* if (string.IsNullOrWhiteSpace(request.FilterOn) == false && string.IsNullOrWhiteSpace(request.FilterQuery) == false)
            {
                if (request.FilterOn.Equals("BookingDate", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = bookings.Where(x => x.BookingDate.ToString().Contains(request.FilterQuery));
                }
                else if (request.FilterOn.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = bookings.Where(x => x.Id.Equals(request.FilterQuery));
                }
                else if (request.FilterOn.Equals("CustomerName", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = bookings.Where(x => x.CustomerName.Contains(request.FilterQuery));
                }
                else if (request.FilterOn.Equals("NoOfMembers", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = bookings.Where(x => x.NoOfMembers.Equals(request.FilterQuery));
                }
                else if (request.FilterOn.Equals("Email", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = bookings.Where(x => x.Email.Equals(Convert.ToInt64(request.FilterQuery)));
                }
                else if (request.FilterOn.Equals("MobileNo", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = bookings.Where(x => x.MobileNo.Equals(Convert.ToInt64(request.FilterQuery)));
                }
                else if (request.FilterOn.Equals("Occassion", StringComparison.OrdinalIgnoreCase))
                {
                    Enum.TryParse(request.FilterQuery, true, out OccassionType occassionFilter);
                    bookings = bookings.Where(x => x.Occassion == occassionFilter);
                }
                else if (request.FilterOn.Equals("BookingTime", StringComparison.OrdinalIgnoreCase))
                {
                    Enum.TryParse(request.FilterQuery, true, out BookingTime bookingTimeFilter);
                    bookings = bookings.Where(x => x.BookingTime == bookingTimeFilter);
                }
                else if (request.FilterOn.Equals("PaymentMode", StringComparison.OrdinalIgnoreCase))
                {
                    Enum.TryParse(request.FilterQuery, true, out PaymentMode paymentFilter);
                    bookings = bookings.Where(x => x.PaymentMode == paymentFilter);
                }
                else if (request.FilterOn.Equals("Status", StringComparison.OrdinalIgnoreCase))
                {
                    Enum.TryParse(request.FilterQuery, true, out Status statusFilter);
                    bookings = bookings.Where(x => x.Status == statusFilter);
                }
                else if (request.FilterOn.Equals("Discount", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = bookings.Where(x => x.Discount_In_Percent.Equals(request.FilterQuery));
                }
                else if (request.FilterOn.Equals("NoOfTables", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = bookings.Where(x => x.NoOfTables.Equals(request.FilterQuery));
                }
                else if (request.FilterOn.Equals("CreatedBy", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = bookings.Where(x => x.CreatedBy.Equals(request.FilterQuery));
                }

            }*/

            //Sorting

            if (string.IsNullOrWhiteSpace(request.SortBy) == false)
            {
                if (request.SortBy.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = (bool)request.IsAscending ? bookings.OrderBy(x => x.Id) : bookings.OrderByDescending(x => x.Id);
                }
                else if (request.SortBy.Equals("BookingDate", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = (bool)request.IsAscending ? bookings.OrderBy(x => x.BookingDate) : bookings.OrderByDescending(x => x.BookingDate);
                }

                else if (request.SortBy.Equals("CustomerName", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = (bool)request.IsAscending ? bookings.OrderBy(x => x.CustomerName) : bookings.OrderByDescending(x => x.CustomerName);
                }
                else if (request.SortBy.Equals("NoOfMembers", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = (bool)request.IsAscending ? bookings.OrderBy(x => x.NoOfMembers) : bookings.OrderByDescending(x => x.NoOfMembers);
                }
                else if (request.SortBy.Equals("Email", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = (bool)request.IsAscending ? bookings.OrderBy(x => x.Email) : bookings.OrderByDescending(x => x.Email);
                }
                else if (request.SortBy.Equals("MobileNo", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = (bool)request.IsAscending ? bookings.OrderBy(x => x.MobileNo) : bookings.OrderByDescending(x => x.MobileNo);
                }
                else if (request.SortBy.Equals("BookingTime", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = (bool)request.IsAscending ? bookings.OrderBy(x => x.BookingTime) : bookings.OrderByDescending(x => x.BookingTime);
                }
                else if (request.SortBy.Equals("Status", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = (bool)request.IsAscending ? bookings.OrderBy(x => x.Status) : bookings.OrderByDescending(x => x.Status);
                }
                else if (request.SortBy.Equals("No of tables", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = (bool)request.IsAscending ? bookings.OrderBy(x => x.NoOfTables) : bookings.OrderByDescending(x => x.NoOfTables);
                }
                else if (request.SortBy.Equals("Discount", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = (bool)request.IsAscending ? bookings.OrderBy(x => x.Discount_In_Percent) : bookings.OrderByDescending(x => x.Discount_In_Percent);
                }
            }
            else
            {
                bookings = (bool)request.IsAscending ? bookings.OrderBy(x => x.Id) : bookings.OrderByDescending(x => x.Id);
            }

            //Pagination
            var skipResults = (request.PageNo - 1) * request.PageSize;
            var result=  bookings.Skip(skipResults).Take(request.PageSize).ToList();

            return _mapper.Map<List<GetBookingResponseDto>>(result);
        }
    }
}
