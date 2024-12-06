<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ListaUsuarios.aspx.cs" Inherits="FINALRESTO.ListaUsuarios" %>

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

    <h1 class="text-white">Usuarios</h1>
    <a href="FormularioUsuario.aspx" class="btn btn-success me-2">Agregar Usuario</a>
    <hr />
    <div class="row row-cols-1 row-cols-md-3 g-4">
        <asp:Repeater runat="server" ID="repRepetidor">
            <ItemTemplate>
                <div class="col">
                    <div class="card">
                        <img src='/Images/<%# Eval("urlImagen") %>?v=<%# DateTime.Now.Ticks %>' class="card-img-top custom-img" alt="imagenUsuario" style="width: 100%; height: 200px; object-fit: contain;" />
                        <div class="card-body">
                            <h5 class="card-title"><%#Eval("nombre") %> <%#Eval("apellido") %></h5>
                            <h5 class="card-title">Dni: <%#Eval("dni") %></h5>
                            <h5 class="card-title"><%#Eval("tipoUsuario") %></h5>
                            <a href="FormularioUsuario.aspx?id=<%#Eval("id") %>" class="btn btn-primary btn-sm">Ver/Modificar</a>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
