namespace SuperJU.API.Controllers.Response
{
    public class RelEstoqueResponse
    {
        public int ProdutoId { get; set; }  
        public required string ProdutoNome {  get; set; }    
        public decimal ValorCusto { get; set; } 
        public decimal ValorVenda { get; set; }
        public int Quantidade { get; set; } 
        public int QtdSaidaU30Dia { get; set; }
        public DateTime? DataUltimaVenda { get; set; }
    }
}