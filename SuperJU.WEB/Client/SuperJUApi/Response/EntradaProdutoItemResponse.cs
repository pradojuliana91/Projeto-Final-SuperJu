namespace SuperJU.WEB.Client.SuperJUApi.Response
{
    public class EntradaProdutoItemResponse
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorCusto { get; set; }
    }
}
