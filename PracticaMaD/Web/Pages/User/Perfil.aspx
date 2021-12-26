<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    Codebehind="Perfil.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.Perfil"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">
    - <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <form id="form1" runat="server">
    <div id="form">
        
        
         <div class="button">
                <asp:Button ID="btnFollows" runat="server" OnClick="BtnSearchFollowsClick" meta:resourcekey="btnFollows" />
         </div>


       
        
    </div>
    </form>
</asp:Content>
