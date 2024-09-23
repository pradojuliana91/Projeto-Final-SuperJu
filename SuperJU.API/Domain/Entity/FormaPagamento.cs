namespace SuperJU.API.Domain.Entity
{
    public class FormaPagamento
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Descricao { get; set; }
    }
}
