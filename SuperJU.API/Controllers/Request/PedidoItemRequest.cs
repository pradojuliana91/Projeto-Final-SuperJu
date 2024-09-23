namespace SuperJU.API.Controllers.Request
{
    public class PedidoItemRequest
    {
        public int? ProdutoId { get; set; }
        public int? Quantidade { get; set; }
        public decimal? Valor { get; set; }
        public decimal? ValorTotal { get; set; }
    }
}