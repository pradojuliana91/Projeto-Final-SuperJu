using SuperJU.WEB.Client.SuperJUApi;
using SuperJU.WEB.Client.SuperJUApi.Request;
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
    public partial class Editar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["editar-produto-IdProduto"] != null)
                {
                    LimpaTela();
                    int idProduto = (int)Session["editar-produto-IdProduto"];
                    CarregarProduto(idProduto);
                }
                else
                {
                    Response.Redirect("/Web/Produto/Pesquisar", false);
                }
            }
        }

        private void LimpaTela()
        {
            txtNome.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            txtQuantidade.Text = string.Empty;
            txtValorCusto.Text = string.Empty;
            txtValorVenda.Text = string.Empty;
        }

        private void CarregarProduto(int idProduto)
        {
            ProdutoResponse produto = SuperJUApiClient.ProdutoBuscaPorId(idProduto);
            if (produto != null)
            {
                txtNome.Text = produto.Nome;
                txtDescricao.Text = produto.Descricao;
                txtQuantidade.Text = produto.Quantidade.ToString();
                txtValorCusto.Text = string.Format("{0:N}", produto.ValorCusto);
                txtValorVenda.Text = string.Format("{0:N}", produto.ValorVenda);
                btnSalvar.Enabled = true;
            }
            else
            {
                Session.Remove("editar-produto-IdProduto");
                CommonUtils.Alerta(this, "Produto Não Encontrado Para Editar!");
                btnSalvar.Enabled = false;
            }
        }

        protected void BtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidaAtualizar())
                {
                    ProdutoEditarRequest produtoEditarRequest = new ProdutoEditarRequest
                    {
                        Nome = txtNome.Text.Trim(),
                        Descricao = txtDescricao.Text.Trim(),
                        Quantidade = int.Parse(txtQuantidade.Text),
                        ValorCusto = string.IsNullOrEmpty(txtValorCusto.Text) ? 0 : decimal.Parse(txtValorCusto.Text),
                        ValorVenda = decimal.Parse(txtValorVenda.Text)
                    };

                    int idProduto = (int)Session["editar-produto-IdProduto"];
                    SuperJUApiClient.ProdutoEditar(idProduto, produtoEditarRequest);

                    Session.Remove("editar-produto-IdProduto");
                    Session["msg-pesquisa-produto"] = "Produto Atualizado Com Sucesso!";
                    Response.Redirect("/Web/Produto/Pesquisar", false);
                }
            }
            catch (Exception ex)
            {
                CommonUtils.Alerta(this, "Erro ao Atualizar Produto!");
                Console.WriteLine("Erro ao atualizar produto: ", ex);
            }
        }

        private bool ValidaAtualizar()
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
            if (string.IsNullOrEmpty(txtQuantidade.Text))
            {
                CommonUtils.AlertaCampoObrigatorio(this, "Quantidade");
                return false;
            }
            if (!int.TryParse(txtQuantidade.Text, out int quantidade))
            {
                CommonUtils.Alerta(this, "O campo quantidade é inválido!");
                return false;
            }
            if (!(quantidade >= 0))
            {
                CommonUtils.Alerta(this, "O campo quantidade deve ser maior ou igual a 0!");
                return false;
            }

            decimal vlrCusto = 0;
            if (!string.IsNullOrEmpty(txtValorCusto.Text))
            {
                if (!decimal.TryParse(txtValorCusto.Text, out vlrCusto))
                {
                    CommonUtils.Alerta(this, "O campo valor de custo é inválido!");
                    return false;
                }
                if (vlrCusto < 0)
                {
                    CommonUtils.Alerta(this, "O campo valor de custo deve ser maior maior ou igual a 0!");
                    return false;
                }
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
            if (vlrVenda <= vlrCusto)
            {
                CommonUtils.Alerta(this, "O campo valor de venda deve ser maior que o valor de custo!");
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