namespace PashaBank.Application.Features.Helpers
{
    public abstract class PagedResponseBase
    {
        public PagedResponseBase(
            int page,
            int pageSize,
            int totalCount)
        {
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}
