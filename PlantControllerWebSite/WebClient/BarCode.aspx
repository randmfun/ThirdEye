<%@ Page Title="Generate Bar Code" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="BarCode.aspx.cs" Inherits="WebClient.BarCode" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Bar Code
    </h2>
    <p>
        &nbsp;<asp:Panel ID="Panel1" runat="server" GroupingText="Select the Log (.bin) File">

            &nbsp;&nbsp;&nbsp;&nbsp; <asp:FileUpload ID="FileUploadControl" runat="server" Width="300" />
            <br />
            <br />

            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnGenBarCode" runat="server" Text="Generate" OnClick="btnGenBarCode_Click" />
        </asp:Panel>

        <asp:Panel ID="Panel2" runat="server" GroupingText="Generated Bar Code Content">
            
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFileName" Text="File Name : " runat="server" Visible="false" />
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblUploadedFileNameContent" Text="" runat="server" />

            <br />
            <br />
            
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblBarCode" Text="Bar Code: " runat="server" Visible="false" />
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblGeneratedBarCodeContent" Text="" runat="server" Visible="false" />
            
            <br />
            <br />

            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Image ID="imgBarCode" runat="server" AlternateText="Image Here" Visible="false"/>
        </asp:Panel>
    </p>
</asp:Content>
