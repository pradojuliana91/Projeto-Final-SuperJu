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
    public partial class Pesquisar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaCliente();
                CarregaFormaPagamento();
                LimpaTela();
                if (Session["msg-pesquisa-pedido"] != null)
                {
                    CommonUtils.Alerta(this, Session["msg-pesquisa-pedido"].ToString());
                    Session.Remove("msg-pesquisa-pedido");
                }
            }
        }

        private void CarregaFormaPagamento()
        {
            List<FormaPagamentoResponse> formasPagamento = SuperJUApiClient.FormaPagamentoBuscaTodos();
            if (formasPagamento == null)
            {
                formasPagamento = new List<FormaPagamentoResponse>();
            }
            formasPagamento.Insert(0, new FormaPagamentoResponse()
            {
                Id = 0,
                Nome = "Selecione..."
            });
            dplFormaPagamento.DataSource = formasPagamento;
            dplFormaPagamento.DataBind();
            dplFormaPagamento.SelectedIndex = 0;
        }

        private void CarregaCliente()
        {
            List<ClienteResponse> clientes = SuperJUApiClient.ClientePesquisa(null, null);
            if (clientes == null)
            {
                clientes = new List<ClienteResponse>();
            }
            clientes.Insert(0, new ClienteResponse()
            {
                Id = 0,
                Nome = "Selecione..."
            });
            dplCliente.DataSource = clientes;
            dplCliente.DataBind();
            dplCliente.SelectedIndex = 0;
        }

        private void LimpaTela()
        {
            txtCodigo.Text = string.Empty;
            txtDataPedidoInicio.Text = string.Empty;
            txtDataPedidoFim.Text = string.Empty;
            dplCliente.SelectedIndex = 0;
            dplFormaPagamento.SelectedIndex = 0;
            gvPedido.Visible = false;
        }

        protected void BtnNovo_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Web/Pedido/Cadastro", false);
        }

        protected void BtnPesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime? dataPedidoInicio = null;
                if (!string.IsNullOrEmpty(txtDataPedidoInicio.Text) && !DateTime.TryParse(txtDataPedidoInicio.Text, out _))
                {
                    CommonUtils.Alerta(this, "O Campo Data Pedido Início é inválida!");
                    return;
                }
                else if (!string.IsNullOrEmpty(txtDataPedidoInicio.Text))
                {
                    dataPedidoInicio = DateTime.Parse(txtDataPedidoInicio.Text).Date;
                }

                DateTime? dataPedidoFim = null;
                if (!string.IsNullOrEmpty(txtDataPedidoFim.Text) && !DateTime.TryParse(txtDataPedidoFim.Text, out _))
                {
                    CommonUtils.Alerta(this, "O Campo Data Pedido Final é inválida!");
                    return;
                }
                else if (!string.IsNullOrEmpty(txtDataPedidoFim.Text))
                {
                    dataPedidoFim = DateTime.Parse(txtDataPedidoFim.Text).Date;
                }

                if (dataPedidoInicio != null && dataPedidoFim != null && dataPedidoInicio > dataPedidoFim)
                {
                    CommonUtils.Alerta(this, "O Campo Data Entrada Inicial deve ser menor ou igual a Data Entrada Final!");
                    return;
                }

                int? idCliente = null;
                if (dplCliente.SelectedIndex > 0)
                {
                    idCliente = int.Parse(dplCliente.SelectedValue);
                }
                int? idFormaPagamento = null;
                if (dplFormaPagamento.SelectedIndex > 0)
                {
                    idFormaPagamento = int.Parse(dplFormaPagamento.SelectedValue);
                }
                int? idPedido = null;
                if (!string.IsNullOrEmpty(txtCodigo.Text))
                {
                    idPedido = int.Parse(txtCodigo.Text);
                }

                List<PedidoResponse> pedidos = SuperJUApiClient.PedidoPesquisa(idPedido, idCliente, idFormaPagamento, dataPedidoInicio, dataPedidoFim);

                if (pedidos != null && pedidos.Count > 0)
                {
                    gvPedido.Visible = true;
                    gvPedido.DataSource = pedidos;
                    gvPedido.DataBind();
                }
                else
                {
                    gvPedido.Visible = false;
                    CommonUtils.Alerta(this, "Não foi Encontrado Pedios com o(s) filtro(s) informado(s)!");
                }
            }
            catch (Exception ex)
            {
                CommonUtils.Alerta(this, "Erro ao Tentar Pesquisar Pedidos!");
                Console.WriteLine("Erro ao pesquisar pedidos - " + ex.Message);
            }
        }

        protected void BtnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                int idPedido = int.Parse(btn.CommandArgument);
                Session["visualizar-pedido-IdPedido"] = idPedido;
                Response.Redirect("/Web/Pedido/Visualizar", false);
            }
            catch (Exception ex)
            {
                CommonUtils.Alerta(this, "Erro ao Tentar Visualizar Pedido!");
                Console.WriteLine("Erro ao visualizar pedido - " + ex.Message);
            }
        }
    }
}