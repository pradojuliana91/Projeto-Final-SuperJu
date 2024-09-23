<%@ Page Title="Relatório Estoque" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pesquisa.aspx.cs" Inherits="SuperJU.WEB.Web.Relatorio.Estoque.Pesquisa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(document).ready(function () {
            $("#txtCodigo").keypress(function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
            $("#txtQuantidadeMaiorQue").keypress(function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
            $("#txtQuantidadeMenorQue").keypress(function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
        })
    </script>
    <div class="container">
        <div>
            <h1>Relatório de Estoque</h1>
        </div>
        <div class="row g-3">
            <div class="col-md-3">
                <label for="txtCodigo" class="form-label">Código</label>
                <asp:TextBox ID="txtCodigo" class="form-control" runat="server" MaxLength="9"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label for="txtProduto" class="form-label">Produto</label>
                <asp:TextBox ID="txtProduto" class="form-control" runat="server" MaxLength="30"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label for="txtQuantidadeMaiorQue" class="form-label">Quantidade Maior Que</label>
                <asp:TextBox ID="txtQuantidadeMaiorQue" class="form-control" runat="server" MaxLength="9"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label for="txtQuantidadeMenorQue" class="form-label">Quantidade Menor Que</label>
                <asp:TextBox ID="txtQuantidadeMenorQue" class="form-control" runat="server" MaxLength="9"></asp:TextBox>
            </div>
            <br />
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
                <asp:BoundField DataField="ProdutoId" HeaderText="Código" />
                <asp:BoundField DataField="ProdutoNome" HeaderText="Nome" />
                <asp:BoundField DataField="ValorCusto" HeaderText="Vlr Custo" DataFormatString="{0:C}" />
                <asp:BoundField DataField="ValorVenda" HeaderText="Vlr Venda" DataFormatString="{0:C}" />
                <asp:BoundField DataField="Quantidade" HeaderText="Qtd" />
                <asp:BoundField DataField="QtdSaidaU30Dia" HeaderText="Qtd Saída Últ. 30 Dias" />
                <asp:BoundField DataField="DataUltimaVenda" HeaderText="Data Última Venda" DataFormatString="{0:dd/MM/yyyy}" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
