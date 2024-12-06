<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="FormularioPedido.aspx.cs" Inherits="FINALRESTO.FormularioPedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <h1 class="text-white">Agregar/Modificar Pedido:</h1>
    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <label for="txtId" class="form-label text-white">Id</label>
                <asp:TextBox runat="server" ID="txtId" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="ddlIdMesa" class="form-label text-white">Numero de Mesa(IdMesa):</label>
                <asp:DropDownList runat="server" ID="ddlIdMesa" CssClass="form-select"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <label for="ddlIdMesero" class="form-label text-white">Mesero(IdUsuario):</label>
                <asp:DropDownList runat="server" ID="ddlIdMesero" CssClass="form-select"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <label for="txtFechaHora" class="form-label text-white">Fecha y Hora de pedido:</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtFechaHora" TextMode="DateTimeLocal" />
                <script>
                        document.getElementById('<%= txtFechaHora.ClientID %>').min = new Date(new Date().setHours(new Date().getHours() - 3)).toISOString().slice(0, 16);
                </script>
            </div>
            <div class="mb-3">
                <label for="txtEstado" class="form-label text-white">Estado de Pedido:</label>
                <asp:DropDownList runat="server" ID="ddlEstado" CssClass="form-select"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <asp:Button Text="Aceptar" ID="btnAceptar" CssClass="btn btn-success" OnClick="btnAceptar_Click" runat="server" />
                <a href="ListaPedidos.aspx" class="btn btn-primary">Regresar</a>
            </div>
        </div>
        <div class="col-6">
            <div class="mb-3 ">
                <asp:Button Text="Agregar Productos" ID="agregarProducto" CssClass="btn btn-success" OnClick="agregarProducto_Click" runat="server" />
            </div>
        </div>
    </div>
    <div class="col-6">
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate>
                <div class="mb-3">
                    <asp:Button Text="Eliminar" ID="btnEliminar" OnClick="btnEliminar_Click"
                        CssClass="btn btn-danger" runat="server" />
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
</asp:Content>
