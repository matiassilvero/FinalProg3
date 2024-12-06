<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ListaPedidosDeProductos.aspx.cs" Inherits="FINALRESTO.ListaPedidosDeProductos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1 class="text-white">Pedidos de Productos realizados</h1>
    <hr />

    <div class="container">
        <asp:GridView
            ID="dgvPedidosDeProductos"
            runat="server"
            CssClass="table table-striped table-bordered table-dark"
            AutoGenerateColumns="False"
            DataKeyNames="Id">

            <Columns>
                <asp:BoundField DataField="id" HeaderText="Id" />
                <asp:BoundField DataField="idPedido" HeaderText="Pedido N°:" />
                <asp:BoundField DataField="idProducto" HeaderText="Producto:" />
                <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />

                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                    <a href="QuitarProducto.aspx?id=<%# Eval("id") %>" class="btn btn-primary btn-sm">Quitar Producto a este Pedido</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

    </div>
</asp:Content>
                        