<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ImageDetails.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.ImageDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    - <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
        <asp:Label class="display-1" ID="lablTitle" runat="server"></asp:Label>
        <br />
        <asp:Image ID="Image1" runat="server" width="400" height="400" />
        <br />
        <asp:HyperLink ID="txtUser" runat="server"></asp:HyperLink>
        <br />
        <asp:Label class="display-1" ID="lablLikes" runat="server"></asp:Label>
        <br />
        <asp:Button class="btn btn-outline-danger" ID="likeButton" OnClick="BtnLikeClick" runat="server" Text="❤️" />
        <br />
        <asp:Label class="display-1" ID="labldescription" runat="server"></asp:Label>
        <br />
        <asp:GridView ID="gvTags" runat="server" CssClass="userFollows"
                    AutoGenerateColumns="False"
                    OnPageIndexChanging="gvTagsPageIndexChanging"
                    ShowHeaderWhenEmpty="True"
                    Font-Size="14">
                    <Columns>
                        <asp:HyperLinkField
                                        DataNavigateUrlFields="tagid"
                                        DataNavigateUrlFormatString="~/Pages/User/TagImages.aspx?ID={0}"
                                        DataTextField="tagname"
                                        HeaderText="Tag Name"
                                        SortExpression="tagname" />
                    </Columns>
                    <rowstyle backcolor="#FFFFFF"  
                       forecolor="DarkBlue"
                       font-italic="true"
                       Font-Size="12"/>
                    
                    <alternatingrowstyle backcolor="#FFFFFF"  
                      forecolor="DarkBlue"
                      font-italic="true"
                      Font-Size="12"/>
                </asp:GridView>
        <br />
        <br />
            <asp:Button class="btn btn-outline-dark" ID="EditTagsButton" OnClick="BtnEditTags" runat="server" Text="Edit Tags" />
        <br />
        <asp:HyperLink ID="CommentsLink" runat="server"> </asp:HyperLink>
        <br />
        <br />
        <asp:Button class="btn btn-outline-dark" ID="btnAddComment" OnClick="BtnAddComment" runat="server" Text="✖️ Comment" />
        <br />
        <asp:Label ID="txtF" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="txtT" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="txtISO" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="txtWB" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Button class="btn btn-danger" ID="btnDelete" runat="server" Text="Delete Image" OnClick="BtnDeleteClick" meta:resourcekey="btnDelete"/>
</asp:Content>
