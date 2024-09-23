namespace SuperJU.API.Controllers.Request
{
    public class PedidoRequest
    {
        public int? ClienteId { get; set; }
        public int? FormaPagamentoId { get; set; }
        public decimal? ValorTotal { get; set; }
        public List<PedidoItemRequest>? Items { get; set; }
    }
}