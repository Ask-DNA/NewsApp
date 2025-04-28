using DomainDTO = NewsApp.Server.Domain.DTO;

namespace NewsApp.Server.UseCases.DTO
{
    public class GetNewsResponseDTO
    {
        public NewsItemDTO[]? NewsItems { get; init; } = null;
        public int? FilteredNewsTotalNumber { get; init; } = null;
        public bool RequestIsValid { get; init; } = false;

        public GetNewsResponseDTO(IEnumerable<DomainDTO.NewsItemDTO> news, int filteredNewsTotalNumber)
        {
            NewsItems = [.. news.Select(n => new NewsItemDTO(n.Title, n.Date.ToString(), n.Excerpt))];
            FilteredNewsTotalNumber = filteredNewsTotalNumber;
            RequestIsValid = true;
        }

        private GetNewsResponseDTO()
        {
            RequestIsValid = false;
        }

        public static GetNewsResponseDTO InvalidRequest()
        {
            return new GetNewsResponseDTO();
        }
    }
}
