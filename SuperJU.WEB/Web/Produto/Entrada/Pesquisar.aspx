<%@ Page Title="Pesquisa Entrada Produto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pesquisar.aspx.cs" Inherits="SuperJU.WEB.Web.Produto.Entrada.Pesquisar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(document).ready(function () {
            $('#txtDataEntradaInicio').inputmask('99/99/9999');
            $('#txtDataEntradaFim').inputmask('99/99/9999');
        })
    </script>
    <div class="container">
        <div>
            <h1>Pesquisa Entrada de Produtos</h1>
        </div>
        <div class="row g-3">
            <div class="col-md-3">
                <label for="txtNumeroNota" class="form-label">Nº Nota</label>
                <asp:TextBox ID="txtNumeroNota" class="form-control" runat="server" MaxLength="20"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label for="txtDataEntradaInicio" class="form-label">Data Entrada Início</label>
                <asp:TextBox ID="txtDataEntradaInicio" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label for="txtDataEntradaFim" class="form-label">Data Entrada Final</label>
                <asp:TextBox ID="txtDataEntradaFim" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox>
            </div>
            <br />
        </div>
        <br />
        <div class="col-md-3">
            <asp:Button ID="btnPesquisar" runat="server" CssClass="btn btn-primary" OnClick="BtnPesquisa_Click" Text="Pesquisar" />
            <asp:Button ID="BtnNovo" runat="server" CssClass="btn btn-secondary" OnClick="BtnNovo_Click" Text="Novo" />
        </div>
        <br />
        <div style="clear: both"></div>
        <asp:GridView ID="gvEntrada" runat="server" AutoGenerateColumns="False" Width="100%"
            CssClass="table table-bordered table-striped table-hover" GridLines="None">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btn_editar" runat="server" ToolTip="Visualizar" Text="Visualizar" CssClass="btn btn-primary"
                            CausesValidation="false" CommandName="visualizar" OnClick="BtnEditar_Click"
                            CommandArgument='<%# Eval("Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="NumeroNota" HeaderText="Nº Nota" />
                <asp:BoundField DataField="DataEntrada" HeaderText="Data Entrada" DataFormatString="{0:dd/MM/yyyy}" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
