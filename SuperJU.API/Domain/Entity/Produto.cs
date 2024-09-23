namespace SuperJU.API.Domain.Entity
{
    public class Produto
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Descricao { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorCusto { get; set; }
        public decimal ValorVenda { get; set; }
    }
}
