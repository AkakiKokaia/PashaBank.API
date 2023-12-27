
using AutoMapper;
using MediatR;
using PashaBank.Application.Exceptions;
using PashaBank.Application.Features.Account.Commands.Register;
using PashaBank.Application.Wrappers;
using PashaBank.Domain.Entities;
using PashaBank.Domain.Interfaces;

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
            var user = await _uow.userRepository.GetAllWhereAsync(x => x.PersonalNumber == newUser.PersonalNumber);
            newUser.UserName = request.PersonalNumber.ToString();
            newUser.NormalizedUserName = request.PersonalNumber.ToString();
            if (user.Any()) throw new ApiException("User Already Registered");
            await _uow.userRepository.CreateUser(newUser, request.Password);
            return new Response<bool>(true, "User succesfully registered");
        }
        catch (Exception ex)
        {
            throw new ApiException("Something went wrong");
        }
    }
}