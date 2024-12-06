<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="FINALRESTO.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="text-white">Error</h1>
    <asp:Label Text="text" class="form-label text-white" ID="lblError" runat="server" />
    <br />
    <a href="Home.aspx" class="btn btn-primary">Regresar a Home</a>
</asp:Content>
