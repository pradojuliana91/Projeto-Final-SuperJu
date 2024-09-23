namespace SuperJU.API.Controllers.Response
{
    public class EntradaProdutoItemResponse
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public required string ProdutoNome { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorCusto { get; set; }
    }
}
