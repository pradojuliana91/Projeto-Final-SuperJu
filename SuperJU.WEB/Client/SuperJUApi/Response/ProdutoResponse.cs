using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Web;
using System.Web.UI.WebControls;

namespace SuperJU.WEB.Client.SuperJUApi.Response
{
    public class ProdutoResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public decimal? ValorCusto { get; set; }
        public decimal ValorVenda { get; set; }
    }
}