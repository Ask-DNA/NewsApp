namespace NewsApp.Server.Domain.DTO
{
    public record NewsItemDTO(string Title, DateOnly Date, string Excerpt);
}
