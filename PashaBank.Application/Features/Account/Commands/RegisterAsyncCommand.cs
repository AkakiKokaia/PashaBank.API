using AutoMapper;
using PashaBank.Application.Exceptions;
using PashaBank.Application.Wrappers;
using PashaBank.Domain.Entities;
using PashaBank.Domain.Interfaces;
using PashaBank.Domain.Interfaces.Repositories.User;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PashaBank.Application.Features.Account.Commands
{
    public class RegisterAsyncCommand : IRequest<Response<bool>>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IDNumber { get; set; }
        public string Password { get; set; }
    }

    public class RegisterAsyncCommandHandler : IRequestHandler<RegisterAsyncCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public RegisterAsyncCommandHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(RegisterAsyncCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newUser = _mapper.Map<UserEntity>(request);
                var user = await _uow.userRepository.GetAllWhereAsync(x => x.Email == newUser.Email.ToLower());
                if (user.Any()) throw new ApiException("User Already Registered");
                newUser.UserName = request.Email;
                newUser.Email = request.Email.ToLower();
                await _uow.userRepository.CreateUser(newUser, request.Password);
                return new Response<bool>(true, "User succesfully registered");
            }
            catch (Exception ex)
            {
                throw new ApiException("Something went wrong");
            }
        }
    }
}
