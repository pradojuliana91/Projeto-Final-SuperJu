using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SuperJU.WEB.Client.SuperJUApi.Response
{
    public class RelVendaResponse
    {
        public int PedidoId { get; set; }  
        public string ClienteNome {  get; set; }
        public string FormaPagamentoNome { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal ValorTotal { get; set; } 
        public decimal ValorCustoTotal { get; set; }
        public decimal ValorLucro { get; set; } 
    }
}