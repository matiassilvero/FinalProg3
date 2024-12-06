<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FINALRESTO.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script>
     function validar() {
         //Capturo el control
         const txtDni = document.getElementById("txtDni");
         const txtPass = document.getElementById("txtPass");
         //Dni
         const dniRegex = /^[0-9]{7,8}$/;
            if (txtDnalue == "" || !dniRegex.test(txtDni.value)) {
                txtDni.clList.add("is-invalid");
                return fa;
            }
         txtDni.classList.remove("is-invalid");

         //Pass
         const passRegex = /^.{2,}$/;
            if (txtPavalue == "" || !passRegex.test(txtPass.value)) {
                txtPass.csList.add("is-invalid");
                return fa;
            }
         txtPass.classList.remove("is-invalid");

         return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-center align-items-start vh-100" style="padding-top: 3rem;">
        <div class="col-10 col-md-6 col-lg-5 text-light p-4 text-center">
            <div class="mb-3">
                <hr />
                <h2>Bienvenido! Ingrese al Resto</h2>
                <div class="mb-3">
                    <label for="txtDni" class="form-label text-white">Dni:</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtDni" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="txtPass" class="form-label text-white">Pass:</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPass" TextMode="Password" CssClass="form-control" />
                </div>
                <asp:Button Text="Ingresar" OnClientClick="return validar()" CssClass="btn btn-success" ID="btnLogin" OnClick="btnLogin_Click" runat="server" />
                <a href="Home.aspx" class="btn btn-primary">Cancelar</a>
                <hr />
            </div>
        </div>
    </div>
</asp:Content>
