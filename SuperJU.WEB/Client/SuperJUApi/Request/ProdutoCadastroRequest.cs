using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Web;
using System.Web.UI.WebControls;

namespace SuperJU.WEB.Client.SuperJUApi.Request
{
    public class ProdutoCadastroRequest
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal ValorVenda { get; set; }
    }
}