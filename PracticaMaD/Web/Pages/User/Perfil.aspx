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
        <center>    
             <div class="button">
                    <asp:Button ID="btnFollows" runat="server" OnClick="BtnSearchFollowsClick" meta:resourcekey="btnFollows" />
                    <asp:Button ID="btnFollowers" runat="server"  OnClick="BtnSearchFollowersClick" meta:resourcekey="btnFollowers" />
                 <asp:Button ID="btnUploadImage" runat="server"  OnClick="BtnUploadImageClick" meta:resourcekey="btnUploadImage" />
                
        </center>
        </div>
        <div>
            <p>
            <asp:Label ID="lblInvalidUser" meta:resourcekey="lblInvalidUser" runat="server" Visible="false"></asp:Label>
            </p>
            <div class="gv">
                <asp:GridView ID="gvUploads" runat="server"
                        AutoGenerateColumns="False"
                        OnPageIndexChanging="gvFollowsPageIndexChanging"
                        ShowHeaderWhenEmpty="True">
                    <Columns>
                        <asp:BoundField DataField="UploadedImage" HeaderText="Title"/>
                        <asp:ImageField DataImageUrlField="UploadedImage" HeaderText="Image"/>
                    </Columns>
                    </asp:GridView>
            </div>

        </div>
    </form>
    
</asp:Content>
