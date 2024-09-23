using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperJU.WEB.Client.SuperJUApi.Request
{
    public class PedidoRequest
    {
        public int ClienteId { get; set; }
        public int FormaPagamentoId { get; set; }
        public decimal ValorTotal { get; set; }
        public List<PedidoItemRequest> Items { get; set; }
    }
}