using NewsApp.Server.Domain.DTO;

namespace NewsApp.Server.Domain.Models
{
    internal class NewsSearchForm
    {
        private readonly string _searchString = string.Empty;
        private readonly DateOnly? _dateFrom = null;
        private readonly DateOnly? _dateTo = null;
        private readonly int? _limit = null;
        private readonly int _offset = 0;
        private readonly IComparer<NewsItem>? _comparer = null;
        private readonly ITextSearchStrategy _textSearchStrategy;

        private NewsSearchForm(NewsSearchFormDTO dto)
        {
            if (dto.DateFrom is not null && dto.DateTo is not null)
                ArgumentOutOfRangeException.ThrowIfGreaterThan((DateOnly)dto.DateFrom, (DateOnly)dto.DateTo, nameof(dto.DateFrom));
            if (dto.Limit is not null)
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero((int)dto.Limit, nameof(dto.Limit));
            ArgumentOutOfRangeException.ThrowIfNegative(dto.Offset, nameof(dto.Offset));

            _searchString = dto.SearchString;
            _dateFrom = dto.DateFrom;
            _dateTo = dto.DateTo;
            _limit = dto.Limit;
            _offset = dto.Offset;

            _comparer = dto.Ordering switch
            {
                NewsSearchFormDTO.OrderingOption.OriginalOrder => null,
                NewsSearchFormDTO.OrderingOption.ByDateAsc => Comparer<NewsItem>.Create((x, y) => x.Date.CompareTo(y.Date)),
                NewsSearchFormDTO.OrderingOption.ByDateDesc => Comparer<NewsItem>.Create((x, y) => y.Date.CompareTo(x.Date)),
                _ => null
            };

            // Стратегия поиска по заголовкам существует всего одна, но в будущем можно добавлять новые
            // (для этого также нужно изменить DTO, чтобы он передавал информацию о выбранной стратегии)
            _textSearchStrategy = new TextSearchStrategyPartialMatch(caseSensitive: false);
        }

        public static explicit operator NewsSearchForm(NewsSearchFormDTO dto)
        {
            return new(dto);
        }

        public IEnumerable<NewsItem> ApplyFiltration(IEnumerable<NewsItem> news)
        {
            news = _searchString == string.Empty ? news : news.Where(n => _textSearchStrategy.Fits(n.Title, _searchString));
            news = _dateFrom is null ? news : news.Where(n => n.Date >= _dateFrom);
            news = _dateTo is null ? news : news.Where(n => n.Date <= _dateTo);
            return news;
        }

        public IEnumerable<NewsItem> ApplyOrdering(IEnumerable<NewsItem> news) 
        {
            return _comparer is null ? news : news.OrderBy(x => x, _comparer);
        }

        public IEnumerable<NewsItem> ApplySlicing(IEnumerable<NewsItem> news)
        {
            return _limit is null ? news.Skip(_offset) : news.Skip(_offset).Take((int)_limit);
        }
    }
}
