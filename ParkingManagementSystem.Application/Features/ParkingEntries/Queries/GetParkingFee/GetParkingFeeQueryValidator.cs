using FluentValidation;

namespace ParkingManagementSystem.Application.Features.ParkingEntries.Queries.GetParkingFee
{
    public class GetParkingFeeQueryValidator : AbstractValidator<GetParkingFeeQuery>
    {
        public GetParkingFeeQueryValidator()
        {
            RuleFor(x => x.TicketNumber)
                .NotEmpty().WithMessage("El número de ticket es requerido.")
                .MaximumLength(30).WithMessage("El número de ticket no puede exceder los 30 caracteres.");
        }
    }
}