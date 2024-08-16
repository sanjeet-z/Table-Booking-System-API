using ApplicationLayer.DTOs.AuthDto.RequestDtos;
using FluentValidation;

namespace ApplicationLayer.Validations
{
    public class QueryFilterValidation : AbstractValidator<QueryRequestDto>
    {
        public QueryFilterValidation()
        {
            RuleFor(x => x.pageNo).GreaterThanOrEqualTo(1).LessThanOrEqualTo(1000).WithMessage("Page size should be greater than 1 and less than 1000");
        }
    }
}
