<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    Codebehind="PerfilCargado.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.PerfilCargado"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">
    - <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <form id="form1" runat="server">
        <div id="form">
        <center>    
             <div class="button">
                    <asp:Button class="btn btn-info" ID="btnFollows" runat="server" OnClick="BtnSearchFollowsClick" meta:resourcekey="btnFollows" />
                    <asp:Button class="btn btn-info" ID="btnFollowers" runat="server"  OnClick="BtnSearchFollowersClick" meta:resourcekey="btnFollowers" />
                    <asp:Button class="btn btn-info" ID="btnUploadImage" runat="server"  OnClick="BtnUploadImageClick" meta:resourcekey="btnUploadImage" />
                
        </center>
        </div>
        <div>
            <p>
            <asp:Label ID="lblInvalidUser" meta:resourcekey="lblInvalidUser" runat="server" Visible="false"></asp:Label>
            </p>
            <div class="gv">
                <center>
                    <asp:GridView ID="gvUploads" runat="server"
                            AutoGenerateColumns="False"
                            OnPageIndexChanging="gvFollowsPageIndexChanging"
                            ShowHeaderWhenEmpty="True">
                        <Columns>
                            <asp:TemplateField HeaderText="Publications">
                                    <ItemTemplate>
                                        <a href="ImageDetails.aspx?imgId=<%# Eval("ImgId")%>">
                                            <img width="400" height="400" src="data:image/jpg;base64,<%# Convert.ToBase64String((byte[])Eval("UploadedImage"))%>"/>
                                        </a>
                                        </ItemTemplate>
                            </asp:TemplateField> 
                        </Columns>
                        </asp:GridView>
                </center>
            </div>
        </div>
    </form>
    
    
</asp:Content>
