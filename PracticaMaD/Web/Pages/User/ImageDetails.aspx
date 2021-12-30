<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ImageDetails.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.ImageDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
        <asp:Label class="display-1" ID="lablTitle" runat="server"></asp:Label>
        <br />
        <asp:Image ID="Image1" runat="server" width="400" height="400" />
        <br />
        <asp:Label class="display-1" ID="lablLikes" runat="server"></asp:Label>
        <br />
        <asp:Button class="btn btn-outline-danger" ID="likeButton" OnClick="BtnLikeClick" runat="server" Text="❤️" />
        <br />
        <asp:Label class="display-1" ID="labldescription" runat="server"></asp:Label>
        <br />
        <asp:HyperLink ID="CommentsLink" runat="server"> </asp:HyperLink>
        <br />
        <br />
        <asp:Button class="btn btn-outline-dark" ID="btnAddComment" OnClick="BtnAddComment" runat="server" Text="✖️ Comment" />
        <br />
        <br />
        <asp:Button class="btn btn-danger" ID="btnDelete" runat="server" Text="Button" OnClick="BtnDeleteClick" meta:resourcekey="btnDelete"/>
</asp:Content>
