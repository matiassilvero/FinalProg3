<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="FormularioProducto.aspx.cs" Inherits="FINALRESTO.FormularioProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function validar() {
            //Capturo el control
            const txtNombre = document.getElementById("txtNombre");
            const txtStock = document.getElementById("txtStock");
            const txtPrecio = document.getElementById("txtPrecio");
            //Nombre
            const nombreRegex = /^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$/;
            if (txtNombre.value == "" || !nombreRegex.test(txtNombre.value)) {
                txtNombre.classList.add("is-invalid");
                return false;
            }
            txtNombre.classList.remove("is-invalid");
            //Stock
            const stockRegex = /^[1-9][0-9]*$/;
            if (txtStock.value == "" || !stockRegex.test(txtStock.value)) {
                txtStock.classList.add("is-invalid");
                return false;
            }
            txtStock.classList.remove("is-invalid");
            //Precio
            const precioRegex = /^[1-9][0-9]*(\.[0-9]+)?$/;
            if (txtPrecio.value == "" || !precioRegex.test(txtPrecio.value)) {
                txtPrecio.classList.add("is-invalid");
                return false;
            }
            txtPrecio.classList.remove("is-invalid");

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <h1 class="text-white">Agregar/Modificar Producto:</h1>

    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <label for="txtId" class="form-label text-white">Id</label>
                <asp:TextBox runat="server" ID="txtId" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtNombre" class="form-label text-white">Nombre:</label>
                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtNombre" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtStock" class="form-label text-white">Stock:</label>
                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtStock" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtPrecio" class="form-label text-white">Precio:</label>
                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPrecio" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtTipoProducto" class="form-label text-white">Tipo de producto:</label>
                <asp:DropDownList runat="server" ID="ddlTipoProducto" CssClass="form-select"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <a href="ListaProductos.aspx" class="btn btn-primary">Regresar</a>
            </div>
        </div>

        <div class="col-6">
            <div class="mb-3">
                <label class="form-label text-white">Imagen de producto:</label>
            </div>
            <div class="mb-3">
                <asp:Image ID="imagenProducto"
                    ImageUrl="https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg"
                    runat="server"
                    CssClass="img-fluid w-50 mb-3" />
            </div>

            <% if (negocio.Seguridad.esGerente(Session["usuario"]))
                { %>
            <div class="mb-3">
                <div class="mb-3">
                    <asp:FileUpload type="file" ID="txtImagenSeleccionada" runat="server" class="form-control" />
                </div>
            </div>
            <div class="mb-3">
                <asp:Button Text="Aceptar/Guardar modificaciones" OnClientClick="return validar()" ID="btnAceptar" CssClass="btn btn-success" OnClick="btnAceptar_Click" runat="server" />
                <%if (reciboId)
                    { %>
                <asp:Button Text="Inactivar" ID="btnInactivar" OnClick="btnInactivar_Click" CssClass="btn btn-warning" runat="server" />
                <asp:Button Text="Eliminar" ID="btnEliminar" OnClick="btnEliminar_Click" CssClass="btn btn-danger" runat="server" />
                <%} %>
                <a href="ListaProductos.aspx" class="btn btn-primary">Cancelar</a>
            </div>
            <% } %>
            <%if (ConfirmaEliminacion)
                { %>
            <div class="mb-3">
                <asp:CheckBox Text="Confirmar Eliminacion" ID="chkConfirmaEliminacion" runat="server" CssClass="text-white" />
                <asp:Button Text="Eliminar" ID="btnConfirmaEliminar" OnClick="btnConfirmaEliminar_Click"
                    CssClass="btn btn-outline-danger" runat="server" />
            </div>
            <%} %>
        </div>
    </div>
</asp:Content>
