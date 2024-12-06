<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="FINALRESTO.MiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <h2 class="text-white">Mi Perfil</h2>
    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <label class="form-label text-white">Id:</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtId" />
            </div>
            <div class="mb-3">
                <label class="form-label text-white">Dni:</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtDni" />
            </div>
            <div class="mb-3">
                <label class="form-label text-white">Nombre:</label>
                <asp:TextBox runat="server" CssClass="form-control" ClientIDMode="Static" ID="txtNombre" />
            </div>
            <div class="mb-3">
                <label class="form-label text-white">Apellido:</label>
                <asp:TextBox ID="txtApellido" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label class="form-label text-white">Pass:</label>
                <asp:TextBox ID="txtPass" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label class="form-label text-white">Fecha de Nacimiento:</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtFechaNacimiento" TextMode="Date" />
            </div>
            <div class="mb-3">
                <label for="txtTipoUsuario" class="form-label text-white">Tipo de usuario:</label>
                <asp:DropDownList runat="server" ID="ddlTipoUsuario" CssClass="form-select"></asp:DropDownList>
            </div>
            <div class="col-md-4">
                <a href="Home.aspx" class="btn btn-primary">Regresar</a>
            </div>
        </div>

        <div class="col-6">
            <div class="mb-3">
                <label class="form-label text-white">Imagen de perfil:</label>
            </div>
            <div class="mb-3">
                <asp:Image ID="imagenPerfil" ImageUrl="https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg"
                    runat="server" CssClass="img-fluid mb-3" />
            </div>
            <% if (negocio.Seguridad.esGerente(Session["usuario"]))
                { %>
            <div class="mb-3">
                <input type="file" id="txtImagenSeleccionada" runat="server" class="form-control" />
            </div>
            <asp:Button Text="Aceptar/Guardar modificaciones" CssClass="btn btn-primary"
                OnClick="btnGuardar_Click" ID="btnGuardar" runat="server" />
            <% } %>
        </div>
    </div>
</asp:Content>
