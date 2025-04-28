using Microsoft.AspNetCore.Mvc;
using NewsApp.Server.UseCases.DTO;
using NewsApp.Server.UseCases.OutputPorts;

namespace NewsApp.Server.WebAPI.Presenters
{
    public class NewsPagePresenter : INewsPagePresenter<IActionResult>
    {
        public IActionResult Present(GetNewsResponseDTO dto)
        {
            if (!dto.RequestIsValid)
                return new BadRequestResult();
            
            return new OkObjectResult(new { News = dto.NewsItems, dto.FilteredNewsTotalNumber });
        }
    }
}
