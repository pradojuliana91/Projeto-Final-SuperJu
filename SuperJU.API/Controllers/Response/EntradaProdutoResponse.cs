namespace SuperJU.API.Controllers.Response
{
    public class EntradaProdutoResponse
    {
        public int Id { get; set; }
        public required string NumeroNota { get; set; }
        public DateTime DataEntrada { get; set; }
        public List<EntradaProdutoItemResponse>? Produtos { get; set; }
    }
}
