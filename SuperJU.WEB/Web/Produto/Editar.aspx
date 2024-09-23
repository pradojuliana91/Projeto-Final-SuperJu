<%@ Page Title="Editar Produto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Editar.aspx.cs" Inherits="SuperJU.WEB.Web.Produto.Editar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(document).ready(function () {
            $("#txtValorCusto").maskMoney({ prefix: 'R$ ', allowNegative: false, thousands: '.', decimal: ',', affixesStay: false });
            $("#txtValorVenda").maskMoney({ prefix: 'R$ ', allowNegative: false, thousands: '.', decimal: ',', affixesStay: false });
            $("#txtQuantidade").keypress(function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
        })
    </script>
    <div class="container">
        <div>
            <h1>Editar Produto</h1>
        </div>
        <div class="row g-3">
            <div class="col-md-3">
                <label for="txtNome" class="form-label">Nome *</label>
                <asp:TextBox ID="txtNome" class="form-control" runat="server" MaxLength="30"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label for="txtDescricao" class="form-label">Descrição *</label>
                <asp:TextBox ID="txtDescricao" class="form-control" runat="server" MaxLength="100"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label for="txtQuantidade" class="form-label">Quantidade *</label>
                <asp:TextBox ID="txtQuantidade" ClientIDMode="Static" class="form-control" MaxLength="9" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label for="txtValorCusto" class="form-label">Valor Custo *</label>
                <asp:TextBox ID="txtValorCusto" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label for="txtValorVenda" class="form-label">Valor Venda *</label>
                <asp:TextBox ID="txtValorVenda" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <br />
        <div class="col-md-3">
            <asp:Button ID="btnSalvar" runat="server" CssClass="btn btn-primary" OnClick="BtnSalvar_Click" Text="Salvar" />
            <asp:Button ID="btnVoltar" runat="server" CssClass="btn btn-secondary" OnClick="BtnVoltar_Click" Text="Voltar" />
        </div>
    </div>
</asp:Content>
