<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ListaMesas.aspx.cs" Inherits="FINALRESTO.ListaMesas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="text-white">Mesas</h1>
    <a href="FormularioMesa.aspx" class="btn btn-success me-2">Agregar Mesa </a>
    <hr/>

 <div class="container" >
    <asp:GridView 
        ID="dgvMesas" 
        runat="server" 
        CssClass="table table-striped table-bordered table-dark" 
        AutoGenerateColumns="False" 
        DataKeyNames="Id">
        
        <Columns>
            <asp:BoundField DataField="id" HeaderText="Id" />
            <asp:BoundField DataField="capacidad" HeaderText="Capacidad(Personas):" />
            <asp:BoundField DataField="disponibilidad" HeaderText="Disponibilidad:" />
            <asp:BoundField DataField="activo" HeaderText="Activo" />
            
            <asp:TemplateField>
                <HeaderTemplate>Acciones</HeaderTemplate>
                <ItemTemplate>
                    <a href="FormularioMesa.aspx?id=<%# Eval("id") %>" class="btn btn-primary btn-sm">Ver/Modificar</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>

</asp:Content>
