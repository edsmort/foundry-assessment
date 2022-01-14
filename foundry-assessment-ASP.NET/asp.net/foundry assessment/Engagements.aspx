<%@ Page Title="Engagements" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Engagements.aspx.cs" Inherits="foundry_assessment.Engagements" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Engagements</h2>
    <asp:GridView ID="gvEngagements" CssClass="table table-striped color-table" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="true" />
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Client" HeaderText="Client" />
            <asp:BoundField DataField="Employee" HeaderText="Employee" />
            <asp:BoundField DataField="Description" HeaderText="Description" />
            <asp:BoundField DataField="Started" HeaderText="Started" />
            <asp:BoundField DataField="Ended" HeaderText="Ended" />
            <asp:CommandField ShowEditButton="true" HeaderText="Edit"/>
            <asp:CommandField ShowDeleteButton="true" HeaderText="Delete"/>
        </Columns>
        <EmptyDataTemplate>No records available</EmptyDataTemplate>
    </asp:GridView>
</asp:Content>
