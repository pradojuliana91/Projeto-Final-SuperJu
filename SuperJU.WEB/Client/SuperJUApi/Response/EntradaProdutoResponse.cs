using System;
using System.Collections.Generic;

namespace SuperJU.WEB.Client.SuperJUApi.Response
{
    public class EntradaProdutoResponse
    {
        public int Id { get; set; }
        public string NumeroNota { get; set; }
        public DateTime DataEntrada { get; set; }
        public List<EntradaProdutoItemResponse> Produtos { get; set; }
    }
}
