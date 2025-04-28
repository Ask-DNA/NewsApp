using NewsApp.Server.Domain.DTO;

namespace NewsApp.Server.Domain.Interfaces.ExternalServices
{
    public interface INewsRepository
    {
        Task<IEnumerable<NewsItemDTO>> GetAllNewsAsync();
    }
}
