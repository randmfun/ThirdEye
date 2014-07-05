<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"
 CodeBehind="BatchList.aspx.cs" Inherits="WebClient.BatchList1" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="600">
    </rsweb:ReportViewer>
</asp:Content>