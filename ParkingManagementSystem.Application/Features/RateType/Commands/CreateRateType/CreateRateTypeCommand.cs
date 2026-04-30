using MediatR;
using ParkingManagementSystem.Application.Common.Extensions;
using ParkingManagementSystem.Application.Common.Results;
using ParkingManagementSystem.Application.Interfaces.Repositories;

namespace ParkingManagementSystem.Application.Features.RateType.Commands.CreateRateType
{
    public class CreateRateTypeCommand : IRequest<MessageResult<int>>
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public bool IsHourly { get; set; }
    }

    public class CreateRateTypeCommandHandler : IRequestHandler<CreateRateTypeCommand, MessageResult<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateRateTypeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MessageResult<int>> Handle(CreateRateTypeCommand request, CancellationToken cancellationToken)
        {
            var exists = await _unitOfWork.RateTypes
                .ExistsByNameAsync(request.Name, cancellationToken);

            if (exists)
                ServiceStatus.BadRequest.Throw("Ya existe una tarifa con ese nombre.");

            var rateType = new ParkingManagementSystem.Domain.Entities.RateType
            {
                Name = request.Name,
                Price = request.Price,
                IsHourly = request.IsHourly
            };

            await _unitOfWork.RateTypes.AddAsync(rateType, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return MessageResult<int>.Of("Tarifa registrada correctamente.", rateType.Id);
        }
    }
}
