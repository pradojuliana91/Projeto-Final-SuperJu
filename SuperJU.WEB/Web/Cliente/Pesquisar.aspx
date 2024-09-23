<%@ Page Title="Pesquisa Cliente" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pesquisar.aspx.cs" Inherits="SuperJU.WEB.Web.Cliente.Pesquisar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(document).ready(function () {
            $("#txtCodigo").keypress(function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
        })
    </script>
    <div class="container">
        <div>
            <h1>Pesquisa Cliente</h1>
        </div>
        <div class="row g-3">
            <div class="col-md-3">
                <label for="txtCodigo" class="form-label">Código</label>
                <asp:TextBox ID="txtCodigo" ClientIDMode="Static" class="form-control" runat="server" MaxLength="9"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label for="txtCliente" class="form-label">Cliente</label>
                <asp:TextBox ID="txtCliente" class="form-control" runat="server" MaxLength="50"></asp:TextBox>
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
        <asp:GridView ID="gvCliente" runat="server" AutoGenerateColumns="False" Width="100%"
            CssClass="table table-bordered table-striped table-hover" GridLines="None">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btn_editar" runat="server" ToolTip="Editar" Text="Editar" CssClass="btn btn-primary"
                            CausesValidation="false" CommandName="editar" OnClick="BtnEditar_Click"
                            CommandArgument='<%# Eval("Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="id" HeaderText="Código" />
                <asp:BoundField DataField="Nome" HeaderText="Nome" />
                <asp:BoundField DataField="DataNascimento" HeaderText="Data Nascimento" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="Telefone" HeaderText="Telefone" DataFormatString="{0:(##) #####-####}" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
