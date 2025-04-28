using NewsApp.Server.Domain.DTO;

namespace NewsApp.Server.Domain.Interfaces.InternalServices
{
    public interface INewsService
    {
        Task<(IEnumerable<NewsItemDTO> news, int filteredNewsTotalNumber)> GetNewsAsync(NewsSearchFormDTO? form = null);
    }
}
