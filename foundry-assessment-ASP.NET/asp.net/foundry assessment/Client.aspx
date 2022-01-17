<%@ Page Title="Clients" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Client.aspx.cs" Inherits="foundry_assessment.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Clients</h2>
    <br />
    <div>
        <asp:Panel ID="pnlAddClient" runat="server" Width="100%" Height="10%">
            <asp:Label runat="server" Text="Enter Client Name"></asp:Label>
            <br />
            <asp:TextBox ID="addName" runat="server" Width="157px"></asp:TextBox>
        </asp:Panel>
    </div>
    <br />
    <div>
        <asp:Button ID="btnAddClient" CssClass="btn btn-primary" Text="Add New Client" runat="server" BorderSpacing="0" OnClick="btnAddClient_Click" />
    </div>
    <br />
    <div>
        <asp:Label runat="server" Text="Search"></asp:Label>
        <br />
        <asp:TextBox ID="txtSearch" runat="server" OnTextChanged="txtSearch_TextChanged" AutoPostBack="true" Width="155px"></asp:TextBox>
    </div>
    <br />
    <asp:GridView ID="gvClients" CssClass="table table-striped color-table" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" 
        OnRowEditing="gvClients_RowEditing" OnRowDeleting="gvClients_RowDeleting" OnRowUpdating="gvClients_RowUpdating" OnRowCancelingEdit="gvClients_RowCancelingEdit" >
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="true" />
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:CommandField ShowEditButton="true" HeaderText="Edit"/>
            <asp:CommandField ShowDeleteButton="true" HeaderText="Delete"/>
        </Columns>
        <EmptyDataTemplate>No records available</EmptyDataTemplate>
    </asp:GridView>
</asp:Content>
