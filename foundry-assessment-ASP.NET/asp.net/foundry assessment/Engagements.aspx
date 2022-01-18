<%@ Page Title="Engagements" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Engagements.aspx.cs" Inherits="foundry_assessment.Engagements" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Engagements</h2>
    <br />
    <div>
        <asp:Panel ID="pnlAddEngagement" runat="server" Width="100%" Height="10%">
            <asp:Label runat="server" Text="Enter Engagement Name *"></asp:Label>
            <br />
            <asp:TextBox ID="txtEngagement" runat="server" Width="157px"></asp:TextBox>
            <br />
            <asp:Label runat="server" Text="Select Client *"></asp:Label>
            <br />
            <asp:DropDownList ID="ddlClient" runat="server" Width="157px"></asp:DropDownList>
            <br />
            <asp:Label runat="server" Text="Select Employee *"></asp:Label>
            <br />
            <asp:DropDownList ID="ddlEmployee" runat="server" Width="157px"></asp:DropDownList>
            <br />
            <asp:Label runat="server" Text="Enter Description"></asp:Label>
            <br />
            <asp:TextBox ID="txtDescription" runat="server" Width="157px"></asp:TextBox>
        </asp:Panel>
    </div>
    <br />
    <div>
        <asp:Button ID="btnAddEngagement" CssClass="btn btn-primary" Text="Add New Engagement" runat="server" BorderSpacing="0" OnClick="btnAddEngagement_Click" />
    </div>
    <br />
    <div>
        <asp:Label runat="server" Text="Search"></asp:Label>
        <br />
        <asp:TextBox ID="txtSearch" runat="server" OnTextChanged="txtSearch_TextChanged" AutoPostBack="true" Width="155px"></asp:TextBox>
    </div>
    <br />
    <asp:GridView ID="gvEngagements" CssClass="table table-striped" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" 
        OnRowEditing="gvEngagements_RowEditing" OnRowCancelingEdit="gvEngagements_RowCancelingEdit" 
        OnRowUpdating="gvEngagements_RowUpdating" OnRowDeleting="gvEngagements_RowDeleting" 
        OnRowCommand="gvEngagements_RowCommand" OnRowDataBound="gvEngagements_RowDataBound">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="true" />
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Client" HeaderText="Client" ReadOnly="true" />
            <asp:BoundField DataField="Employee" HeaderText="Employee" ReadOnly="true" />
            <asp:BoundField DataField="Description" HeaderText="Description" />
            <asp:BoundField DataField="Started" HeaderText="Started" ReadOnly="true" />
            <asp:BoundField DataField="Ended" HeaderText="Ended" ReadOnly="true" />
            <asp:ButtonField ButtonType="Link" HeaderText="End" CommandName="EndEngagement" Text="End" />
            <asp:CommandField ShowEditButton="true" HeaderText="Edit"/>
            <asp:CommandField ShowDeleteButton="true" HeaderText="Delete"/>
        </Columns>
        <EmptyDataTemplate>No records available</EmptyDataTemplate>
    </asp:GridView>
</asp:Content>
