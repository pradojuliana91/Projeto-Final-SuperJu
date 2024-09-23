<%@ Page Title="Entrada Produto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="SuperJU.WEB.Web.Produto.Entrada.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(document).ready(function () {
            $("#txtValorCusto").maskMoney({ prefix: 'R$ ', allowNegative: true, thousands: '.', decimal: ',', affixesStay: false });
            $("#txtValorVenda").maskMoney({ prefix: 'R$ ', allowNegative: true, thousands: '.', decimal: ',', affixesStay: false });
            $("#txtQuantidade").keypress(function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
        })
    </script>
    <div class="container">
        <div>
            <h1>Entrada de Produtos</h1>
        </div>
        <div class="row g-3">
            <div class="col-md-3">
                <label for="txtNumeroNota" class="form-label">Nº Nota *</label>
                <asp:TextBox ID="txtNumeroNota" class="form-control" runat="server" MaxLength="20"></asp:TextBox>
            </div>
        </div>
        <br />
        <div class="row g-3">
            <div class="col-md-3">
                <label for="dplProduto" class="form-label">Produto *</label>
                <asp:DropDownList ID="dplProduto" DataTextField="Nome" DataValueField="Id" AutoPostBack="true" runat="Server" CssClass="form-select"
                    OnSelectedIndexChanged="DplProduto_SelectedIndexChanged">
                </asp:DropDownList>
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
            <div class="col-md-3">
                <asp:Button ID="btnAdicionar" runat="server" CssClass="btn btn-primary" OnClick="BtnAdicionar_Click" Text="Adicionar" />
            </div>
        </div>
        <br />
        <div class="col-md-3">
            <asp:Button ID="btnSalvar" runat="server" CssClass="btn btn-primary" OnClick="BtnSalvar_Click" Text="Finalizar Entrada" />
            <asp:Button ID="btnVoltar" runat="server" CssClass="btn btn-secondary" OnClick="BtnVoltar_Click" Text="Voltar" />
        </div>
        <br />
        <div style="clear: both"></div>
        <asp:GridView ID="gvEntrada" runat="server" AutoGenerateColumns="False" Width="100%"
            CssClass="table table-bordered table-striped table-hover" GridLines="None">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btn_excluir" runat="server" ToolTip="Excluir" Text="Excluir" CssClass="btn btn-primary"
                            CausesValidation="false" CommandName="Excluir" OnClick="BtnExcluir_Click"
                            CommandArgument='<%# Eval("ProdutoId") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ProdutoId" HeaderText="Código Produto" />
                <asp:BoundField DataField="ProdutoNome" HeaderText="Nome Produto" />
                <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" />
                <asp:BoundField DataField="ValorCusto" HeaderText="Vlr Custo" />
                <asp:BoundField DataField="ValorVenda" HeaderText="Vlr Venda" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
