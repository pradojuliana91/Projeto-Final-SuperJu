namespace SuperJU.API.Controllers.Request
{
    public class ProdutoEditarRequest
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public int? Quantidade { get; set; }
        public decimal? ValorCusto { get; set; }
        public decimal? ValorVenda { get; set; }
    }
}
