using NewsApp.Server.UseCases.DTO;

namespace NewsApp.Server.UseCases.OutputPorts
{
    public interface INewsPagePresenter<TResult>
    {
        TResult Present(GetNewsResponseDTO dto);
    }
}
