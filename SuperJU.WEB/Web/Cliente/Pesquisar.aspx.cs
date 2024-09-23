using SuperJU.WEB.Client.SuperJUApi;
using SuperJU.WEB.Client.SuperJUApi.Response;
using SuperJU.WEB.Utils;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SuperJU.WEB.Web.Cliente
{
    public partial class Pesquisar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LimpaTela();
                if (Session["msg-pesquisa-cliente"] != null)
                {
                    CommonUtils.Alerta(this, Session["msg-pesquisa-cliente"].ToString());
                    Session.Remove("msg-pesquisa-cliente");
                }
            }
        }

        private void LimpaTela()
        {
            txtCodigo.Text = string.Empty;
            txtCliente.Text = string.Empty;
            gvCliente.Visible = false;
        }

        protected void BtnNovo_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Web/Cliente/Cadastro", false);
        }

        protected void BtnPesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                int? idCliente = null;
                if (!string.IsNullOrEmpty(txtCodigo.Text) && int.TryParse(txtCodigo.Text, out int id))
                {
                    idCliente = id;
                }

                List<ClienteResponse> clientes = SuperJUApiClient.ClientePesquisa(idCliente, txtCliente.Text);

                if (clientes != null && clientes.Count > 0)
                {
                    gvCliente.Visible = true;
                    gvCliente.DataSource = clientes;
                    gvCliente.DataBind();
                }
                else
                {
                    gvCliente.Visible = false;
                    CommonUtils.Alerta(this, "Não foi Encontrado Cliente(s) com o(s) filtro(s) informado(s)!");
                }
            }
            catch (Exception ex)
            {
                CommonUtils.Alerta(this, "Erro ao Tentar Pesquisar Clientes!");
                Console.WriteLine("Erro ao pesquisar cliente - " + ex.Message);
            }
        }

        protected void BtnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                int idCliente = int.Parse(btn.CommandArgument);
                Session["editar-cliente-IdCliente"] = idCliente;
                Response.Redirect("/Web/Cliente/Editar", false);
            }
            catch (Exception ex)
            {
                CommonUtils.Alerta(this, "Erro ao Tentar Editar Cliente!");
                Console.WriteLine("Erro ao editar cliente - " + ex.Message);
            }
        }
    }
}