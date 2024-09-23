using SuperJU.API.Domain.Entity;

namespace SuperJU.API.Domain.Repository
{
    public interface IClienteRepository
    {
        List<Cliente>? Pesquisar(int? id, string? nome);
        Cliente? BuscaPorId(int id);
        int Inserir(Cliente cliente);
        void Editar(int idCliente, Cliente cliente);
    }
}
