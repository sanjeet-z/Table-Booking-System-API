using ApplicationLayer.DTOs.TableBookingDtos.RequestDto;
using FluentValidation;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ApplicationLayer.Validations
{
    public class UpdateBookingValidator : AbstractValidator<UpdateTableBookingRequestDto>
    {
        public UpdateBookingValidator()
        {
            RuleFor(x => x.BookingDate.ToString())
                        .NotNull().NotEmpty()
                        .WithMessage("Booking date is required.")
                        .Must(IsValidDateOfBooking)
                        .WithMessage("Date of appointment should be DD/MM/YYYY, DD-MM-YYYY formate and should not a past date and not more than 1 month from the current date also check is it valid or not.");
            RuleFor(x => x.MobileNo)
                         .NotEmpty()
                         .NotNull()
                         .WithMessage("Mobile no is required.").Must(IsValidPhone)
                         .WithMessage("Mobile no contains 10 digits");
            RuleFor(x => x.NoOfMembers)
                         .NotEmpty()
                         .NotNull()
                         .WithMessage("No of members is required.")
                         .GreaterThanOrEqualTo(1)
                         .WithMessage("No of members should be at least one");
            RuleFor(x => x.CustomerName)
                         .NotNull().NotEmpty()
                         .WithMessage("Customer name is required")
                         .MinimumLength(3)
                         .MaximumLength(100)
                         .WithMessage("Customer name contains minimum 3 and maximum 100 characters.");
            RuleFor(x => x.Email)
                         .NotNull().NotEmpty()
                         .WithMessage("Email is required")
                         .EmailAddress().WithMessage("Please enter valid email!");
            RuleFor(x => x.Occassion)
                         .NotNull().NotEmpty()
                         .WithMessage("Occassion is required").IsInEnum().WithMessage("Please enter valid Occassion");
            RuleFor(x => x.BookingTime)
                         .NotNull().NotEmpty()
                         .WithMessage("Booking time is required").IsInEnum().WithMessage("Please enter valid BoookingTime");
           
        }


        private bool IsValidDateOfBooking(string doa)
        {
            var formats = new[] { "MM/dd/yyyy", "MM/dd/yyyy HH:mm:ss", "dd/MM/yyyy HH:mm:ss", "dd/MM/yyyy", "MM-dd-yyyy", "MM-dd-yyyy HH:mm:ss", "dd-MM-yyyy HH:mm:ss", "dd-MM-yyyy" };
            DateTime dateOfBooking;
            if (DateTime.TryParseExact(doa, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfBooking))
            {
                if (dateOfBooking > DateTime.Now && dateOfBooking < DateTime.Now.AddDays(30))
                {
                    return true;
                }
            }
            else if (doa == "")
            {
                return false;
            }
            return false;
        }
        public static bool IsValidPhone(string phone)
        {
            return Regex.IsMatch(phone, "\\A[0-9]{10}\\z");
        }

    }
}
