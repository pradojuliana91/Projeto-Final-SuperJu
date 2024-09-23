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
    public partial class Pesquisar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LimpaTela();
                if (Session["msg-pesquisa-entrada"] != null)
                {
                    CommonUtils.Alerta(this, Session["msg-pesquisa-entrada"].ToString());
                    Session.Remove("msg-pesquisa-entrada");
                }
            }
        }

        private void LimpaTela()
        {
            txtNumeroNota.Text = string.Empty;
            txtDataEntradaInicio.Text = string.Empty;
            txtDataEntradaFim.Text = string.Empty;
            gvEntrada.Visible = false;
        }

        protected void BtnNovo_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Web/Produto/Entrada/Cadastro", false);
        }

        protected void BtnPesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime? dataEntradaInicio = null;
                if (!string.IsNullOrEmpty(txtDataEntradaInicio.Text) && !DateTime.TryParse(txtDataEntradaInicio.Text, out _))
                {
                    CommonUtils.Alerta(this, "O Campo Data Entrada Início é inválida!");
                    return;
                }
                else if (!string.IsNullOrEmpty(txtDataEntradaInicio.Text))
                {
                    dataEntradaInicio = DateTime.Parse(txtDataEntradaInicio.Text).Date;
                }

                DateTime? dataEntradaFim = null;
                if (!string.IsNullOrEmpty(txtDataEntradaFim.Text) && !DateTime.TryParse(txtDataEntradaFim.Text, out _))
                {
                    CommonUtils.Alerta(this, "O Campo Data Final Início é inválida!");
                    return;
                }
                else if (!string.IsNullOrEmpty(txtDataEntradaFim.Text))
                {
                    dataEntradaFim = DateTime.Parse(txtDataEntradaFim.Text).Date;
                }

                if (dataEntradaInicio != null && dataEntradaFim != null && dataEntradaInicio > dataEntradaFim)
                {
                    CommonUtils.Alerta(this, "O Campo Data Entrada Inicial deve ser menor ou igual a Data Entrada Final!");
                    return;
                }

                List<EntradaProdutoResponse> entradas = SuperJUApiClient.EntradaProdutoPesquisa(txtNumeroNota.Text, dataEntradaInicio, dataEntradaFim);

                if (entradas != null && entradas.Count > 0)
                {
                    gvEntrada.Visible = true;
                    gvEntrada.DataSource = entradas;
                    gvEntrada.DataBind();
                }
                else
                {
                    gvEntrada.Visible = false;
                    CommonUtils.Alerta(this, "Não foi Encontrado Entrada de Produtos com o(s) filtro(s) informado(s)!");
                }
            }
            catch (Exception ex)
            {
                CommonUtils.Alerta(this, "Erro ao Tentar Pesquisar Entrada de Produtos!");
                Console.WriteLine("Erro ao pesquisar entrada de produtos - " + ex.Message);
            }
        }

        protected void BtnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                int idEntrada = int.Parse(btn.CommandArgument);
                Session["visualizar-entrada-IdEntrada"] = idEntrada;
                Response.Redirect("/Web/Produto/Entrada/Visualizar", false);
            }
            catch (Exception ex)
            {
                CommonUtils.Alerta(this, "Erro ao Tentar Visualizar Entrada de Produtos!");
                Console.WriteLine("Erro ao visualzar entrada de produtos - " + ex.Message);
            }
        }
    }
}