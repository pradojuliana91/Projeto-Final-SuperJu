using SuperJU.API.Domain.Entity;

namespace SuperJU.API.Domain.Repository
{
    public interface IFormaPagamentoRepository
    {
        List<FormaPagamento>? BuscaTodos();
    }
}
