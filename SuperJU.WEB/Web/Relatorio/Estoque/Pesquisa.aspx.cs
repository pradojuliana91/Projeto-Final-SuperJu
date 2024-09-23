using SuperJU.WEB.Client.SuperJUApi;
using SuperJU.WEB.Client.SuperJUApi.Response;
using SuperJU.WEB.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuperJU.WEB.Web.Relatorio.Estoque
{
    public partial class Pesquisa : System.Web.UI.Page
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
            txtCodigo.Text = string.Empty;
            txtProduto.Text = string.Empty;
            txtQuantidadeMenorQue.Text = string.Empty;
            txtQuantidadeMaiorQue.Text = string.Empty;
            gvRel.Visible = false;
        }

        protected void BtnPesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                int? idProduto = null;
                if (!string.IsNullOrEmpty(txtCodigo.Text) && int.TryParse(txtCodigo.Text, out int idProdutoAux))
                {
                    idProduto = idProdutoAux;
                }
                int? qtdMaiorQue = null;
                if (!string.IsNullOrEmpty(txtQuantidadeMaiorQue.Text) && int.TryParse(txtQuantidadeMaiorQue.Text, out int qtdMaiorQueAux))
                {
                    qtdMaiorQue = qtdMaiorQueAux;
                }
                int? qtdMenorQue = null;
                if (!string.IsNullOrEmpty(txtQuantidadeMenorQue.Text) && int.TryParse(txtQuantidadeMenorQue.Text, out int qtdMenorQueAux))
                {
                    qtdMenorQue = qtdMenorQueAux;
                }

                List<RelEstoqueResponse> relEstoque = SuperJUApiClient.RelEstoque(idProduto, txtProduto.Text, qtdMaiorQue, qtdMenorQue);

                if (relEstoque != null && relEstoque.Count > 0)
                {
                    gvRel.Visible = true;
                    gvRel.DataSource = relEstoque;
                    gvRel.DataBind();
                }
                else
                {
                    gvRel.Visible = false;
                    CommonUtils.Alerta(this, "Não foi Encontrado Registro(s) Para o Relatório de Estoque com o(s) filtro(s) informado(s)!");
                }
            }
            catch (Exception ex)
            {
                CommonUtils.Alerta(this, "Erro ao Tentar Gerar Relatório de Estoque!");
                Console.WriteLine("Erro ao gerar relatorio de estoque - " + ex.Message);
            }
        }

    }
}