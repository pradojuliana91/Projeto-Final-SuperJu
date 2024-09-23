using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SuperJU.WEB.Client.SuperJUApi.Response
{
    public class RelEstoqueResponse
    {
        public int ProdutoId { get; set; }  
        public string ProdutoNome {  get; set; }    
        public decimal ValorCusto { get; set; } 
        public decimal ValorVenda { get; set; }
        public int Quantidade { get; set; } 
        public int QtdSaidaU30Dia { get; set; }
        public DateTime? DataUltimaVenda { get; set; }
    }
}