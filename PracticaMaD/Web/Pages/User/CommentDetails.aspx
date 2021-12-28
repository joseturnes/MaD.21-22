<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="CommentDetails.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.CommentDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <asp:Label class="display-1" ID="lablTitle" runat="server"></asp:Label>
    <br />
    <asp:Image ID="Image1" runat="server" width="200" height="200" />
    <br />
    <p>
    <asp:Label ID="lblInvalidUser" meta:resourcekey="lblInvalidUser" runat="server" Visible="false"></asp:Label>
    </p>
    <center>
        <form runat="server">
            <asp:GridView ID="gvComments" runat="server" CssClass="userFollows"
                    AutoGenerateColumns="False"
                    OnPageIndexChanging="gvCommentsPageIndexChanging"
                    ShowHeaderWhenEmpty="True">
                <Columns>
                    <asp:BoundField DataField="userName" HeaderText="UserName" />
                    <asp:BoundField DataField="comDate" HeaderText="ComDate" />
                    <asp:BoundField DataField="content" HeaderText="Content" />
                </Columns>
            </asp:GridView>
        </form>
    </center>
</asp:Content>
