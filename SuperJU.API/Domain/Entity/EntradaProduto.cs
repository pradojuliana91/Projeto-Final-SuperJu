namespace SuperJU.API.Domain.Entity
{
    public class EntradaProduto
    {
        public int Id { get; set; }
        public required string NumeroNota { get; set; }
        public DateTime DataEntrada { get; set; }
    }
}
