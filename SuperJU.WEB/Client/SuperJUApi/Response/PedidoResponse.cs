using System;
using System.Collections.Generic;

namespace SuperJU.WEB.Client.SuperJUApi.Response
{
    public class PedidoResponse
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public int FormaPagamentoId { get; set; }
        public string FormaPagamentoNome { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal ValorTotal { get; set; }
        public List<PedidoItemResponse> Items { get; set; }
    }
}
