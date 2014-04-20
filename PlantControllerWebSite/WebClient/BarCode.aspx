<%@ Page Title="Generate Bar Code" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="BarCode.aspx.cs" Inherits="WebClient.BarCode" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Bar Code
    </h2>
    <p>
        <asp:Label ID="lblBinFile" runat="server" Text="Bin File"></asp:Label>
        <asp:TextBox ID="txtBarCodeFile" runat="server"></asp:TextBox>
        
        <br />
        <asp:Button ID="btnGenBarCode" runat="server" Text="Generate" />
        <br />

        <asp:Image ID="imgBarCode" runat="server" />
        
    </p>
</asp:Content>
