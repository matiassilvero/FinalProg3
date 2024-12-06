<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="FINALRESTO.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function validar() {
            //Capturo el control
            const txtNombre = document.getElementById("txtNombre");
            const txtApellido = document.getElementById("txtApellido");
            const txtDni = document.getElementById("txtDni");
            const txtPass = document.getElementById("txtPass");
            const txtFechaNacimiento = document.getElementById('<%= txtFechaNacimiento.ClientID %>');

            //Nombre
            const nombreRegex = /^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$/;
            if (txtNombre.value == "" || !nombreRegex.test(txtNombre.value)) {
                txtNombre.classList.add("is-invalid");
                return false;
            }
            txtNombre.classList.remove("is-invalid");

            //Apellido
            const apellidoRegex = /^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$/;
            if (txtApellido.value == "" || !apellidoRegex.test(txtApellido.value)) {
                txtApellido.classList.add("is-invalid");
                return false;
            }
            txtApellido.classList.remove("is-invalid");

            //Dni
            const dniRegex = /^[0-9]{7,8}$/;
            if (txtDni.value == "" || !dniRegex.test(txtDni.value)) {
                txtDni.classList.add("is-invalid");
                return false;
            }
            txtDni.classList.remove("is-invalid");

            //Pass
            const passRegex = /^.{2,}$/;
            if (txtPass.value == "" || !passRegex.test(txtPass.value)) {
                txtPass.classList.add("is-invalid");
                return false;
            }
            txtPass.classList.remove("is-invalid");

            //Fecha de Nacimiento
            const maxDate = new Date('2004-12-31');
            if (txtFechaNacimiento.value == "" || new Date(txtFechaNacimiento.value) > maxDate) {
                txtFechaNacimiento.classList.add("is-invalid");
                return false;
            }
            txtFechaNacimiento.classList.remove("is-invalid");

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="text-white">Registro</h2>
    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <label for="txtNombre" class="form-label text-white">Nombre:</label>
                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtNombre" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtApellid" class="form-label text-white">Apellido:</label>
                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtApellido" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtDni" class="form-label text-white">Dni:</label>
                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtDni" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtPass" class="form-label text-white">Pass:</label>
                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPass" TextMode="Password" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtFechaNacimiento" class="form-label text-white">Fecha de nacimiento:</label>
                <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control" ID="txtFechaNacimiento" TextMode="Date" />
            </div>
            <div class="mb-3">
                <asp:Button Text="Registrarme" OnClientClick="return validar()" CssClass="btn btn-success" ID="btnRegistro" OnClick="btnRegistro_Click" runat="server" />
            </div>
            <div class="mb-3">
                <a href="Home.aspx" class="btn btn-primary">Regresar</a>
            </div>
        </div>
        <div class="col-6">
            <div class="mb-3">
                <label class="form-label text-white">Imagen de perfil:</label>
            </div>
            <asp:Image ID="imagenPerfil"
                ImageUrl="https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg"
                runat="server"
                CssClass="img-fluid w-50 mb-3" />

            <input type="file" id="txtImagenSeleccionada" runat="server" class="form-control" />
        </div>
    </div>
</asp:Content>
