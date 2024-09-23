using SuperJU.WEB.Client.SuperJUApi;
using SuperJU.WEB.Client.SuperJUApi.Response;
using SuperJU.WEB.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuperJU.WEB.Web.Produto.Entrada
{
    public partial class Visualizar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["visualizar-entrada-IdEntrada"] != null)
                {
                    LimpaTela();
                    int idEntrada = (int)Session["visualizar-entrada-IdEntrada"];
                    CarregaTela(idEntrada);
                }
                else
                {
                    Response.Redirect("/Web/Produto/Entrada/Pesquisar", false);
                }                
            }
        }

        private void CarregaTela(int idEntrada)
        {
            EntradaProdutoResponse entradaProdutoResponse = SuperJUApiClient.EntradaProdutoBuscaPorId(idEntrada);
            if (entradaProdutoResponse != null && entradaProdutoResponse.Produtos != null && entradaProdutoResponse.Produtos.Count > 0)
            {                
                txtNumeroNota.Text = entradaProdutoResponse.NumeroNota;
                txtDataEntrada.Text = entradaProdutoResponse.DataEntrada.ToString("dd/MM/yyyy");
                gvEntrada.Visible = true;
                gvEntrada.DataSource = entradaProdutoResponse.Produtos;
                gvEntrada.DataBind();
            }
            else
            {
                Session.Remove("visualizar-entrada-IdEntrada");
                CommonUtils.Alerta(this, "Entrada de Produtos Não Encontrado Para Visualizar!");               
            }
        }

        private void LimpaTela()
        {
            txtNumeroNota.Text = string.Empty;
            txtNumeroNota.Enabled = false;            
            txtDataEntrada.Text = string.Empty;
            txtDataEntrada.Enabled = false;
            gvEntrada.Visible = false;
        }

        protected void BtnVoltar_Click(object sender, EventArgs e)
        {
            Session.Remove("visualizar-entrada-IdEntrada");
            Response.Redirect("/Web/Produto/Entrada/Pesquisar", false);
        }
    }
}