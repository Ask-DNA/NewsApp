using Microsoft.AspNetCore.Mvc;
using NewsApp.Server.UseCases.DTO;
using NewsApp.Server.UseCases.InputPorts;

namespace NewsApp.Server.WebAPI.Controllers
{
    [ApiController]
    [Route("Api")]
    public class NewsController(IGetNewsInputPort<IActionResult> getNewsInteractor) : Controller
    {
        [HttpGet]
        [Route("News/Get")]
        public async Task<IActionResult> Get([FromQuery]GetNewsRequestDTO requestDTO)
        {
            IActionResult result = await getNewsInteractor.GetNewsAsync(requestDTO);
            return result;
        }
    }
}
