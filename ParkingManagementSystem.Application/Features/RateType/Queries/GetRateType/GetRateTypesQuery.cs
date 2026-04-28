using MediatR;
using ParkingManagementSystem.Application.Common.Results;
using ParkingManagementSystem.Application.DTOs;
using ParkingManagementSystem.Application.Interfaces.Repositories;

namespace ParkingManagementSystem.Application.Features.RateType.Queries.GetRateType
{
    public class GetRateTypesQuery : IRequest<MessageResult<List<GetRateTypesResponseDto>>>
    {
    }

    public class GetRateTypesQueryHandler: IRequestHandler<GetRateTypesQuery, MessageResult<List<GetRateTypesResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRateTypesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MessageResult<List<GetRateTypesResponseDto>>> Handle(GetRateTypesQuery request, CancellationToken cancellationToken)
        {
            var rateTypes = await _unitOfWork.RateTypes.GetAllAsync(cancellationToken);

            var response = rateTypes.Select(rt => new GetRateTypesResponseDto
            {
                Id = rt.Id,
                Name = rt.Name,
                Price = rt.Price,
                IsHourly = rt.IsHourly
            }).ToList();

            return MessageResult<List<GetRateTypesResponseDto>>.Of("Consulta realizada correctamente.", response);
        }
    }
}
