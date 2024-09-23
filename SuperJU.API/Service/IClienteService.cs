using SuperJU.API.Controllers.Request;
using SuperJU.API.Controllers.Response;

namespace SuperJU.API.Service
{
    public interface IClienteService
    {
        List<ClienteResponse> Pesquisar(int? id, string? nome);
        ClienteResponse BuscarPorId(int id);
        ClienteCadastroResponse Cadastrar(ClienteCadstroEditarRequest clienteRequest);
        void Atualizar(int id, ClienteCadstroEditarRequest clienteRequest);
    }
}
