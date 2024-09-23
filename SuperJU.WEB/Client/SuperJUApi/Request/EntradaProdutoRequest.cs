using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperJU.WEB.Client.SuperJUApi.Request
{
    public class EntradaProdutoRequest
    {
        public string NumeroNota { get; set; }
        public List<EntradaProdutoItemRequest> Produtos { get; set; }
    }
}