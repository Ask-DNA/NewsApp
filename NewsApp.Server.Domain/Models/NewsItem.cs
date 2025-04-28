using NewsApp.Server.Domain.DTO;

namespace NewsApp.Server.Domain.Models
{
    internal class NewsItem(string title, DateOnly date, string excerpt)
    {
        public string Title { get; init; } = title;
        public DateOnly Date { get; init; } = date;
        public string Excerpt { get; init; } = excerpt;

        public static explicit operator NewsItem(NewsItemDTO dto)
        {
            return new(dto.Title, dto.Date, dto.Excerpt);
        }

        public static explicit operator NewsItemDTO(NewsItem model)
        {
            return new(model.Title, model.Date, model.Excerpt);
        }

        // Здесь могла бы быть логика поведения модели, но, исходя из текущих требований,
        // новостным статьям не нужно какое-либо собственное поведение
    }
}
