
using AutoMapper;
using MediatR;
using PashaBank.Application.DTOs.User;
using PashaBank.Application.Features.Account.Queries.Users;
using PashaBank.Application.Wrappers;
using PashaBank.Domain.Interfaces;

public class GetUsersAsyncQueryHandler : IRequestHandler<GetUsersAsyncQuery, Response<List<GetUserResponse>>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetUsersAsyncQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }
    public async Task<Response<List<GetUserResponse>>> Handle(GetUsersAsyncQuery request, CancellationToken cancellationToken)
    {
        var user = _uow.userRepository.GetAll();
        var result = _mapper.Map<List<GetUserResponse>>(user);

        return new Response<List<GetUserResponse>>(result);
    }
}