<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ListaFacturas.aspx.cs" Inherits="FINALRESTO.ListaFacturas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="text-white">Facturas</h1>
    <hr />
    <div class="container">
        <asp:GridView
            ID="dgvFacturas"
            runat="server"
            CssClass="table table-striped table-bordered table-dark"
            AutoGenerateColumns="False"
            DataKeyNames="Id">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="Id" />
                <asp:BoundField DataField="numeroFactura" HeaderText="Numero de Factura:" />
                <asp:BoundField DataField="mesa" HeaderText="Mesa:" />
                <asp:BoundField DataField="mesero" HeaderText="Mesero:" />
                <asp:BoundField DataField="fecha" HeaderText="Fecha y Hora:" />
                <asp:BoundField DataField="importe" HeaderText="Importe total:" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
