namespace SuperJU.API.Controllers.Response
{
    public class PedidoItemResponse
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public required string ProdutoNome { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
