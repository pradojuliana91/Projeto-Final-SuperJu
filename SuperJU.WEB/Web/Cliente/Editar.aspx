<%@ Page Title="Editar Cliente" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Editar.aspx.cs" Inherits="SuperJU.WEB.Web.Cliente.Editar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(document).ready(function () {
            $('#txtCPF').inputmask('999.999.999-99');
            $('#txtDataNascimento').inputmask('99/99/9999');
            $('#txtTelefone').inputmask('(99) 99999-9999');
            $('#txtCEP').inputmask('99999-999');
        })
    </script>
    <div class="container">
        <div>
            <h1>Editar Cliente</h1>
        </div>
        <div class="row g-3">
            <div class="col-md-3">
                <label for="txtNome" class="form-label">Nome *</label>
                <asp:TextBox ID="txtNome" class="form-control" runat="server" MaxLength="50"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label for="txtCPF" class="form-label">CPF *</label>
                <asp:TextBox ID="txtCPF" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label for="txtDataNascimento" class="form-label">Data Nascimento *</label>
                <asp:TextBox ID="txtDataNascimento" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label for="txtTelefone" class="form-label">Telefone *</label>
                <asp:TextBox ID="txtTelefone" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label for="txtCEP" class="form-label">CEP *</label>
                <asp:TextBox ID="txtCEP" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label for="txtEndereco" class="form-label">Endereço *</label>
                <asp:TextBox ID="txtEndereco" class="form-control" runat="server" MaxLength="50"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label for="txtComplemento" class="form-label">Complemento</label>
                <asp:TextBox ID="txtComplemento" class="form-control" runat="server" MaxLength="50"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label for="txtBairro" class="form-label">Bairro *</label>
                <asp:TextBox ID="txtBairro" class="form-control" runat="server" MaxLength="30"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label for="txtCidade" class="form-label">Cidade *</label>
                <asp:TextBox ID="txtCidade" class="form-control" runat="server" MaxLength="30"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label for="dplEstado" class="form-label">Estado *</label>
                <asp:DropDownList ID="dplEstado" runat="Server" CssClass="form-select">
                    <asp:ListItem Text="Selecione..." Value="0" />
                    <asp:ListItem Text="Acre" Value="AC" />
                    <asp:ListItem Text="Alagoas" Value="AL" />
                    <asp:ListItem Text="Amapá" Value="AP" />
                    <asp:ListItem Text="Amazonas" Value="AM" />
                    <asp:ListItem Text="Bahia" Value="BA" />
                    <asp:ListItem Text="Ceará" Value="CE" />
                    <asp:ListItem Text="Espírito Santo" Value="ES" />
                    <asp:ListItem Text="Goiás" Value="GO" />
                    <asp:ListItem Text="Maranhão" Value="MA" />
                    <asp:ListItem Text="Mato Grosso" Value="MT" />
                    <asp:ListItem Text="Mato Grosso do Sul" Value="MS" />
                    <asp:ListItem Text="Minas Gerais" Value="MG" />
                    <asp:ListItem Text="Pará" Value="PA" />
                    <asp:ListItem Text="Paraíba" Value="PB" />
                    <asp:ListItem Text="Paraná" Value="PR" />
                    <asp:ListItem Text="Pernambuco" Value="PE" />
                    <asp:ListItem Text="Piauí" Value="PI" />
                    <asp:ListItem Text="Rio de Janeiro" Value="RJ" />
                    <asp:ListItem Text="Rio Grande do Norte" Value="RN" />
                    <asp:ListItem Text="Rio Grande do Sul" Value="RS" />
                    <asp:ListItem Text="Rondônia" Value="RO" />
                    <asp:ListItem Text="Roraima" Value="RR" />
                    <asp:ListItem Text="Santa Catarina" Value="SC" />
                    <asp:ListItem Text="São Paulo" Value="SP" />
                    <asp:ListItem Text="Sergipe" Value="SE" />
                    <asp:ListItem Text="Tocantins" Value="TO" />
                    <asp:ListItem Text="Distrito Federal" Value="DF" />
                </asp:DropDownList>
            </div>
        </div>
        <br />
        <div class="col-md-3">
            <asp:Button ID="btnSalvar" runat="server" CssClass="btn btn-primary" OnClick="BtnSalvar_Click" Text="Salvar" />
            <asp:Button ID="btnVoltar" runat="server" CssClass="btn btn-secondary" OnClick="BtnVoltar_Click" Text="Voltar" />
        </div>
    </div>
</asp:Content>
