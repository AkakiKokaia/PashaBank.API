﻿using AutoMapper;
using MediatR;
using PashaBank.Application.DTOs.User;
using PashaBank.Application.Wrappers;
using PashaBank.Domain.Interfaces;

namespace PashaBank.Application.Features.Account.Queries
{
    public class GetUserByIdAsyncQuery : IRequest<Response<GetUserResponse>>
    {
        public Guid Id { get; set; }
    }

    public class GetUserByIdAsyncQueryHandler : IRequestHandler<GetUserByIdAsyncQuery, Response<GetUserResponse>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetUserByIdAsyncQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Response<GetUserResponse>> Handle(GetUserByIdAsyncQuery request, CancellationToken cancellationToken)
        {
            var user = await _uow.userRepository.GetById(request.Id);
            var result = _mapper.Map<GetUserResponse>(user);

            return new Response<GetUserResponse>(result);
        }
    }
}
