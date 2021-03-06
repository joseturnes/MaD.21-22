<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="UserExists.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.UserExists" meta:resourcekey="PageResource2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:HyperLink ID="lnkMainPage" runat="server" NavigateUrl="~/Pages/MainPage.aspx" meta:resourcekey="lnkMainPageResource1">MainPage</asp:HyperLink> - <asp:Label ID="lblSearchUser" runat="server" Text="<%$Resources: , lblExplanation_txt %>"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <center>
            <div class="w-25 p-3">
                <div>
                    <asp:Label ID="lblUserName" runat="server" meta:resourcekey="lblUserNameResource1"></asp:Label>
                    <br />
                    <asp:TextBox class="form-control" ID="txtUserName" runat="server" meta:resourcekey="txtUserNameResource1" required></asp:TextBox>
                    <br />
                    <asp:Label ID="lblUserExists" runat="server" meta:resourcekey="lblUserExistsResource1" Visible="False"></asp:Label>
                    <br />
                    <asp:Label ID="lblUserNotExists" runat="server" meta:resourcekey="lblUserNotExistsResource1" Visible="False"></asp:Label>
                    <br />
                    <asp:Button class="btn btn-info" ID="btnUserExists" runat="server" OnClick="btnUserExists_Click" Text="<%$Resources: Common , searchButton %>" />          
                </div>
    </center>
</asp:Content>