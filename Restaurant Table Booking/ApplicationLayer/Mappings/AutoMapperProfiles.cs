using ApplicationLayer.DTOs.AuthDto.RequestDtos;
using ApplicationLayer.DTOs.TableBookingDtos.RequestDto;
using ApplicationLayer.DTOs.TableBookingDtos.ResponseDto;
using ApplicationLayer.Features.TableBookingFeature.Commands.Create;
using ApplicationLayer.Features.TableBookingFeature.Commands.Update;
using ApplicationLayer.Features.TableBookingFeature.Queries.GetAll;
using AutoMapper;
using DomainLayer.Entities.TableBookingDb;

namespace ApplicationLayer.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
          
            CreateMap<AddBookingRequestDto, AddBookingCommand>().ReverseMap();
            CreateMap<AddBookingCommand, TableBookingDetails>();
            CreateMap<GetAllBookingQuery, QueryRequestDto>().ReverseMap();
            CreateMap<GetBookingResponseDto, TableBookingDetails>().ReverseMap();
            CreateMap<UpdateTableBookingCommand, UpdateTableBookingRequestDto>().ReverseMap();
            CreateMap<UpdateTableBookingCommand, TableBookingDetails>().ReverseMap();
        }
    }
}
