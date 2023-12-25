﻿using AutoMapper;
using PashaBank.Application.DTOs.Account;
using PashaBank.Application.Exceptions;
using PashaBank.Application.Wrappers;
using PashaBank.Domain.Interfaces;
using PashaBank.Domain.Interfaces.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace PashaBank.Application.Features.Account.Commands
{
    public class LoginAsyncCommand : IRequest<Response<LoginResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginAsyncCommandHandler : IRequestHandler<LoginAsyncCommand, Response<LoginResponse>>
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly ITokenService tokenService;

        public LoginAsyncCommandHandler(IUnitOfWork uow, IMapper mapper, ITokenService tokenService)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.tokenService = tokenService;
        }

        public async Task<Response<LoginResponse>> Handle(LoginAsyncCommand request, CancellationToken cancellationToken)
        {
            var user = await uow.userRepository.ValidateUser(request.Email.ToLower(), request.Password);

            if (user == null) throw new ApiException("Email or Password Incorrect");

            var creds = await tokenService.CreateReturnCredentials(user, cancellationToken);

            var result = new LoginResponse
            {
                AccessToken = creds.AccessToken,
                RefreshToken = creds.RefreshToken,
                user = mapper.Map<UserResponse>(user)
            };

            return new Response<LoginResponse>(result, "Success Response");
        }
    }
}