using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using NewsApp.Server.Domain.Interfaces.ExternalServices;
using NewsApp.Server.Infrastructure.Data.DTO;
using NewsApp.Server.Infrastructure.Data.Interfaces;
using DomainDTO = NewsApp.Server.Domain.DTO;

namespace NewsApp.Server.Infrastructure.Data.Repository
{
    public class NewsRepository(IDistributedCache cache, IWebParser parser) : INewsRepository
    {
        private const string CacheKey = "news";
        private const int CacheRelativeExpirationInMinutes = 2;

        public async Task<IEnumerable<DomainDTO.NewsItemDTO>> GetAllNewsAsync()
        {
            // Если кэш не содержит искомого значения, выполняется обращение к парсеру
            NewsItemDTO[] news = await FromCache() ?? await FromParser();
            return [.. news.Select(n => (DomainDTO.NewsItemDTO)n)];
        }

        private async Task<NewsItemDTO[]?> FromCache()
        {
            // В кэше список новостей хранится в виде массива, единым вхождением
            string? serializedNews = await cache.GetStringAsync(CacheKey);
            if (serializedNews is null)
                return null;
            return JsonSerializer.Deserialize<NewsItemDTO[]>(serializedNews)
                ?? throw new InvalidOperationException("Impossible to deserialize cache entry");
        }

        private async Task<NewsItemDTO[]> FromParser()
        {
            NewsItemDTO[] news = await parser.GetAllNewsAsync();

            // Сериализация и добавление в кэш полученного из парсера списка новостей
            string serializedNews = JsonSerializer.Serialize(news);
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CacheRelativeExpirationInMinutes)
            };
            await cache.SetStringAsync(CacheKey, serializedNews, cacheOptions);

            return news;
        }
    }
}
