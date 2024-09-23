namespace SuperJU.API.Controllers.Request
{
    public class EntradaProdutoRequest
    {
        public string? NumeroNota { get; set; }
        public List<EntradaProdutoItemRequest>? Produtos { get; set; }
    }
}
