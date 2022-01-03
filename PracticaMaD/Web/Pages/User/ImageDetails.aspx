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
        <asp:HyperLink ID="txtUser" runat="server" ForeColor="Black" meta:resourcekey="txtUser"></asp:HyperLink>
        <br />
        <asp:Label class="display-1" ID="lablLikes" runat="server" meta:resourcekey="lablLikes"></asp:Label>
        <br />
        <asp:Button class="btn btn-outline-danger" ID="likeButton" OnClick="BtnLikeClick" runat="server" Text="🖤" />
        <br />
        <asp:Label class="display-1" ID="labldescription" runat="server" meta:resourcekey="lblDescription"></asp:Label>
        <br />
        <asp:GridView ID="gvTags" runat="server" CssClass="userFollows"
                    AutoGenerateColumns="False"
                    OnPageIndexChanging="gvTagsPageIndexChanging"
                    ShowHeaderWhenEmpty="True"
                    Font-Size="14"
                    BorderStyle="None">
                    <Columns>
                        <asp:HyperLinkField
                                        DataNavigateUrlFields="tagid"
                                        DataNavigateUrlFormatString="~/Pages/User/TagImages.aspx?ID={0}"
                                        DataTextField="tagname"
                                        HeaderText="Tag List"
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
        <asp:HyperLink ID="CommentsLink" ForeColor="Black" runat="server" meta:resourcekey="CommentsLink"></asp:HyperLink>
        <br />
        <br />
        <asp:Button class="btn btn-outline-dark" ID="btnAddComment" OnClick="BtnAddComment" runat="server"  meta:resourcekey="btnComment" />
        <br />
        <asp:Label ID="txtF" runat="server" meta:resourcekey="txtF"></asp:Label>
        <br />
        <asp:Label ID="txtT" runat="server" meta:resourcekey="txtT"></asp:Label>
        <br />
        <asp:Label ID="txtISO" runat="server" meta:resourcekey="txtISO"></asp:Label>
        <br />
        <asp:Label ID="txtWB" runat="server" meta:resourcekey="txtWB"></asp:Label>
        <br />
        <asp:Button class="btn btn-danger" ID="btnDelete" runat="server" Text="Delete Image" OnClick="BtnDeleteClick" meta:resourcekey="btnDelete"/>
</asp:Content>
