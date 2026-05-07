using FluentValidation;

namespace ParkingManagementSystem.Application.Features.ParkingEntries.Queries.GetParkingEntryByTicket
{
    public class GetParkingEntryByTicketQueryValidator : AbstractValidator<GetParkingEntryByTicketQuery>
    {
        public GetParkingEntryByTicketQueryValidator()
        {
            RuleFor(x => x.TicketNumber)
                .NotEmpty().WithMessage("El número de ticket es requerido.")
                .MaximumLength(30).WithMessage("El número de ticket no puede exceder los 30 caracteres.");
        }
    }
}
