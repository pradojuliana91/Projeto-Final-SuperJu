<%@ Page Title="Relatório Venda" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pesquisa.aspx.cs" Inherits="SuperJU.WEB.Web.Relatorio.Venda.Pesquisa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(document).ready(function () {
            $('#txtDataPedidoInicio').inputmask('99/99/9999');
            $('#txtDataPedidoFim').inputmask('99/99/9999');
            $("#txtCodigo").keypress(function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
        })
    </script>
    <div class="container">
        <div>
            <h1>Relatório de Vendas</h1>
        </div>
        <div class="row g-3">
            <div class="col-md-3">
                <label for="txtCodigo" class="form-label">Código</label>
                <asp:TextBox ID="txtCodigo" class="form-control" ClientIDMode="Static" runat="server" MaxLength="9"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label for="dplCliente" class="form-label">Cliente</label>
                <asp:DropDownList ID="dplCliente" DataTextField="Nome" DataValueField="Id" runat="Server" CssClass="form-select"></asp:DropDownList>
            </div>
            <div class="col-md-3">
                <label for="dplFormaPagamento" class="form-label">Forma Pagamento</label>
                <asp:DropDownList ID="dplFormaPagamento" DataTextField="Nome" DataValueField="Id" runat="Server" CssClass="form-select"></asp:DropDownList>
            </div>
        </div>
        <br />
        <div class="row g-3">
            <div class="col-md-3">
                <label for="txtDataPedidoInicio" class="form-label">Data Entrada Início</label>
                <asp:TextBox ID="txtDataPedidoInicio" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label for="txtDataPedidoFim" class="form-label">Data Entrada Final</label>
                <asp:TextBox ID="txtDataPedidoFim" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <br />
        <div class="col-md-3">
            <asp:Button ID="btnPesquisar" runat="server" CssClass="btn btn-primary" OnClick="BtnPesquisa_Click" Text="Pesquisar" />
        </div>
        <br />
        <div style="clear: both"></div>
        <asp:GridView ID="gvRel" runat="server" AutoGenerateColumns="False" Width="100%"
            CssClass="table table-bordered table-striped table-hover" GridLines="None">
            <Columns>
                <asp:BoundField DataField="PedidoId" HeaderText="Cod. Pedido" />
                <asp:BoundField DataField="ClienteNome" HeaderText="Cliente" />
                <asp:BoundField DataField="FormaPagamentoNome" HeaderText="Forma Pag." />
                <asp:BoundField DataField="DataPedido" HeaderText="Data" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="ValorTotal" HeaderText="Vlr Total" DataFormatString="{0:C}" />
                <asp:BoundField DataField="ValorCustoTotal" HeaderText="Vlr Custo" DataFormatString="{0:C}" />
                <asp:BoundField DataField="ValorLucro" HeaderText="Vlr Lucro" DataFormatString="{0:C}" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
