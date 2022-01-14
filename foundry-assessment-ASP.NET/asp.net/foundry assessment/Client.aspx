<%@ Page Title="Clients" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Client.aspx.cs" Inherits="foundry_assessment.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Clients</h2>
    <div class="row color-table">
        <asp:Button ID="btnAddClient" Text="Add New Client" runat="server" />
    </div>
    <asp:GridView ID="gvClients" CssClass="table table-striped color-table" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" 
        OnRowEditing="gvClients_RowEditing" OnRowDeleting="gvClients_RowDeleting" OnRowUpdating="gvClients_RowUpdating" OnRowCancelingEdit="gvClients_RowCancelingEdit" EnableViewState="false">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="true" />
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:CommandField ShowEditButton="true" HeaderText="Edit"/>
            <asp:CommandField ShowDeleteButton="true" HeaderText="Delete"/>
        </Columns>
        <EmptyDataTemplate>No records available</EmptyDataTemplate>
    </asp:GridView>
</asp:Content>
