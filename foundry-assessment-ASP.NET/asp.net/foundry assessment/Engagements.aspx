﻿<%@ Page Title="Engagements" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Engagements.aspx.cs" Inherits="foundry_assessment.Engagements" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Engagements</h2>
    <table>
        <thead>
            <tr><th>Id</th></tr>
            <tr><th>Name</th></tr>
            <tr><th>Client</th></tr>
            <tr><th>Employee</th></tr>
            <tr><th>Description</th></tr>
            <tr><th>Started</th></tr>
            <tr><th>Ended</th></tr>
        </thead>
        <tbody id="engagements">
        </tbody>
    </table>
</asp:Content>
