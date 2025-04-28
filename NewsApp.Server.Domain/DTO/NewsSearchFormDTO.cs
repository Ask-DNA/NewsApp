namespace NewsApp.Server.Domain.DTO
{
    public class NewsSearchFormDTO
    {
        public string SearchString { get; init; } = string.Empty;
        public DateOnly? DateFrom { get; init; } = null;
        public DateOnly? DateTo { get; init; } = null;
        public OrderingOption Ordering { get; init; } = OrderingOption.OriginalOrder;
        public int? Limit { get; init; } = null;
        public int Offset { get; init; } = 0;

        public enum OrderingOption
        {
            OriginalOrder,
            ByDateAsc,
            ByDateDesc
        }
    }
}
