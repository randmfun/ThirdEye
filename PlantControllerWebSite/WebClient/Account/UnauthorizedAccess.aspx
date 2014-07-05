<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="UnauthorizedAccess.aspx.cs" Inherits="WebClient.Account.UnauthorizedAccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Unauthorized Access</h2>
    <p>
        You have attempted to access a page that you are not authorized to view.
    </p>
    <p>
        If you have any questions, please contact the site administrator.
    </p>
</asp:Content>
