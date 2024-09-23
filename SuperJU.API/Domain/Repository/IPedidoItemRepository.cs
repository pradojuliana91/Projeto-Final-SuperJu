using SuperJU.API.Domain.Entity;

namespace SuperJU.API.Domain.Repository
{
    public interface IPedidoItemRepository
    {
        List<PedidoItem> BuscaPorPedidoId(int pedidoId);
        void Inserir(PedidoItem pedidoItem);
    }
}
