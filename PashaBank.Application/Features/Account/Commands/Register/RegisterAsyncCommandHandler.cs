
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using PashaBank.Application.Exceptions;
using PashaBank.Application.Features.Account.Commands.Register;
using PashaBank.Application.Wrappers;
using PashaBank.Domain.Entities;
using PashaBank.Domain.Interfaces;

public class RegisterAsyncCommandHandler : IRequestHandler<RegisterAsyncCommand, Response<bool>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public RegisterAsyncCommandHandler(IUnitOfWork uow, IMapper mapper, IConfiguration configuration)
    {
        _uow = uow;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<Response<bool>> Handle(RegisterAsyncCommand request, CancellationToken cancellationToken)
    {
        var newUser = _mapper.Map<UserEntity>(request);
        var user = await _uow.userRepository.GetAllWhereAsync(x => x.PersonalNumber == newUser.PersonalNumber);
        if (user.Any()) throw new ApiException("User Already Registered");
        newUser.UserName = request.PersonalNumber.ToString();
        newUser.NormalizedUserName = request.PersonalNumber.ToString();
        bool recommendationAllowed = false;
        bool recommendationExceeds = false;
        if (request.RecommendedById != null)
        {
            var levelString = _configuration["RecursiveRegistration:Level"];
            if (int.TryParse(levelString, out var level))
            {
                recommendationAllowed = _uow.userService.IsRecommendationAllowed(request.RecommendedById, level);
                if (recommendationAllowed)
                {
                    recommendationExceeds = _uow.userService.HasRecommendedMoreThanThree(request.RecommendedById);
                    if (recommendationExceeds)
                    {
                        await _uow.userRepository.CreateUser(newUser, request.Password);
                    }
                    else
                    {
                        throw new ApiException("User recommendation already exists maximum value");
                    }
                }
                else
                {
                    throw new ApiException("Recommendation is not allowed");
                }
            }
        }
        else
        {
            await _uow.userRepository.CreateUser(newUser, request.Password);
        }
        return new Response<bool>(true, "User succesfully registered");
    }
}