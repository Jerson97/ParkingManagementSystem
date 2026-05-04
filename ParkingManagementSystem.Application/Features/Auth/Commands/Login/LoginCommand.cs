using MediatR;
using ParkingManagementSystem.Application.Common.Extensions;
using ParkingManagementSystem.Application.Common.Results;
using ParkingManagementSystem.Application.DTOs;
using ParkingManagementSystem.Application.Interfaces.Repositories;
using ParkingManagementSystem.Application.Interfaces.Token;

namespace ParkingManagementSystem.Application.Features.Auth.Commands.Login
{
    public class LoginCommand : IRequest<MessageResult<LoginResponseDto>>
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, MessageResult<LoginResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginCommandHandler( IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<MessageResult<LoginResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users
                .GetByUsernameAsync(request.Username, cancellationToken);

            if (user is null)
                ServiceStatus.Unauthorized.Throw("Usuario o contraseña incorrectos.");

            if (!user.IsActive)
                ServiceStatus.Forbidden.Throw("El usuario se encuentra desactivado.");

            var isValidPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!isValidPassword)
                ServiceStatus.Unauthorized.Throw("Usuario o contraseña incorrectos.");

            var token = _jwtTokenGenerator.GenerateToken(user);

            var response = new LoginResponseDto
            {
                UserId = user.Id,
                FullName = user.FullName,
                Username = user.Username,
                Role = user.Role.ToString(),
                Token = token
            };

            return MessageResult<LoginResponseDto>.Of("Inicio de sesión correcto.", response);
        }
    }
}
