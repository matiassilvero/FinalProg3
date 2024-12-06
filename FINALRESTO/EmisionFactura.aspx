<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="EmisionFactura.aspx.cs" Inherits="FINALRESTO.EmisionFactura" %>

<%@ Import Namespace="dominio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .white-text {
            color: white;
        }

        .double-space {
            height: 2.5rem;
        }

        .dashed-line {
            border-bottom: 1px dashed #000;
        }

        .encuadrado {
            color: white;
            border: 1px solid #000;
            padding: 20px;
        }

        .product-list {
            margin-top: 20px;
        }

        .product-item {
            margin-bottom: 10px;
        }

        .total-price {
            font-weight: bold;
            font-size: 1.2rem;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container encuadrado">
        <h2 class="white-text">Resto UTN <span class="float-end">FACTURA</span></h2>
        <p class="white-text">Av. Hipolito Yrigoyen 288, Pacheco, Tigre</p>

        <div class="row mb-4">
            <div class="col-md-4">
                <label for="txtFactura" class="form-label white-text">Factura N°:</label>
                <asp:Label runat="server" ID="lblFactura" CssClass="form-control white-text"></asp:Label>
            </div>
            <div class="col-md-4">
                <label for="txtMesa" class="form-label white-text">Numero de Mesa:</label>
                <asp:Label runat="server" ID="lblMesa" CssClass="form-control white-text"></asp:Label>
            </div>
            <div class="col-md-4">
                <label for="txtFechaFactura" class="form-label white-text">Fecha y Hora:</label>
                <asp:Label runat="server" ID="lblFecha" CssClass="form-control white-text"></asp:Label>
            </div>
        </div>

        <div class="row mb-4">
            <div class="col-md-4">
                <label for="txtMesero" class="form-label white-text">Mesero:</label>
                <asp:Label runat="server" ID="lblMesero" CssClass="form-control white-text"></asp:Label>
            </div>
            <div class="col-md-4">
                <label for="txtTotal" class="form-label white-text">Total en pesos:</label>
                <asp:Label runat="server" ID="lblTotal" CssClass="form-control white-text"></asp:Label>
            </div>
        </div>

        <div class="row mb-4">
            <div class="col-md-12">
                <label for="txtProductos" class="form-label white-text">Productos pedidos:</label>
                <table class="table table-bordered table-striped">
                    <thead>
                        <tr>
                            <th>Nombre:</th>
                            <th>Cantidad:</th>
                            <th>Precio:</th>
                            <th>Subtotal:</th>
                            <th>Tipo de Producto:</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater runat="server" ID="repProductos">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("nombre") %></td>
                                    <td><%# Eval("cantidad") %></td>
                                    <td><%# Eval("precio", "{0:C}") %></td>
                                    <td><%# Eval("Subtotal", "{0:C}") %></td>
                                    <td><%# ((dominio.TipoProducto)Eval("tipoProducto")).ToString() == dominio.TipoProducto.PLATO.ToString() ? "Plato" : "Bebida" %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <br>
                <a href="ListaPedidos.aspx" class="btn btn-primary">Volver a Pedidos</a>
                <asp:Button runat="server" ID="btnImprimir" Text="Imprimir Factura" CssClass="btn btn-success float-end" OnClientClick="window.print();" />
            </div>
        </div>
    </div>
</asp:Content>
