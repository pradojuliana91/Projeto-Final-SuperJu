using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SuperJU.WEB.Client.SuperJUApi.Request;
using SuperJU.WEB.Client.SuperJUApi.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace SuperJU.WEB.Client.SuperJUApi
{
    public static class SuperJUApiClient
    {
        private static readonly string SUPER_JU_API_URL = ConfigurationManager.AppSettings.Get("SUPER_JU_API_URL");

        private static readonly string URL_CLIENTE_PESQUISA = SUPER_JU_API_URL + "/clientes";
        private static readonly string URL_CLIENTE_POR_ID = SUPER_JU_API_URL + "/clientes/{0}";
        private static readonly string URL_CLIENTE_CADASTRAR = SUPER_JU_API_URL + "/clientes";
        private static readonly string URL_CLIENTE_EDITAR = SUPER_JU_API_URL + "/clientes/{0}";

        private static readonly string URL_PRODUTO_PESQUISA = SUPER_JU_API_URL + "/produtos";
        private static readonly string URL_PRODUTO_POR_ID = SUPER_JU_API_URL + "/produtos/{0}";
        private static readonly string URL_PRODUTO_CADASTRAR = SUPER_JU_API_URL + "/produtos";
        private static readonly string URL_PRODUTO_EDITAR = SUPER_JU_API_URL + "/produtos/{0}";
        private static readonly string URL_PRODUTO_RELATORIO = SUPER_JU_API_URL + "/produtos/relatorio";

        private static readonly string URL_ENTRADA_PRODUTO_PESQUISA = SUPER_JU_API_URL + "/produtos/entrada";
        private static readonly string URL_ENTRADA_PRODUTO_POR_ID = SUPER_JU_API_URL + "/produtos/entrada/{0}";
        private static readonly string URL_ENTRADA_PRODUTO_CADASTRAR = SUPER_JU_API_URL + "/produtos/entrada";

        private static readonly string URL_PEDIDO_PESQUISA = SUPER_JU_API_URL + "/pedidos";
        private static readonly string URL_PEDIDO_POR_ID = SUPER_JU_API_URL + "/pedidos/{0}";
        private static readonly string URL_PEDIDO_CADASTRAR = SUPER_JU_API_URL + "/pedidos";
        private static readonly string URL_PEDIDO_RELATORIO = SUPER_JU_API_URL + "/pedidos/relatorio";

        private static readonly string URL_PEDIDO_FORMA_PAGAMENTO_BUSCA_TODOS = SUPER_JU_API_URL + "/pedidos/forma-pagamento";

        public static List<ClienteResponse> ClientePesquisa(int? id, string nome)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            if (id != null)
            {
                values.Add("id", id.ToString());
            }
            if (!string.IsNullOrEmpty(nome))
            {
                values.Add("nome", nome);
            }

            HttpClient httpClient = GetDefaultHttpClient();
            var responseMessage = httpClient.GetAsync(URL_CLIENTE_PESQUISA + GetRequestParam(values)).Result;
            if (responseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            responseMessage.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<List<ClienteResponse>>(responseMessage.Content.ReadAsStringAsync().Result);
        }

        public static ClienteResponse ClienteBuscaPorId(int id)
        {
            HttpClient httpClient = GetDefaultHttpClient();
            var responseMessage = httpClient.GetAsync(string.Format(URL_CLIENTE_POR_ID, id)).Result;
            if (responseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            responseMessage.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<ClienteResponse>(responseMessage.Content.ReadAsStringAsync().Result);
        }

        public static ClienteCadastroResponse ClienteCadastro(ClienteCadstroEditarRequest request)
        {
            using (HttpClient httpClient = GetDefaultHttpClient())
            {
                using (HttpContent content = GetContentJson(request))
                {
                    var responseMessage = httpClient.PostAsync(URL_CLIENTE_CADASTRAR, content).Result;
                    responseMessage.EnsureSuccessStatusCode();
                    return JsonConvert.DeserializeObject<ClienteCadastroResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                }
            }
        }

        public static void ClienteEditar(int id, ClienteCadstroEditarRequest request)
        {
            using (HttpClient httpClient = GetDefaultHttpClient())
            {
                using (HttpContent content = GetContentJson(request))
                {
                    var responseMessage = httpClient.PutAsync(string.Format(URL_CLIENTE_EDITAR, id), content).Result;
                    responseMessage.EnsureSuccessStatusCode();
                }
            }
        }

        public static List<ProdutoResponse> ProdutoPesquisa(int? id, string nome)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            if (id != null)
            {
                values.Add("id", id.ToString());
            }
            if (!string.IsNullOrEmpty(nome))
            {
                values.Add("nome", nome);
            }

            HttpClient httpClient = GetDefaultHttpClient();
            var responseMessage = httpClient.GetAsync(URL_PRODUTO_PESQUISA + GetRequestParam(values)).Result;
            if (responseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            responseMessage.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<List<ProdutoResponse>>(responseMessage.Content.ReadAsStringAsync().Result);
        }

        public static ProdutoResponse ProdutoBuscaPorId(int id)
        {
            HttpClient httpClient = GetDefaultHttpClient();
            var responseMessage = httpClient.GetAsync(string.Format(URL_PRODUTO_POR_ID, id)).Result;
            if (responseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            responseMessage.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<ProdutoResponse>(responseMessage.Content.ReadAsStringAsync().Result);
        }

        public static ProdutoCadastroResponse ProdutoCadastro(ProdutoCadastroRequest request)
        {
            using (HttpClient httpClient = GetDefaultHttpClient())
            {
                using (HttpContent content = GetContentJson(request))
                {
                    var responseMessage = httpClient.PostAsync(URL_PRODUTO_CADASTRAR, GetContentJson(request)).Result;
                    responseMessage.EnsureSuccessStatusCode();
                    return JsonConvert.DeserializeObject<ProdutoCadastroResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                }
            }
        }

        public static void ProdutoEditar(int id, ProdutoEditarRequest request)
        {
            using (HttpClient httpClient = GetDefaultHttpClient())
            {
                using (HttpContent content = GetContentJson(request))
                {
                    var responseMessage = httpClient.PutAsync(string.Format(URL_PRODUTO_EDITAR, id), GetContentJson(request)).Result;
                    responseMessage.EnsureSuccessStatusCode();
                }
            }
        }

        public static List<EntradaProdutoResponse> EntradaProdutoPesquisa(string numeroNota, DateTime? dataInicio, DateTime? dataFim)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(numeroNota))
            {
                values.Add("numeroNota", numeroNota);
            }
            if (dataInicio != null)
            {
                values.Add("dataInicio", dataInicio?.ToString("yyyy-MM-dd"));
            }
            if (dataFim != null)
            {
                values.Add("dataFim", dataFim?.ToString("yyyy-MM-dd"));
            }

            HttpClient httpClient = GetDefaultHttpClient();
            var responseMessage = httpClient.GetAsync(URL_ENTRADA_PRODUTO_PESQUISA + GetRequestParam(values)).Result;
            if (responseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            responseMessage.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<List<EntradaProdutoResponse>>(responseMessage.Content.ReadAsStringAsync().Result);
        }

        public static EntradaProdutoCadastroResponse EntradaProdutoCadastro(EntradaProdutoRequest request)
        {
            using (HttpClient httpClient = GetDefaultHttpClient())
            {
                using (HttpContent content = GetContentJson(request))
                {
                    var responseMessage = httpClient.PostAsync(URL_ENTRADA_PRODUTO_CADASTRAR, content).Result;
                    responseMessage.EnsureSuccessStatusCode();
                    return JsonConvert.DeserializeObject<EntradaProdutoCadastroResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                }
            }
        }

        public static EntradaProdutoResponse EntradaProdutoBuscaPorId(int id)
        {
            HttpClient httpClient = GetDefaultHttpClient();
            var responseMessage = httpClient.GetAsync(string.Format(URL_ENTRADA_PRODUTO_POR_ID, id)).Result;
            if (responseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            responseMessage.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<EntradaProdutoResponse>(responseMessage.Content.ReadAsStringAsync().Result);
        }

        public static List<PedidoResponse> PedidoPesquisa(int? pedidoId, int? clienteId, int? formaPagamentoId, DateTime? dataInicio, DateTime? dataFim)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            if (pedidoId != null)
            {
                values.Add("pedidoId", pedidoId.ToString());
            }
            if (clienteId != null)
            {
                values.Add("clienteId", clienteId.ToString());
            }
            if (formaPagamentoId != null)
            {
                values.Add("formaPagamentoId", formaPagamentoId.ToString());
            }
            if (dataInicio != null)
            {
                values.Add("dataInicio", dataInicio?.ToString("yyyy-MM-dd"));
            }
            if (dataFim != null)
            {
                values.Add("dataFim", dataFim?.ToString("yyyy-MM-dd"));
            }

            HttpClient httpClient = GetDefaultHttpClient();
            var responseMessage = httpClient.GetAsync(URL_PEDIDO_PESQUISA + GetRequestParam(values)).Result;
            if (responseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            responseMessage.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<List<PedidoResponse>>(responseMessage.Content.ReadAsStringAsync().Result);
        }

        public static EntradaProdutoCadastroResponse PedidoCadastro(PedidoRequest request)
        {
            using (HttpClient httpClient = GetDefaultHttpClient())
            {
                using (HttpContent content = GetContentJson(request))
                {
                    var responseMessage = httpClient.PostAsync(URL_PEDIDO_CADASTRAR, content).Result;
                    responseMessage.EnsureSuccessStatusCode();
                    return JsonConvert.DeserializeObject<EntradaProdutoCadastroResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                }
            }
        }

        public static PedidoResponse PedidoBuscaPorId(int id)
        {
            HttpClient httpClient = GetDefaultHttpClient();
            var responseMessage = httpClient.GetAsync(string.Format(URL_PEDIDO_POR_ID, id)).Result;
            if (responseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            responseMessage.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<PedidoResponse>(responseMessage.Content.ReadAsStringAsync().Result);
        }

        public static List<FormaPagamentoResponse> FormaPagamentoBuscaTodos()
        {
            HttpClient httpClient = GetDefaultHttpClient();
            var responseMessage = httpClient.GetAsync(URL_PEDIDO_FORMA_PAGAMENTO_BUSCA_TODOS).Result;
            if (responseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            responseMessage.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<List<FormaPagamentoResponse>>(responseMessage.Content.ReadAsStringAsync().Result);
        }


        public static List<RelEstoqueResponse> RelEstoque(int? produtoId, string produtoNome, int? qtdMaiorQue, int? qtdMenorQue)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            if (produtoId != null)
            {
                values.Add("produtoId", produtoId.ToString());
            }
            if (!string.IsNullOrEmpty(produtoNome))
            {
                values.Add("produtoNome", produtoNome);
            }
            if (qtdMaiorQue != null)
            {
                values.Add("qtdMaiorQue", qtdMaiorQue.ToString());
            }
            if (qtdMenorQue != null)
            {
                values.Add("qtdMenorQue", qtdMenorQue.ToString());
            }

            HttpClient httpClient = GetDefaultHttpClient();
            var responseMessage = httpClient.GetAsync(URL_PRODUTO_RELATORIO + GetRequestParam(values)).Result;
            if (responseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            responseMessage.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<List<RelEstoqueResponse>>(responseMessage.Content.ReadAsStringAsync().Result);
        }

        public static List<RelVendaResponse> RelVenda(int? pedidoId, int? clienteId, int? formaPagamentoId, DateTime? dataInicio, DateTime? dataFim)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            if (pedidoId != null)
            {
                values.Add("pedidoId", pedidoId.ToString());
            }
            if (clienteId != null)
            {
                values.Add("clienteId", clienteId.ToString());
            }
            if (formaPagamentoId != null)
            {
                values.Add("formaPagamentoId", formaPagamentoId.ToString());
            }
            if (dataInicio != null)
            {
                values.Add("dataInicio", dataInicio?.ToString("yyyy-MM-dd"));
            }
            if (dataFim != null)
            {
                values.Add("dataFim", dataFim?.ToString("yyyy-MM-dd"));
            }

            HttpClient httpClient = GetDefaultHttpClient();
            var responseMessage = httpClient.GetAsync(URL_PEDIDO_RELATORIO + GetRequestParam(values)).Result;
            if (responseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            responseMessage.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<List<RelVendaResponse>>(responseMessage.Content.ReadAsStringAsync().Result);
        }

        private static HttpClient GetDefaultHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        private static string GetRequestParam(Dictionary<string, string> values)
        {
            string param = string.Empty;
            if (values != null && values.Count > 0)
            {
                foreach (var item in values)
                {
                    if (string.IsNullOrEmpty(param))
                    {
                        param += "?";
                    }
                    else
                    {
                        param += "&";
                    }
                    param += item.Key + "=" + item.Value;
                }
            }
            return param;
        }

        private static HttpContent GetContentJson(object request)
        {
            return new StringContent(SerializaJson(request), Encoding.UTF8, "application/json");
        }
        private static string SerializaJson(object request)
        {
            return JsonConvert.SerializeObject(request, Formatting.Indented);
        }

    }
}