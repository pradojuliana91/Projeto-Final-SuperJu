﻿namespace SuperJU.API.Controllers.Request
{
    public class EntradaProdutoItemRequest
    {
        public int? ProdutoId { get; set; }
        public int? Quantidade { get; set; }
        public decimal? ValorCusto { get; set; }
        public decimal? ValorVenda { get; set; }
    }
}
