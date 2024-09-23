using SuperJU.API.Domain.Entity;

namespace SuperJU.API.Domain.Repository
{
    public interface IEntradaProdutoItemRepository
    {
        List<EntradaProdutoItem> BuscaPorEntradaProdutoId(int entradaProdutoId);
        void Inserir(EntradaProdutoItem entradaProdutoItem);
    }
}
