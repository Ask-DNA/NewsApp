using DomainDTO = NewsApp.Server.Domain.DTO;
using NewsApp.Server.Domain.Interfaces.InternalServices;
using NewsApp.Server.UseCases.DTO;
using NewsApp.Server.UseCases.InputPorts;
using NewsApp.Server.UseCases.OutputPorts;

namespace NewsApp.Server.UseCases.Interactors
{
    public class GetNewsInteractor<TResult>(INewsService newsService, INewsPagePresenter<TResult> presenter)
        : IGetNewsInputPort<TResult>
    {
        public async Task<TResult> GetNewsAsync(GetNewsRequestDTO requestDTO)
        {
            if (!requestDTO.Validate())
                return presenter.Present(GetNewsResponseDTO.InvalidRequest());
            
            var form = (DomainDTO.NewsSearchFormDTO?)requestDTO;
            (IEnumerable<DomainDTO.NewsItemDTO> news, int filteredNewsTotalNumber) = await newsService.GetNewsAsync(form);
            GetNewsResponseDTO responseDTO = new(news, filteredNewsTotalNumber);
            return presenter.Present(responseDTO);
        }
    }
}
