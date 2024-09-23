using SuperJU.API.Controllers.Response;
using SuperJU.API.Domain.Entity;

namespace SuperJU.API.Domain.Repository
{
    public interface IPedidoRepository
    {
        List<Pedido>? Pesquisar(int? pedidoId, int? clienteId, int? formaPagamentoId, DateTime? dataInicio, DateTime? dataFim);
        Pedido? BuscaPorId(int id);
        int Inserir(Pedido pedido);
        List<RelVendaResponse>? RelVenda(int? pedidoId, int? clienteId, int? formaPagamentoId, DateTime? dataInicio, DateTime? dataFim);
    }
}