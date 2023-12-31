﻿using PashaBank.Application.Features.Helpers;
using PashaBank.Domain.Enums;

namespace PashaBank.Application.Features.Account.Queries.GetUsers
{
    public class GetUsersAsyncQueryResponse : PagedResponseBase
    {
        public GetUsersAsyncQueryResponse(List<GetUsersAsyncQueryResponseItem> users, int page, int pageSize, int totalCount)
            : base(page, pageSize, totalCount)
        {
            Users = users;
        }

        public List<GetUsersAsyncQueryResponseItem> Users { get; }
    }

    public class GetUsersAsyncQueryResponseItem
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public GenderTypeEnum Gender { get; set; }
        public string PhotoURL { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DocumentTypeEnum DocumentType { get; set; }
        public string? DocumentSeries { get; set; }
        public string? DocumentNumber { get; set; }
        public DateTimeOffset DateOfIssue { get; set; }
        public DateTimeOffset DateOfExpiry { get; set; }
        public long PersonalNumber { get; set; }
        public string? IssuingAgency { get; set; }
        public ContactTypeEnum ContactType { get; set; }
        public string ContactInformation { get; set; }
        public AddressType AddressType { get; set; }
        public string Address { get; set; }
        public Guid? RecommendedById { get; set; }
        public decimal AccummulatedBonus { get; set; }
    }
}
