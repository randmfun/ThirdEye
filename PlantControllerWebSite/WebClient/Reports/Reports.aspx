<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Reports.aspx.cs" Inherits="WebClient.Reports.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="clear hideSkiplink">
        <asp:LoginView ID="LoginView1" runat="server" EnableViewState="false">
            <LoggedInTemplate>
               <asp:Menu ID="NavigationMenu" runat="server" CssClass="menu" EnableViewState="false"
                            IncludeStyleBlock="false" Orientation="Horizontal"
                            OnMenuItemClick="NavigationMenu_MenuItemClick" >
                    <Items>
                        <asp:MenuItem Text="Users" Value="Projekty"></asp:MenuItem>
                        <asp:MenuItem Text="Licencje" Value="Licencje"></asp:MenuItem>
                        <asp:MenuItem Text="Kontrahenci" Value="Kontrahenci"></asp:MenuItem>
                    </Items>
                </asp:Menu>
            </LoggedInTemplate>
        </asp:LoginView>
    </div>
</asp:Content>
