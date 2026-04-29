using MediatR;
using ParkingManagementSystem.Application.Common.Extensions;
using ParkingManagementSystem.Application.Common.Results;
using ParkingManagementSystem.Application.Interfaces.Repositories;

namespace ParkingManagementSystem.Application.Features.RateType.Commands.DeleteRateType
{
    public class DeleteRateTypeCommand : IRequest<MessageResult<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteRateTypeCommandHandler : IRequestHandler<DeleteRateTypeCommand, MessageResult<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRateTypeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MessageResult<int>> Handle(DeleteRateTypeCommand request, CancellationToken cancellationToken)
        {
            var rateType = await _unitOfWork.RateTypes
                .GetByIdAsync(request.Id, cancellationToken);

            if (rateType is null)
                ServiceStatus.NotFound.Throw("No se encontró la tarifa.");

            if (!rateType.IsActive)
                ServiceStatus.BadRequest.Throw("La tarifa ya se encuentra desactivada.");

            rateType.IsActive = false;

            _unitOfWork.RateTypes.Update(rateType);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return MessageResult<int>.Of("Tarifa desactivada correctamente.", rateType.Id);
        }
    }
}
