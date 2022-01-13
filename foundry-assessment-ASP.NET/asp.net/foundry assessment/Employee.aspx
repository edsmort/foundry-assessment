<%@ Page Title="Employees" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="foundry_assessment.Employee" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Employees</h2>
    <asp:GridView ID="gvEmployees" CssClass="table table-striped color-table" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="Id" />
            <asp:BoundField DataField="name" HeaderText="Name" />
            <asp:CommandField ShowEditButton="true" HeaderText="Edit" />
            <asp:CommandField ShowDeleteButton="true" HeaderText="Delete" />
        </Columns>
        <EmptyDataTemplate>No records available</EmptyDataTemplate>
    </asp:GridView>
</asp:Content>
