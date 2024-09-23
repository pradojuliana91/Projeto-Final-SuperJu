namespace SuperJU.API.Domain.Entity
{
    public class EntradaProdutoItem
    {
        public int Id { get; set; }
        public int EntradaProdutoId { get; set; }
        public int ProdutoId { get; set; }
        public string? ProdutoNome { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorCusto { get; set; }
    }
}
