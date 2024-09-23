namespace SuperJU.API.Controllers.Response
{
    public class PedidoResponse
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public required string ClienteNome { get; set; }
        public int FormaPagamentoId { get; set; }
        public required string FormaPagamentoNome { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal ValorTotal { get; set; }
        public List<PedidoItemResponse>? Items { get; set; }
    }
}
