namespace SuperJU.API.Domain.Entity
{
    public class Pedido
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string? ClienteNome { get; set; }
        public int FormaPagamentoId { get; set; }
        public string? FormaPagamentoNome { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
