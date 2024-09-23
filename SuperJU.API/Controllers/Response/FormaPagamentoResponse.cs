namespace SuperJU.API.Controllers.Response
{
    public class FormaPagamentoResponse
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Descricao { get; set; }
    }
}
