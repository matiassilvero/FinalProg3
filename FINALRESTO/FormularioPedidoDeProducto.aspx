<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="FormularioPedidoDeProducto.aspx.cs" Inherits="FINALRESTO.FormularioPedidoDeProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function validar() {
            //Capturo el control
            const txtCantidad = document.getElementById("txtCantidad");
            //Cantidad
            const cantidadRegex = /^[1-9][0-9]*$/;
            if (txtCantidad.value == "" || !cantidadRegex.test(txtCantidad.value)) {
                txtCantidad.classList.add("is-invalid");
                return false;
            }
            txtCantidad.classList.remove("is-invalid");
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="text-white">Agregar Producto:</h1>
    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <label for="txtId" class="form-label text-white">Id</label>
                <asp:TextBox runat="server" ID="txtId" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtIdPedido" class="form-label text-white">Id Pedido:</label>
                <asp:TextBox runat="server" ID="txtIdPedido" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtIdProducto" class="form-label text-white">Id Producto:</label>
                <asp:TextBox runat="server" ID="txtIdProducto" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtCantidad" class="form-label text-white">Cantidad:</label>
                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtCantidad" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <asp:Button Text="Aceptar" OnClienClick="return validar()" ID="btnAceptar" OnClick="btnAceptar_Click" CssClass="btn btn-primary" runat="server" />
                <a href="ListaPedidos.aspx" class=" btn btn-primary">Cancelar</a>
            </div>
        </div>
    </div>
</asp:Content>
