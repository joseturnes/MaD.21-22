<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    Codebehind="Follows.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.Follows"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">
    - <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
     <p>
        <asp:Label ID="lblInvalidUser" meta:resourcekey="lblInvalidUser" runat="server" Visible="false"></asp:Label>
    </p>
    <center>
            <asp:GridView ID="gvFollows" runat="server" CssClass="userFollows"
                AutoGenerateColumns="False"
                OnPageIndexChanging="gvFollowsPageIndexChanging"
                ShowHeaderWhenEmpty="True">
                <Columns>
                    <asp:HyperLinkField
                        DataNavigateUrlFields="UserId"
                        DataNavigateUrlFormatString="PerfilCargado.aspx?ID={0}"
                        DataTextField="UserName"
                        HeaderText="User Name"
                        SortExpression="UserName" />
                </Columns>
            </asp:GridView>
    </center>
</asp:Content>
