<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="FormularioMesa.aspx.cs" Inherits="FINALRESTO.FormularioMesa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function validar() {
            //Capturo el control
            const txtCapacidad = document.getElementById("txtCapacidad");
            //Capacidad
            const capacidadRegex = /^[1-9][0-9]*$/;
            if (txtCapacidad.value == "" || !capacidadRegex.test(txtCapacidad.value)) {
                txtCapacidad.classList.add("is-invalid");
                return false;
            }
            txtCapacidad.classList.remove("is-invalid");
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <h1 class="text-white">Agregar/Modificar Mesa:</h1>
    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <label for="txtId" class="form-label text-white">Id</label>
                <asp:TextBox runat="server" ID="txtId" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtCapacidad" class="form-label text-white">Capacidad:</label>
                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtCapacidad" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtDisponibilidad" class="form-label text-white">Disponibilidad:</label>
                <asp:DropDownList runat="server" ID="ddlDisponibilidad" CssClass="form-select"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <asp:Button Text="Aceptar" ID="btnAceptar" OnClientClick="return validar()" CssClass="btn btn-success" OnClick="btnAceptar_Click" runat="server" />
                <%if (reciboId)
                    { %>
                <asp:Button Text="Inactivar" ID="btnInactivar" OnClick="btnInactivar_Click" CssClass="btn btn-warning" runat="server" />
                <%} %>
                <a href="ListaMesas.aspx" class="btn btn-primary">Regresar</a>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-6">
            <asp:UpdatePanel ID="UpdatePanel" runat="server">
                <ContentTemplate>
                    <div class="mb-3">
                        <%if (reciboId)
                            { %>
                        <asp:Button Text="Eliminar" ID="btnEliminar" OnClick="btnEliminar_Click"
                            CssClass="btn btn-danger" runat="server" />
                        <%} %>
                    </div>
                    <%if (ConfirmaEliminacion)
                        { %>
                    <div class="mb-3">
                        <asp:CheckBox Text="Confirmar Eliminación" ID="chkConfirmaEliminacion" runat="server" />
                        <asp:Button Text="Eliminar" ID="btnConfirmaEliminar" OnClick="btnConfirmaEliminar_Click"
                            CssClass="btn btn-outline-danger" runat="server" />
                    </div>
                    <%} %>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
