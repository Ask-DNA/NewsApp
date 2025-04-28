using NewsApp.Server.Infrastructure.Data.DTO;

namespace NewsApp.Server.Infrastructure.Data.Interfaces
{
    public interface IWebParser
    {
        Task<NewsItemDTO[]> GetAllNewsAsync();
    }
}
