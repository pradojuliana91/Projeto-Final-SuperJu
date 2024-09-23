<%@ Page Title="Visualiza Entrada Produto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Visualizar.aspx.cs" Inherits="SuperJU.WEB.Web.Produto.Entrada.Visualizar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div>
            <h1>Visualiza Entrada de Produtos</h1>
        </div>
        <div class="row g-3">
            <div class="col-md-3">
                <label for="txtNumeroNota" class="form-label">Nº Nota</label>
                <asp:TextBox ID="txtNumeroNota" class="form-control" runat="server" MaxLength="20"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label for="txtDataEntrada" class="form-label">Data Entrada</label>
                <asp:TextBox ID="txtDataEntrada" class="form-control" runat="server" MaxLength="50"></asp:TextBox>
            </div>
        </div>
        <br />
        <div class="col-md-3">
            <asp:Button ID="btnVoltar" runat="server" CssClass="btn btn-secondary" OnClick="BtnVoltar_Click" Text="Voltar" />
        </div>
        <br />
        <div style="clear: both"></div>
        <asp:GridView ID="gvEntrada" runat="server" AutoGenerateColumns="False" Width="100%"
            CssClass="table table-bordered table-striped table-hover" GridLines="None">
            <Columns>
                <asp:BoundField DataField="ProdutoId" HeaderText="Código Produto" />
                <asp:BoundField DataField="ProdutoNome" HeaderText="Nome Produto" />
                <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" />
                <asp:BoundField DataField="ValorCusto" HeaderText="Vlr Custo" DataFormatString="{0:C}" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
