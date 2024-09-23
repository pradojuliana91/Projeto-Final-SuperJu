using SuperJU.WEB.Client.SuperJUApi;
using SuperJU.WEB.Client.SuperJUApi.Request;
using SuperJU.WEB.Client.SuperJUApi.Response;
using SuperJU.WEB.DTO;
using SuperJU.WEB.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuperJU.WEB.Web.Produto.Entrada
{
    public partial class Cadastro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaProdutos();
                LimpaTela();
            }
        }

        private void LimpaTela()
        {
            Session.Remove("entrada-produto-items");
            txtNumeroNota.Text = string.Empty;
            gvEntrada.Visible = false;
            LimpaAdicionar();
        }

        private void LimpaAdicionar()
        {
            txtQuantidade.Text = string.Empty;
            txtValorCusto.Text = string.Empty;
            txtValorVenda.Text = string.Empty;
            dplProduto.SelectedIndex = 0;
        }

        private void CarregaProdutos()
        {
            List<ProdutoResponse> produtos = SuperJUApiClient.ProdutoPesquisa(null, null);
            if (produtos == null)
            {
                produtos = new List<ProdutoResponse>();
            }
            produtos.Insert(0, new ProdutoResponse
            {
                Id = 0,
                Nome = "Selecione..."
            });
            dplProduto.DataSource = produtos;
            dplProduto.DataBind();
            dplProduto.SelectedIndex = 0;
        }

        protected void BtnAdicionar_Click(object sender, EventArgs e)
        {
            if (ValidaAdicionar())
            {
                List<EntradaProdutoAdicionarDTO> listEntradaAux = new List<EntradaProdutoAdicionarDTO>();
                if (Session["entrada-produto-items"] != null)
                {
                    listEntradaAux = (List<EntradaProdutoAdicionarDTO>)Session["entrada-produto-items"];                    
                }
                listEntradaAux.Add(new EntradaProdutoAdicionarDTO
                {
                    ProdutoId = int.Parse(dplProduto.SelectedValue),
                    ProdutoNome = dplProduto.SelectedItem.Text,
                    Quantidade = int.Parse(txtQuantidade.Text),
                    ValorCusto = string.IsNullOrEmpty(txtValorCusto.Text) ? 0 : decimal.Parse(txtValorCusto.Text),
                    ValorVenda = decimal.Parse(txtValorVenda.Text)
                });

                Session["entrada-produto-items"] = listEntradaAux;
                gvEntrada.DataSource = listEntradaAux;
                gvEntrada.DataBind();
                gvEntrada.Visible = true;
                LimpaAdicionar();
            }
        }

        private bool ValidaAdicionar()
        {
            if (dplProduto.SelectedIndex == 0)
            {
                CommonUtils.Alerta(this, "Selecione produto para adicionar!");
                return false;
            }
            if (string.IsNullOrEmpty(txtQuantidade.Text))
            {
                CommonUtils.AlertaCampoObrigatorio(this, "Quantidade");
                return false;
            }
            if (!int.TryParse(txtQuantidade.Text, out int quantidade) || quantidade <= 0)
            {
                CommonUtils.Alerta(this, "O campo quantidade deve ser maior que 0!");
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
                    CommonUtils.Alerta(this, "O campo valor de custo deve ser maior ou igual a 0!");
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
            if (vlrCusto >= vlrVenda)
            {
                CommonUtils.Alerta(this, "O campo valor de venda deve ser maior que o valor de custo!");
                return false;
            }
            List<EntradaProdutoAdicionarDTO> listEntradaAux = new List<EntradaProdutoAdicionarDTO>();
            if (Session["entrada-produto-items"] != null)
            {
                listEntradaAux = (List<EntradaProdutoAdicionarDTO>)Session["entrada-produto-items"];
                if (listEntradaAux != null && listEntradaAux.Count > 0 && listEntradaAux.Any(s => s.ProdutoId == int.Parse(dplProduto.SelectedValue)))
                {
                    CommonUtils.Alerta(this, "Não pode adicionar mesmo produto. Exclua e adicione novamente!");
                    return false;
                }
            }
            return true;
        }

        protected void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (Session["entrada-produto-items"] != null)
            {
                Button btn = (Button)sender;
                int idProduto = int.Parse(btn.CommandArgument);

                List<EntradaProdutoAdicionarDTO> listEntradaAux = (List<EntradaProdutoAdicionarDTO>)Session["entrada-produto-items"];
                EntradaProdutoAdicionarDTO entradaProdutoAdicionarDTO = listEntradaAux.Find(w => w.ProdutoId == idProduto);
                listEntradaAux.Remove(entradaProdutoAdicionarDTO);

                if (listEntradaAux == null || listEntradaAux.Count == 0)
                {
                    Session.Remove("entrada-produto-items");
                    gvEntrada.Visible = false;
                }
                else
                {
                    Session["entrada-produto-items"] = listEntradaAux;
                    gvEntrada.DataSource = listEntradaAux;
                    gvEntrada.DataBind();
                    gvEntrada.Visible = true;
                }
            }
        }


        protected void BtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidaCadastro())
                {
                    List<EntradaProdutoAdicionarDTO> listEntradaAux = (List<EntradaProdutoAdicionarDTO>)Session["entrada-produto-items"];

                    EntradaProdutoRequest request = new EntradaProdutoRequest();
                    request.NumeroNota = txtNumeroNota.Text;
                    List<EntradaProdutoItemRequest> requestItems = new List<EntradaProdutoItemRequest>();
                    listEntradaAux.ForEach(a =>
                    {
                        requestItems.Add(new EntradaProdutoItemRequest()
                        {
                            ProdutoId = a.ProdutoId,
                            Quantidade = a.Quantidade,
                            ValorCusto = a.ValorCusto,
                            ValorVenda = a.ValorVenda
                        });
                    });
                    request.Produtos = requestItems;

                    SuperJUApiClient.EntradaProdutoCadastro(request);
                    Session.Add("msg-pesquisa-entrada", "Entrada de Produtos Realizada Com Sucesso!");
                    Response.Redirect("/Web/Produto/Entrada/Pesquisar", false);
                }
            }
            catch (Exception ex)
            {
                CommonUtils.Alerta(this, "Erro ao Cadastrar Entrada de Produto!");
                Console.WriteLine("Erro ao cadastrar entrada de produto", ex.Message);
            }
        }

        private bool ValidaCadastro()
        {
            if (string.IsNullOrEmpty(txtNumeroNota.Text))
            {
                CommonUtils.AlertaCampoObrigatorio(this, "Nº Nota");
                return false;
            }
            if (Session["entrada-produto-items"] == null)
            {
                CommonUtils.Alerta(this, "Não existe produtos adicionados na entrada de produtos!");
                return false;
            }
            return true;
        }

        protected void BtnVoltar_Click(object sender, EventArgs e)
        {
            Session.Remove("entrada-produto-items");
            Response.Redirect("/Web/Produto/Entrada/Pesquisar", false);
        }

        protected void DplProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQuantidade.Text = string.Empty;
            if (dplProduto.SelectedIndex > 0)
            {
                ProdutoResponse produtoResponse = SuperJUApiClient.ProdutoBuscaPorId(int.Parse(dplProduto.SelectedValue));
                txtQuantidade.Text = string.Empty;
                txtValorCusto.Text = string.Format("{0:N}", produtoResponse.ValorCusto);
                txtValorVenda.Text = string.Format("{0:N}", produtoResponse.ValorVenda);
            }
            else
            {
                txtValorCusto.Text = string.Empty;
                txtValorVenda.Text = string.Empty;
            }
        }

    }
}