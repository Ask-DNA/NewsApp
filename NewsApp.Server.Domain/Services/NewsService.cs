using NewsApp.Server.Domain.DTO;
using NewsApp.Server.Domain.Interfaces.ExternalServices;
using NewsApp.Server.Domain.Interfaces.InternalServices;
using NewsApp.Server.Domain.Models;

namespace NewsApp.Server.Domain.Services
{
    public class NewsService(INewsRepository repository) : INewsService
    {
        // Возвращает выбранные новости и число всех новостей ПРОШЕДШИХ ФИЛЬТРАЦИЮ (нужно для корректной постраничной навигации)
        public async Task<(IEnumerable<NewsItemDTO> news, int filteredNewsTotalNumber)> GetNewsAsync(NewsSearchFormDTO? form = null)
        {
            IEnumerable<NewsItemDTO> news = await repository.GetAllNewsAsync();
            int filteredNewsTotalNumber = news.Count();
            if (form is not null)
            {
                NewsSearchForm formDomain = (NewsSearchForm)form;

                // Отображение DTO на доменные модели
                NewsItem[] newsDomain = [.. news.Select(n => (NewsItem)n)];

                // Фильтрация
                newsDomain = [.. formDomain.ApplyFiltration(newsDomain)];
                filteredNewsTotalNumber = newsDomain.Length;

                // Сортировка
                newsDomain = [.. formDomain.ApplyOrdering(newsDomain)];

                // Выборка по limit и offset (для постраничного отображения списка новостей)
                newsDomain = [.. formDomain.ApplySlicing(newsDomain)];

                // Обратное отображение
                news = [.. newsDomain.Select(n => (NewsItemDTO)n)];
            }
            return (news, filteredNewsTotalNumber);
        }
    }
}
