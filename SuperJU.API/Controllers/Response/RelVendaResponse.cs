namespace SuperJU.API.Controllers.Response
{
    public class RelVendaResponse
    {
        public int PedidoId { get; set; }  
        public required string ClienteNome {  get; set; }
        public required string FormaPagamentoNome { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal ValorTotal { get; set; } 
        public decimal ValorCustoTotal { get; set; }
        public decimal ValorLucro { get; set; } 
    }
}