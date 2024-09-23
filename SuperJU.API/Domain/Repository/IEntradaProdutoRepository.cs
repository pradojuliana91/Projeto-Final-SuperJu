using SuperJU.API.Domain.Entity;

namespace SuperJU.API.Domain.Repository
{
    public interface IEntradaProdutoRepository
    {
        List<EntradaProduto>? Pesquisar(string? numeroNota, DateTime? dataInicio, DateTime? dataFim);
        EntradaProduto? BuscaPorId(int id);
        int Inserir(EntradaProduto entradaProduto);
    }
}

