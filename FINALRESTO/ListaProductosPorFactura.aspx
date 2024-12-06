<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ListaProductosPorFactura.aspx.cs" Inherits="FINALRESTO.ListaProductosPorFactura" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <h1 class="text-white">Pedidos de Productos realizados</h1>
    <hr />
    //Crear aca arriba un encabezado con datos de la factura, abajo mostraremos el dgv con los productos
    <div class="container">
        <asp:GridView
            ID="dgvProductos"
            runat="server"
            CssClass="table table-striped table-bordered table-dark"
            AutoGenerateColumns="False"
            DataKeyNames="Id">

            <Columns>
                <asp:BoundField DataField="nombre" HeaderText="Producto:" />
                <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                <asp:BoundField DataField="precio" HeaderText="Precio Unitario:" />
                <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
