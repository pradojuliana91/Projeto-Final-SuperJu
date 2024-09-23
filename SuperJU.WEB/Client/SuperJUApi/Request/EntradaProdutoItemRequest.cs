using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperJU.WEB.Client.SuperJUApi.Request
{
    public class EntradaProdutoItemRequest
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorCusto { get; set; }
        public decimal ValorVenda { get; set; }
    }
}