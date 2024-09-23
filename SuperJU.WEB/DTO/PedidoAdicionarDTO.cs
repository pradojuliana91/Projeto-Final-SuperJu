using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SuperJU.WEB.DTO
{
    public class PedidoAdicionarDTO
    {
        public int ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public int Quantidade { get; set; }        
        public decimal ValorVenda { get; set; }
        public decimal ValorTotal { get; set; }
    }
}