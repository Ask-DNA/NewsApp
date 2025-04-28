using NewsApp.Server.UseCases.DTO;

namespace NewsApp.Server.UseCases.InputPorts
{
    public interface IGetNewsInputPort<TResult>
    {
        Task<TResult> GetNewsAsync(GetNewsRequestDTO requestDTO);
    }
}
