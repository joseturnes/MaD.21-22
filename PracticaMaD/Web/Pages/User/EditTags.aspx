<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    Codebehind="EditTags.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.EditTags"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">
    - <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <div id="form">
            <div class="field">
                        <asp:Label ID="lblEditTags" runat="server" meta:resourcekey="lblEditTags"></asp:Label>
                        <asp:TextBox class="form-control" ID="txtTags" runat="server" meta:resourcekey="txtTags"></asp:TextBox>                   
            </div>
            <div class="button">
                <asp:Button ID="btnModifyTags" class="btn btn-primary" runat="server" OnClick="BtnEditClick" meta:resourcekey="btnModifyTags"/>
            </div>
    </div>
</asp:Content>
