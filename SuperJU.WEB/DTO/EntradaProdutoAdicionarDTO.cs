using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperJU.WEB.DTO
{
    public class EntradaProdutoAdicionarDTO
    {
        public int ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorCusto { get; set; }
        public decimal ValorVenda { get; set; }
    }
}