using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PashaBank.Application.Exceptions;
using PashaBank.Application.Features.Product.Commands;
using PashaBank.Application.Wrappers;
using PashaBank.Domain.Entities;
using PashaBank.Domain.Interfaces;
using PashaBank.Infrastructure;

namespace PashaBank.Application.Features.Account.Commands.Update
{
    public class UpdateUserAsyncCommandHandler : IRequestHandler<UpdateUserAsyncCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly PashaBankDbContext _context;

        public UpdateUserAsyncCommandHandler(IUnitOfWork uow, IMapper mapper, PashaBankDbContext context)
        {
            _uow = uow;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Response<bool>> Handle(UpdateUserAsyncCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

            _mapper.Map(request, user);
            await _uow.userRepository.Update(user);
            return new Response<bool>(true, "User successfully updated");
        }
    }
}
