using AutoMapper;
using MediatR;
using PashaBank.Application.Features.Account.Commands.Delete;
using PashaBank.Application.Wrappers;
using PashaBank.Domain.Interfaces;

public class SoftDeleteUserAsyncCommandHandler : IRequestHandler<DeleteUserAsyncCommand, Response<bool>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public SoftDeleteUserAsyncCommandHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<Response<bool>> Handle(DeleteUserAsyncCommand request, CancellationToken cancellationToken)
    {
        var user = await _uow.userRepository.GetById(request.Id);
        if (user == null) throw new Exception("Card is null");

        await _uow.userRepository.Delete(user, cancellationToken);
        return new Response<bool>(true, "Success Response");
    }
}