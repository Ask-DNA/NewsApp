using DomainDTO = NewsApp.Server.Domain.DTO;

namespace NewsApp.Server.Infrastructure.Data.DTO
{
    public class NewsItemDTO(string title, DateOnly date, string excerpt)
    {
        public string Title { get; init; } = title;

        public DateOnly Date { get; init; } = date;

        public string Excerpt { get; init; } = excerpt;

        public static explicit operator DomainDTO.NewsItemDTO(NewsItemDTO dto)
        {
            return new(dto.Title, dto.Date, dto.Excerpt);
        }
    }
}
