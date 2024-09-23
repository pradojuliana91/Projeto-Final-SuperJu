using SuperJU.WEB.Client.SuperJUApi;
using SuperJU.WEB.Client.SuperJUApi.Request;
using SuperJU.WEB.Utils;
using System;

namespace SuperJU.WEB.Web.Produto
{
    public partial class Cadastro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LimpaTela();
            }
        }

        private void LimpaTela()
        {
            txtNome.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            txtValorVenda.Text = string.Empty;
        }

        protected void BtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidaCadastro())
                {
                    ProdutoCadastroRequest produtoCadastroRequest = new ProdutoCadastroRequest
                    {
                        Nome = txtNome.Text,
                        Descricao = txtDescricao.Text,
                        ValorVenda = decimal.Parse(txtValorVenda.Text)
                    };

                    SuperJUApiClient.ProdutoCadastro(produtoCadastroRequest);
                    Session.Add("msg-pesquisa-produto", "Produto Cadastrado Com Sucesso!");
                    Response.Redirect("/Web/Produto/Pesquisar", false);
                }
            }
            catch (Exception ex)
            {
                CommonUtils.Alerta(this, "Erro ao Cadastrar Produto!");
                Console.WriteLine("Erro ao cadastrar produto", ex.Message);
            }
        }

        private bool ValidaCadastro()
        {
            if (string.IsNullOrEmpty(txtNome.Text))
            {
                CommonUtils.AlertaCampoObrigatorio(this, "Nome");
                return false;
            }
            if (string.IsNullOrEmpty(txtDescricao.Text))
            {
                CommonUtils.AlertaCampoObrigatorio(this, "Descrição");
                return false;
            }
            if (string.IsNullOrEmpty(txtValorVenda.Text))
            {
                CommonUtils.AlertaCampoObrigatorio(this, "Valor Venda");
                return false;
            }
            if (!decimal.TryParse(txtValorVenda.Text, out decimal vlrVenda))
            {
                CommonUtils.Alerta(this, "O campo valor de venda é inválido!");
                return false;
            }
            if (vlrVenda <= 0)
            {
                CommonUtils.Alerta(this, "O campo valor de venda deve ser maior que 0!");
                return false;
            }
            return true;
        }

        protected void BtnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Web/Produto/Pesquisar", false);
        }
    }
}