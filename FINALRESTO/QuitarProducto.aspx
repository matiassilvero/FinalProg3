<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="QuitarProducto.aspx.cs" Inherits="FINALRESTO.QuitarProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="text-white">Quitar Producto:</h1>
    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <label for="txtIdPedido" class="form-label text-white">Id Pedido:</label>
                <asp:TextBox runat="server" ID="txtIdPedido" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtIdProducto" class="form-label text-white">Id Producto:</label>
                <asp:TextBox runat="server" ID="txtIdProducto" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtCantidad" class="form-label text-white">Cantidad a quitar:</label>
                <asp:TextBox runat="server" ID="txtCantidad" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <asp:Button Text="Aceptar" ID="btnAceptar" OnClick="btnAceptar_Click" CssClass="btn btn-primary" runat="server" />
                <a href="ListaPedidos.aspx" class=" btn btn-primary">Cancelar</a>
            </div>
        </div>
    </div>


</asp:Content>
