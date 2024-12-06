<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ListaPedidos.aspx.cs" Inherits="FINALRESTO.ListaPedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="text-white">Pedidos</h1>
    <a href="FormularioPedido.aspx" class="btn btn-success me-2">Agregar Pedido</a>
    <hr />
    <div class="container">
        <asp:GridView
            runat="server"
            ID="dgvPedidos"
            CssClass="table table-striped table-bordered table-dark"
            AutoGenerateColumns="False"
            DataKeyNames="id"
            OnRowDataBound="dgvPedidos_RowDataBound">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="Id Pedido" />
                <asp:BoundField DataField="fecha" HeaderText="Fecha y Hora" />
                <asp:BoundField DataField="idMesa" HeaderText="Mesa" />
                <asp:BoundField DataField="apellido" HeaderText="Mesero" />
                <asp:BoundField DataField="estado" HeaderText="Estado de Pedido" />

                <asp:TemplateField HeaderText="Productos">
                    <ItemTemplate>
                        <asp:Repeater ID="repProductos" runat="server">
                            <HeaderTemplate>
                                <table class="table table-bordered table-striped mt-2">
                                    <thead>
                                        <tr>
                                            <th>Nombre</th>
                                            <th>Cantidad</th>
                                            <th>Precio</th>
                                            <th>Subtotal</th>
                                            <th>Tipo</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>

                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("nombre") %></td>
                                    <td><%# Eval("cantidad") %></td>
                                    <td><%# Eval("precio", "{0:C}") %></td>
                                    <td><%# Eval("Subtotal", "{0:C}") %></td>
                                    <td><%# ((dominio.TipoProducto)Eval("tipoProducto")).ToString() == dominio.TipoProducto.PLATO.ToString() ? "Plato" : "Bebida" %></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                        </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <%# Eval("estado").ToString() == "FACTURADO" || Eval("estado").ToString() == "CANCELADO" ? 
                                "" : "<a href='EmisionFactura.aspx?id=" + Eval("id") + "' class='btn btn-success me-3 mb-2'>Facturar</a>" %>

                        <%# Eval("estado").ToString() == "FACTURADO" || Eval("estado").ToString() == "CANCELADO" ? 
                                "" : "<a href='ListaPedidosDeProductos.aspx?id=" + Eval("id") + "' class='btn btn-primary me-3 mb-2'>Quitar Productos</a>" %>

                        <asp:Button ID="btnVer" runat="server" Text="✍" CssClass="btn btn-primary mb-2" OnClick="btnVer_Click" CommandArgument='<%# Eval("id") %>'
                            Enabled='<%# !(Eval("estado").ToString() == "FACTURADO" || Eval("estado").ToString() == "CANCELADO") %>' />
                    </ItemTemplate>


                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
