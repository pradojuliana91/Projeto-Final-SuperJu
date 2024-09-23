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

namespace SuperJU.WEB.Web.Pedido
{
    public partial class Cadastro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaProdutos();
                CarregaCliente();
                CarregaFormaPagamento();
                LimpaTela();
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


        private void LimpaTela()
        {
            Session.Remove("pedido-produto-items");
            dplCliente.SelectedIndex = 0;
            dplFormaPagamento.SelectedIndex = 0;
            txtValorTotalPedido.Text = string.Empty;
            txtValorTotalPedido.Enabled = false;
            gvPedido.Visible = false;
            LimpaAdicionar();
        }

        private void LimpaAdicionar()
        {
            dplProduto.SelectedIndex = 0;
            txtQuantidade.Text = string.Empty;
            txtQuantidadeEmEstoque.Text = string.Empty;
            txtQuantidadeEmEstoque.Enabled = false;
            txtValorVenda.Text = string.Empty;
            txtValorVenda.Enabled = false;
        }

        protected void BtnAdicionar_Click(object sender, EventArgs e)
        {
            if (ValidaAdicionar())
            {
                ProdutoResponse produtoResponse = SuperJUApiClient.ProdutoBuscaPorId(int.Parse(dplProduto.SelectedValue));

                List<PedidoAdicionarDTO> listPedidoItemAux = new List<PedidoAdicionarDTO>();
                if (Session["pedido-produto-items"] != null)
                {
                    listPedidoItemAux = (List<PedidoAdicionarDTO>)Session["pedido-produto-items"];
                }
                listPedidoItemAux.Add(new PedidoAdicionarDTO
                {
                    ProdutoId = produtoResponse.Id,
                    ProdutoNome = produtoResponse.Nome,
                    Quantidade = int.Parse(txtQuantidade.Text),
                    ValorVenda = produtoResponse.ValorVenda,
                    ValorTotal = (int.Parse(txtQuantidade.Text) * produtoResponse.ValorVenda)
                });

                txtValorTotalPedido.Text = string.Format("{0:N}", listPedidoItemAux.Select(x => x.ValorTotal).Sum());

                Session["pedido-produto-items"] = listPedidoItemAux;
                gvPedido.DataSource = listPedidoItemAux;
                gvPedido.DataBind();
                gvPedido.Visible = true;
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
            if ((int.Parse(txtQuantidadeEmEstoque.Text) - quantidade) < 0)
            {
                CommonUtils.Alerta(this, "Quantidade inválida. Estoque insuficiente!");
                return false;
            }
            if (!decimal.TryParse(txtValorVenda.Text, out decimal valorVendal) || valorVendal <= 0)
            {
                CommonUtils.Alerta(this, "Ajustar valor de venda no cadastro. Valor de venda deve ser maior que 0 !");
                return false;
            }
            if (Session["pedido-produto-items"] != null)
            {
                List<PedidoAdicionarDTO> listPedidoItemAux = (List<PedidoAdicionarDTO>)Session["pedido-produto-items"];
                if (listPedidoItemAux != null && listPedidoItemAux.Count > 0 && listPedidoItemAux.Any(s => s.ProdutoId == int.Parse(dplProduto.SelectedValue))) {
                    CommonUtils.Alerta(this, "Não pode adicionar mesmo produto. Exclua e adicione novamente!");
                    return false;
                }
            }
            return true;
        }

        protected void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (Session["pedido-produto-items"] != null)
            {
                Button btn = (Button)sender;
                int idProduto = int.Parse(btn.CommandArgument);

                List<PedidoAdicionarDTO> listPedidoaAux = (List<PedidoAdicionarDTO>)Session["pedido-produto-items"];
                PedidoAdicionarDTO pedidoAdicionar = listPedidoaAux.Find(w => w.ProdutoId == idProduto);
                listPedidoaAux.Remove(pedidoAdicionar);

                if (listPedidoaAux == null || listPedidoaAux.Count == 0)
                {
                    txtValorTotalPedido.Text = string.Empty;
                    gvPedido.Visible = false;
                    Session.Remove("pedido-produto-items");
                }
                else
                {
                    txtValorTotalPedido.Text = string.Format("{0:N}", listPedidoaAux.Select(x => x.ValorTotal).Sum());
                    Session["pedido-produto-items"] = listPedidoaAux;
                    gvPedido.DataSource = listPedidoaAux;
                    gvPedido.DataBind();
                    gvPedido.Visible = true;
                }
            }
        }

        protected void BtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidaCadastro())
                {
                    List<PedidoAdicionarDTO> listPedidoAux = (List<PedidoAdicionarDTO>)Session["pedido-produto-items"];

                    PedidoRequest pedidoRequest = new PedidoRequest();
                    pedidoRequest.ClienteId = int.Parse(dplCliente.SelectedValue);
                    pedidoRequest.FormaPagamentoId = int.Parse(dplFormaPagamento.SelectedValue);
                    pedidoRequest.ValorTotal = listPedidoAux.Select(s => s.ValorTotal).Sum();

                    List<PedidoItemRequest> requestItems = new List<PedidoItemRequest>();
                    listPedidoAux.ForEach(a =>
                    {
                        requestItems.Add(new PedidoItemRequest()
                        {
                            ProdutoId = a.ProdutoId,
                            Quantidade = a.Quantidade,
                            Valor = a.ValorVenda,
                            ValorTotal = a.ValorTotal
                        });
                    });
                    pedidoRequest.Items = requestItems;

                    SuperJUApiClient.PedidoCadastro(pedidoRequest);
                    Session.Add("msg-pesquisa-pedido", "Pedido Criado Com Sucesso!");
                    Response.Redirect("/Web/Pedido/Pesquisar", false);
                }
            }
            catch (Exception ex)
            {
                CommonUtils.Alerta(this, "Erro ao Criar Pedido!");
                Console.WriteLine("Erro ao criar pedido", ex.Message);
            }
        }

        private bool ValidaCadastro()
        {
            if (dplCliente.SelectedIndex == 0)
            {
                CommonUtils.AlertaCampoObrigatorio(this, "Cliente");
                return false;
            }
            if (dplFormaPagamento.SelectedIndex == 0)
            {
                CommonUtils.AlertaCampoObrigatorio(this, "Forma de Pagamento");
                return false;
            }
            if (Session["pedido-produto-items"] == null)
            {
                CommonUtils.Alerta(this, "Não existe produto(s) adicionados no pedido!");
                return false;
            }
            if (((List<PedidoAdicionarDTO>)Session["pedido-produto-items"]).Select(s => s.ValorTotal).Sum() != decimal.Parse(txtValorTotalPedido.Text))
            {
                CommonUtils.Alerta(this, "Valor Total Não Confere. Pedido Adulterado!");
                return false;
            }
            return true;
        }

        protected void BtnVoltar_Click(object sender, EventArgs e)
        {
            Session.Remove("pedido-produto-items");
            Response.Redirect("/Web/Pedido/Pesquisar", false);
        }

        protected void DplProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dplProduto.SelectedIndex > 0)
            {
                ProdutoResponse produtoResponse = SuperJUApiClient.ProdutoBuscaPorId(int.Parse(dplProduto.SelectedValue));
                txtQuantidade.Text = string.Empty;
                txtQuantidadeEmEstoque.Text = produtoResponse.Quantidade.ToString();
                txtValorVenda.Text = string.Format("{0:N}", produtoResponse.ValorVenda);
            }
            else
            {
                txtQuantidade.Text = string.Empty;
                txtQuantidadeEmEstoque.Text = string.Empty;
                txtValorVenda.Text = string.Empty;
            }
        }
    }
}