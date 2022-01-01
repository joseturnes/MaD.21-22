<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    Codebehind="EditTags.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.EditTags"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">
    - <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <div id="form">
            <div class="field">
                        <asp:Label ID="lblEditTags" runat="server" meta:resourcekey="lblEditTags"></asp:Label>
                        <asp:TextBox class="form-control" ID="txtTags" runat="server" ></asp:TextBox>                   
            </div>
            <div class="button">
                <asp:Button class="btn btn-primary" ID="btnComment" runat="server" OnClick="BtnEditClick" Text="Modify" />
            </div>
    </div>
</asp:Content>
