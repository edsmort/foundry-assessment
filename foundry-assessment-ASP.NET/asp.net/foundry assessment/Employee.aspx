<%@ Page Title="Employees" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="foundry_assessment.Employee" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Employees</h2>
    <br />
    <div>
        <asp:Panel ID="pnlAddEmployee" runat="server" Width="100%" Height="10%">
            <asp:Label runat="server" Text="Enter Employee Name"></asp:Label>
            <br />
            <asp:TextBox ID="addName" runat="server" Width="157px"></asp:TextBox>
        </asp:Panel>
    </div>
    <br />
    <div>
        <asp:Button ID="btnAddEmployee" CssClass="btn btn-primary" Text="Add New Employee" runat="server" BorderSpacing="0" OnClick="btnAddEmployee_Click" />
    </div>
    <br />
    <asp:GridView ID="gvEmployees" CssClass="table table-striped color-table" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" 
        OnRowEditing="gvEmployees_RowEditing" OnRowCancelingEdit="gvEmployees_RowCancelingEdit" 
        OnRowDeleting="gvEmployees_RowDeleting" OnRowUpdating="gvEmployees_RowUpdating">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="Id" ReadOnly="true" />
            <asp:BoundField DataField="name" HeaderText="Name" />
            <asp:CommandField ShowEditButton="true" HeaderText="Edit" />
            <asp:CommandField ShowDeleteButton="true" HeaderText="Delete" />
        </Columns>
        <EmptyDataTemplate>No records available</EmptyDataTemplate>
    </asp:GridView>
</asp:Content>
