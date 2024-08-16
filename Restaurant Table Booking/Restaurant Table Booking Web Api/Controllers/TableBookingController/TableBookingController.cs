using ApplicationLayer.DTOs.AuthDto.RequestDtos;
using ApplicationLayer.DTOs.TableBookingDtos.RequestDto;
using ApplicationLayer.Features.TableBookingFeature.Commands.Create;
using ApplicationLayer.Features.TableBookingFeature.Commands.Delete;
using ApplicationLayer.Features.TableBookingFeature.Commands.Update;
using ApplicationLayer.Features.TableBookingFeature.Queries.GetAll;
using ApplicationLayer.Features.TableBookingFeature.Queries.GetById;
using ApplicationLayer.IUnitOfWork;
using ApplicationLayer.Validations;
using AutoMapper;
using DomainLayer.Entities.IdentityDbUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Restaurant_Table_Booking_Web_Api.Controllers.TableBookingController
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableBookingController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AddBookingValidator _validator;

        public TableBookingController(IMediator mediator, IMapper mapper, UserManager<ApplicationUser>userManager, IUnitOfWorkRepository unitOfWorkRepository)
        {
            _mediator = mediator;
            _mapper = mapper;
            _userManager = userManager;
            _validator = new AddBookingValidator(unitOfWorkRepository);
        }


        [HttpPost]
        [Authorize(Roles = "Admin, Accountant")]
        public async Task<IActionResult> Create(AddBookingRequestDto addBookingRequest)
        {
            var result = _validator.Validate(addBookingRequest);
            if (result.IsValid)
            {
                ClaimsPrincipal currentUser = HttpContext.User;
                ApplicationUser? user = await _userManager.FindByNameAsync(currentUser.Identity.Name);
                var UserId = user.Id;

                var validData = _mapper.Map<AddBookingCommand>(addBookingRequest);

                validData.CreatedBy = UserId;
                
                var booking = await _mediator.Send(validData);
                if (booking == null) { return NotFound(booking); }
                return Ok(booking);
            }

            var errorMessage = result.Errors.Select(x => x.ErrorMessage).ToList();
            return BadRequest(errorMessage);
        }



        [HttpGet]
        [Authorize(Roles = "Admin, Accountant")]
        public async Task<IActionResult> GetAll([FromQuery] QueryRequestDto queryRequest)
        {
            var validator = new QueryFilterValidation();
            var result = validator.Validate(queryRequest);

            if (!result.IsValid)
            {
                var error = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(error);
            }
            var bookings = await _mediator.Send(new GetAllBookingQuery()
            { FilterOn = queryRequest.filterOn, FilterQuery = queryRequest.filterQuery, SortBy = queryRequest.sortBy, IsAscending = queryRequest.isAscending ?? true, PageNo = queryRequest.pageNo, PageSize = queryRequest.pageSize });
            return Ok(bookings);
        }



        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Accountant")]
        public async Task<IActionResult> GetById(int id)
        {
            var bookingData = await _mediator.Send(new GetTableBookingDataByIdQuery() { Id = id });

            if (bookingData == null)
            {
                return NotFound();
            }
            return Ok(bookingData);
        }



        [HttpPut]
        [Authorize(Roles = "Admin, Accountant")]
        public async Task<IActionResult> Update(UpdateTableBookingRequestDto updateTableBookingRequest)
        {
            var validator = new UpdateBookingValidator();
            var result = validator.Validate(updateTableBookingRequest);
            if (result.IsValid)
            {
                var validData = _mapper.Map<UpdateTableBookingCommand>(updateTableBookingRequest);
                var booking = await _mediator.Send(validData);
                if (booking == null) { return NotFound(booking); }
                return Ok(booking);
            }
            var errorMessage = result.Errors.Select(x => x.ErrorMessage).ToList();
            return BadRequest(errorMessage);
        }



        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _mediator.Send(new DeleteBookingCommand { Id = id });
            if (data == null)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }
    }
}
