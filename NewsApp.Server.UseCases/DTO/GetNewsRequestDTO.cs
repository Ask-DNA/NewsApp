using NewsApp.Server.Domain.DTO;

namespace NewsApp.Server.UseCases.DTO
{
    public class GetNewsRequestDTO
    {
        public string SearchString { get; init; } = string.Empty;
        public string? DateFrom { get; init; } = null;
        public string? DateTo { get; init; } = null;
        public string? OrderBy { get; init; } = null;
        public int? PageNum { get; init; } = null;
        public int? PageSize { get; init; } = null;

        //Не используется статическое поле для обеспечения потокобезопасности
        private readonly Dictionary<string, NewsSearchFormDTO.OrderingOption> OrderingOptionMapping = new()
        {
            { "keep original", NewsSearchFormDTO.OrderingOption.OriginalOrder },
            { "date asc", NewsSearchFormDTO.OrderingOption.ByDateAsc },
            { "date desc", NewsSearchFormDTO.OrderingOption.ByDateDesc },
            { "date", NewsSearchFormDTO.OrderingOption.ByDateAsc }
        };

        public bool Validate()
        {
            if (DateFrom is not null && !DateOnly.TryParse(DateFrom, out _))
                return false;
            if (DateTo is not null && !DateOnly.TryParse(DateTo, out _))
                return false;
            if (DateFrom is not null && DateTo is not null)
                if (DateOnly.Parse(DateFrom) > DateOnly.Parse(DateTo))
                    return false;
            if (OrderBy is not null && !OrderingOptionMapping.ContainsKey(OrderBy.ToLower()))
                return false;
            if ((PageNum is not null && PageNum <= 0) || (PageSize is not null && PageSize <= 0))
                return false;
            if (PageNum is not null && PageNum != 1 && PageSize is null)
                return false;

            return true;
        }

        public static explicit operator NewsSearchFormDTO?(GetNewsRequestDTO model)
        {
            if (!model.Validate())
                throw new InvalidOperationException("Invalid request DTO");

            NewsSearchFormDTO result = new()
            {
                SearchString = model.SearchString,
                DateFrom = model.DateFrom is null ? null : DateOnly.Parse(model.DateFrom),
                DateTo = model.DateTo is null ? null : DateOnly.Parse(model.DateTo),
                Ordering = model.OrderBy is null
                    ? NewsSearchFormDTO.OrderingOption.OriginalOrder
                    : model.OrderingOptionMapping[model.OrderBy!.ToLower()],
                Limit = model.PageSize,
                Offset = model.PageSize is null ? 0 : model.PageSize.Value * (model.PageNum!.Value - 1)
            };

            return result;
        }
    }
}
