<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ListaProductos.aspx.cs" Inherits="FINALRESTO.ListaProductos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .custom-img {
            width: 100%;
            height: 200px;
            object-fit: cover;
            border-radius: 8px;
            background-color: black;
        }
    </style>

    <h1 class="text-white">Productos</h1>
    <a href="FormularioProducto.aspx" class="btn btn-success me-2">Agregar Producto</a>
    <hr />
    <div class="row row-cols-1 row-cols-md-3 g-4">
        <asp:Repeater runat="server" ID="repRepetidor">
            <ItemTemplate>
                <div class="col">
                    <div class="card">
                        <img src='/Images/<%# Eval("urlImagen") %>?v=<%# DateTime.Now.Ticks %>' class="card-img-top custom-img" alt="imagenProducto" style="width: 100%; height: 200px; object-fit: contain;" />
                        <div class="card-body">
                            <h5 class="card-title"><%#Eval("nombre") %></h5>
                            <h5 class="card-title">$<%#Eval("precio") %> c/u</h5>
                            <h5 class="card-title">Stock: <%#Eval("stock") %></h5>
                            <h5 class="card-title"><%#Eval("tipoProducto") %></h5>
                            <a href="FormularioProducto.aspx?id=<%#Eval("id") %>" class="btn btn-primary btn-sm">Ver/Modificar</a><%--Le paso el Id por URL--%>
                            <a href="FormularioPedidoDeProducto.aspx?id=<%#Eval("id") %>" class="btn btn-primary btn-sm" Visible='<%# recibiIdPedido %>'>Agregar a Pedido</a><%--Le paso el Id por URL--%>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

</asp:Content>
