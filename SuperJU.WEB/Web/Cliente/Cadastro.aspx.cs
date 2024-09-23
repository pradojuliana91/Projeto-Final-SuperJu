using SuperJU.WEB.Client.SuperJUApi;
using SuperJU.WEB.Client.SuperJUApi.Request;
using SuperJU.WEB.Utils;
using System;

namespace SuperJU.WEB.Web.Cliente
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
            txtCPF.Text = string.Empty;
            txtDataNascimento.Text = string.Empty;
            txtTelefone.Text = string.Empty;
            txtEndereco.Text = string.Empty;
            txtComplemento.Text = string.Empty;
            txtCEP.Text = string.Empty;
            txtBairro.Text = string.Empty;
            txtCidade.Text = string.Empty;
            dplEstado.SelectedIndex = 0;
        }

        protected void BtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidaCadastro())
                {
                    ClienteCadstroEditarRequest clienteCadstroEditarRequest = new ClienteCadstroEditarRequest
                    {
                        Nome = txtNome.Text.Trim(),
                        CPF = txtCPF.Text.Replace(".", "").Replace("-", "").Replace("_", "").Trim(),
                        DataNascimento = DateTime.Parse(txtDataNascimento.Text).Date,
                        Telefone = txtTelefone.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Replace("_", "").Trim(),
                        CEP = txtCEP.Text.Replace("-", "").Replace("_", "").Trim(),
                        Endereco = txtEndereco.Text.Trim(),
                        Complemento = (string.IsNullOrEmpty(txtComplemento.Text)) ? null : txtComplemento.Text.Trim(),
                        Bairro = txtBairro.Text.Trim(),
                        Cidade = txtCidade.Text.Trim(),
                        Estado = dplEstado.SelectedValue                       
                    };

                    SuperJUApiClient.ClienteCadastro(clienteCadstroEditarRequest);
                    Session.Add("msg-pesquisa-cliente", "Cliente Cadastrado Com Sucesso!");
                    Response.Redirect("/Web/Cliente/Pesquisar", false);
                }                             
            }
            catch (Exception ex)
            {
                CommonUtils.Alerta(this, "Erro ao Cadastrar Cliente!");
                Console.WriteLine("Erro ao cadastrar cliente", ex.Message);
            }
        }

        private bool ValidaCadastro()
        {
            if (string.IsNullOrEmpty(txtNome.Text))
            {
                CommonUtils.AlertaCampoObrigatorio(this, "Nome");
                return false;
            }
            if (string.IsNullOrEmpty(txtCPF.Text))
            {
                CommonUtils.AlertaCampoObrigatorio(this, "CPF");
                return false;
            }
            if (txtCPF.Text.Replace(".","").Replace("-","").Replace("_", "").Trim().Length != 11)
            {
                CommonUtils.Alerta(this, "O campo CPF deve conter 11 dígitos!");
                return false;
            }
            if (string.IsNullOrEmpty(txtDataNascimento.Text))
            {
                CommonUtils.AlertaCampoObrigatorio(this, "Data Nascimento");
                return false;
            }
            if (!DateTime.TryParse(txtDataNascimento.Text, out DateTime dataNascimento))
            {
                CommonUtils.Alerta(this, "O Campo Data Nascimento é inválido!");
                return false;
            }
            if (dataNascimento.Date > new DateTime(2018, 12, 31).Date)
            {
                CommonUtils.Alerta(this, "O Campo Data Nascimento deve ser menor ou igual ao ano de 2018!");
                return false;
            }
            if (string.IsNullOrEmpty(txtTelefone.Text))
            {
                CommonUtils.AlertaCampoObrigatorio(this, "Telefone");
                return false;
            }
            if (txtTelefone.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Replace("_", "").Trim().Length != 11)
            {
                CommonUtils.Alerta(this, "O campo Telefone deve conter 11 dígitos!");
                return false;
            }
            if (string.IsNullOrEmpty(txtCEP.Text))
            {
                CommonUtils.AlertaCampoObrigatorio(this, "CEP");
                return false;
            }
            if (txtCEP.Text.Replace("-", "").Replace("_", "").Trim().Length != 8)
            {
                CommonUtils.Alerta(this, "O campo CEP deve conter 8 dígitos!");
                return false;
            }
            if (string.IsNullOrEmpty(txtEndereco.Text))
            {
                CommonUtils.AlertaCampoObrigatorio(this, "Endereço");
                return false;
            }
            if (string.IsNullOrEmpty(txtBairro.Text))
            {
                CommonUtils.AlertaCampoObrigatorio(this, "Bairro");
                return false;
            }
            if (string.IsNullOrEmpty(txtCidade.Text))
            {
                CommonUtils.AlertaCampoObrigatorio(this, "Cidade");
                return false;
            }
            if (dplEstado.SelectedIndex == -1)
            {
                CommonUtils.AlertaCampoObrigatorio(this, "Estado");
                return false;
            }
            return true;
        }

        protected void BtnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Web/Cliente/Pesquisar", false);
        }
    }
}