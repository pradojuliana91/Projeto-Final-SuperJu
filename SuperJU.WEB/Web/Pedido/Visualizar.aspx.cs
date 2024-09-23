using SuperJU.WEB.Client.SuperJUApi;
using SuperJU.WEB.Client.SuperJUApi.Response;
using SuperJU.WEB.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuperJU.WEB.Web.Pedido
{
    public partial class Visualizar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["visualizar-pedido-IdPedido"] != null)
                {
                    LimpaTela();
                    int idPedido = (int)Session["visualizar-pedido-IdPedido"];
                    CarregaTela(idPedido);
                }
                else
                {
                    Response.Redirect("/Web/Pedido/Pesquisar", false);
                }
            }
        }

        private void CarregaTela(int idPedido)
        {
            PedidoResponse pedidoResponse = SuperJUApiClient.PedidoBuscaPorId(idPedido);
            if (pedidoResponse != null && pedidoResponse.Items != null && pedidoResponse.Items.Count > 0)
            {
                txtCliente.Text = pedidoResponse.ClienteNome;
                txtFormaPagamento.Text = pedidoResponse.FormaPagamentoNome;
                txtDataPedido.Text = pedidoResponse.DataPedido.ToString("dd/MM/yyyy");
                txtValorTotalPedido.Text = string.Format("{0:N}", pedidoResponse.ValorTotal);
                gvPedido.Visible = true;
                gvPedido.DataSource = pedidoResponse.Items;
                gvPedido.DataBind();
            }
            else
            {
                Session.Remove("visualizar-pedido-IdPedido");
                CommonUtils.Alerta(this, "Pedido Não Encontrado Para Visualizar!");
            }
        }

        private void LimpaTela()
        {
            txtCliente.Text = string.Empty;
            txtCliente.Enabled = false;
            txtFormaPagamento.Text = string.Empty;
            txtFormaPagamento.Enabled = false;
            txtDataPedido.Text = string.Empty;
            txtDataPedido.Enabled = false;
            txtValorTotalPedido.Text = string.Empty;
            txtValorTotalPedido.Enabled = false;
            gvPedido.Visible = false;
        }

        protected void BtnVoltar_Click(object sender, EventArgs e)
        {
            Session.Remove("visualizar-pedido-IdPedido");
            Response.Redirect("/Web/Pedido/Pesquisar", false);
        }
    }
}