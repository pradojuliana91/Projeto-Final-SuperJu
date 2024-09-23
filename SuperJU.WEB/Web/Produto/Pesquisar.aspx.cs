using SuperJU.WEB.Client.SuperJUApi;
using SuperJU.WEB.Client.SuperJUApi.Response;
using SuperJU.WEB.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuperJU.WEB.Web.Produto
{
    public partial class Pesquisar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LimpaTela();
                if (Session["msg-pesquisa-produto"] != null)
                {
                    CommonUtils.Alerta(this, Session["msg-pesquisa-produto"].ToString());
                    Session.Remove("msg-pesquisa-produto");
                }
            }
        }

        private void LimpaTela()
        {
            txtCodigo.Text = string.Empty;
            txtProduto.Text = string.Empty;
            gvProduto.Visible = false;
        }

        protected void BtnNovo_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Web/Produto/Cadastro", false);
        }

        protected void BtnPesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                int? idProduto = null;
                if (!string.IsNullOrEmpty(txtCodigo.Text) && int.TryParse(txtCodigo.Text, out int id))
                {
                    idProduto = id;
                }

                List<ProdutoResponse> produtos = SuperJUApiClient.ProdutoPesquisa(idProduto, txtProduto.Text);

                if (produtos != null && produtos.Count > 0)
                {
                    gvProduto.Visible = true;
                    gvProduto.DataSource = produtos;
                    gvProduto.DataBind();
                }
                else
                {
                    gvProduto.Visible = false;
                    CommonUtils.Alerta(this, "Não foi Encontrado Produto(s) com o(s) filtro(s) informado(s)!");
                }
            }
            catch (Exception ex)
            {
                CommonUtils.Alerta(this, "Erro ao Tentar Pesquisar Produtos!");
                Console.WriteLine("Erro ao pesquisar produto - " + ex.Message);
            }
        }

        protected void BtnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                int idProduto = int.Parse(btn.CommandArgument);
                Session["editar-produto-IdProduto"] = idProduto;
                Response.Redirect("/Web/Produto/Editar", false);
            }
            catch (Exception ex)
            {
                CommonUtils.Alerta(this, "Erro ao Tentar Editar Produto!");
                Console.WriteLine("Erro ao editar produto - " + ex.Message);
            }
        }
    }
}