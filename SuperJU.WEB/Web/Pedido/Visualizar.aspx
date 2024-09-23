<%@ Page Title="Visualizar Pedido" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Visualizar.aspx.cs" Inherits="SuperJU.WEB.Web.Pedido.Visualizar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(document).ready(function () {
            $("#txtValorTotalPedido").maskMoney({ prefix: 'R$ ', allowNegative: true, thousands: '.', decimal: ',', affixesStay: false });
        })
    </script>
    <div class="container">
        <div>
            <h1>Visualizar Pedido</h1>
        </div>
        <div class="row g-3">
            <div class="col-md-3">
                <label for="txtCliente" class="form-label">Cliente</label>
                <asp:TextBox ID="txtCliente" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label for="txtFormaPagamento" class="form-label">Forma Pagamento</label>
                <asp:TextBox ID="txtFormaPagamento" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label for="txtDataPedido" class="form-label">Data Pedido</label>
                <asp:TextBox ID="txtDataPedido" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label for="txtValorTotalPedido" class="form-label">Valor Total Pedido</label>
                <asp:TextBox ID="txtValorTotalPedido" ClientIDMode="Static" class="form-control" MaxLength="20" runat="server"></asp:TextBox>
            </div>
        </div>
        <br />
        <div class="col-md-3">
            <asp:Button ID="btnVoltar" runat="server" CssClass="btn btn-secondary" OnClick="BtnVoltar_Click" Text="Voltar" />
        </div>
        <br />
        <div style="clear: both"></div>
        <asp:GridView ID="gvPedido" runat="server" AutoGenerateColumns="False" Width="100%"
            CssClass="table table-bordered table-striped table-hover" GridLines="None">
            <Columns>
                <asp:BoundField DataField="ProdutoId" HeaderText="Código Produto" />
                <asp:BoundField DataField="ProdutoNome" HeaderText="Nome Produto" />
                <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" />
                <asp:BoundField DataField="Valor" HeaderText="Vlr Venda" DataFormatString="{0:C}" />
                <asp:BoundField DataField="ValorTotal" HeaderText="Vlr Total" DataFormatString="{0:C}" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
