using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperJU.WEB.Client.SuperJUApi.Request
{
    public class PedidoItemRequest
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorTotal { get; set; }
    }
}