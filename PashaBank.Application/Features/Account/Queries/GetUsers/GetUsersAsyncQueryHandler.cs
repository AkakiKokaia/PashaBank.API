using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PashaBank.Domain.Interfaces;
using PashaBank.Infrastructure;

namespace PashaBank.Application.Features.Account.Queries.GetUsers
{
    public class GetUsersAsyncQueryHandler : IRequestHandler<GetUsersAsyncQuery, GetUsersAsyncQueryResponse>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly PashaBankDbContext _dbContext;

        public GetUsersAsyncQueryHandler(IUnitOfWork uow, IMapper mapper, IHttpContextAccessor httpContextAccessor, PashaBankDbContext dbContext)
        {
            _uow = uow;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        public async Task<GetUsersAsyncQueryResponse> Handle(GetUsersAsyncQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Users.Where(x => !x.LockoutEnabled);
            var totalCount = await query.CountAsync();

            var users = await query
                .OrderBy(x => x.Id)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new GetUsersAsyncQueryResponseItem
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    Surname = x.Surname,
                    DateOfBirth = x.DateOfBirth,
                    Gender = x.Gender,
                    PhotoURL = x.PhotoURL,
                    CreatedAt = x.CreatedAt,
                    DocumentType = x.DocumentType,
                    DocumentNumber = x.DocumentNumber,
                    DocumentSeries = x.DocumentSeries,
                    DateOfIssue = x.DateOfIssue,
                    DateOfExpiry = x.DateOfExpiry,
                    PersonalNumber = x.PersonalNumber,
                    IssuingAgency = x.IssuingAgency,
                    ContactType = x.ContactType,
                    ContactInformation = x.ContactInformation,
                    AddressType = x.AddressType,
                    Address = x.Address,
                    RecommendedById = x.RecommendedById,
                    AccummulatedBonus = x.AccummulatedBonus
                })
                .ToListAsync();


            return new GetUsersAsyncQueryResponse(users, request.Page, request.PageSize, totalCount);
        }
    }
}
