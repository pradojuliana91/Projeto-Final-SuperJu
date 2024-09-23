using SuperJU.API.Controllers.Request;
using SuperJU.API.Controllers.Response;
using SuperJU.API.Domain.Entity;
using SuperJU.API.Domain.Repository;
using SuperJU.API.Exceptions;

namespace SuperJU.API.Service
{
    public interface IPedidoService
    {
        List<PedidoResponse> Pesquisa(int? pedidoId, int? clienteId, int? formaPagamentoId, DateTime? dataInicio, DateTime? dataFim);
        PedidoResponse BuscaPorId(int id);
        PedidoCadastroResponse Inserir(PedidoRequest pedidoRequest);
        List<FormaPagamentoResponse> BuscaFormasPagamento();
        List<RelVendaResponse> RelVenda(int? pedidoId, int? clienteId, int? formaPagamentoId, DateTime? dataInicio, DateTime? dataFim);
    }
}
