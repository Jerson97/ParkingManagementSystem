using System.Text.Json.Serialization;
using MediatR;
using ParkingManagementSystem.Application.Common.Extensions;
using ParkingManagementSystem.Application.Common.Results;
using ParkingManagementSystem.Application.Interfaces.Repositories;

namespace ParkingManagementSystem.Application.Features.RateType.Commands.UpdateRateType
{
    public class UpdateRateTypeCommand : IRequest<MessageResult<int>>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public bool? IsHourly { get; set; }
    }

    public class UpdateRateTypeCommandHandler: IRequestHandler<UpdateRateTypeCommand, MessageResult<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRateTypeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MessageResult<int>> Handle(UpdateRateTypeCommand request, CancellationToken cancellationToken)
        {
            var rateType = await _unitOfWork.RateTypes
                .GetByIdAsync(request.Id, cancellationToken);

            if (rateType is null)
                ServiceStatus.NotFound.Throw("No se encontró la tarifa.");

            if (!string.IsNullOrWhiteSpace(request.Name))
                rateType.Name = request.Name;

            if (request.Price.HasValue)
                rateType.Price = request.Price.Value;

            if (request.IsHourly.HasValue)
                rateType.IsHourly = request.IsHourly.Value;

            _unitOfWork.RateTypes.Update(rateType);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return MessageResult<int>.Of("Tarifa actualizada correctamente.", rateType.Id);
        }
    }
}
