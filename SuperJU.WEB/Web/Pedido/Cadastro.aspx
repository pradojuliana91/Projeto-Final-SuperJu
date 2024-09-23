<%@ Page Title="Pedido" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="SuperJU.WEB.Web.Pedido.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(document).ready(function () {
            $("#txtValorTotalPedido").maskMoney({ prefix: 'R$ ', allowNegative: true, thousands: '.', decimal: ',', affixesStay: false });
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
            <h1>Pedido</h1>
        </div>
        <div class="row g-3">
            <div class="col-md-3">
                <label for="dplCliente" class="form-label">Cliente *</label>
                <asp:DropDownList ID="dplCliente" DataTextField="Nome" DataValueField="Id" runat="Server" CssClass="form-select"></asp:DropDownList>
            </div>
            <div class="col-md-3">
                <label for="dplFormaPagamento" class="form-label">Forma Pagamento *</label>
                <asp:DropDownList ID="dplFormaPagamento" DataTextField="Nome" DataValueField="Id" runat="Server" CssClass="form-select"></asp:DropDownList>
            </div>
            <div class="col-md-3">
                <label for="txtValorTotalPedido" class="form-label">Valor Total Pedido</label>
                <asp:TextBox ID="txtValorTotalPedido" ClientIDMode="Static" class="form-control" MaxLength="20" runat="server"></asp:TextBox>
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
                <label for="txtQuantidadeEmEstoque" class="form-label">Quantidade Em Estoque</label>
                <asp:TextBox ID="txtQuantidadeEmEstoque" ClientIDMode="Static" class="form-control" MaxLength="9" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label for="txtValorVenda" class="form-label">Valor Venda</label>
                <asp:TextBox ID="txtValorVenda" ClientIDMode="Static" class="form-control" MaxLength="20" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <asp:Button ID="btnAdicionar" runat="server" CssClass="btn btn-primary" OnClick="BtnAdicionar_Click" Text="Adicionar" />
            </div>
        </div>
        <br />
        <div class="col-md-3">
            <asp:Button ID="btnSalvar" runat="server" CssClass="btn btn-primary" OnClick="BtnSalvar_Click" Text="Finalizar Pedido" />
            <asp:Button ID="btnVoltar" runat="server" CssClass="btn btn-secondary" OnClick="BtnVoltar_Click" Text="Voltar" />
        </div>
        <br />
        <div style="clear: both"></div>
        <asp:GridView ID="gvPedido" runat="server" AutoGenerateColumns="False" Width="100%"
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
                <asp:BoundField DataField="ValorVenda" HeaderText="Vlr Venda" DataFormatString="{0:C}" />
                <asp:BoundField DataField="ValorTotal" HeaderText="Vlr Total" DataFormatString="{0:C}" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
