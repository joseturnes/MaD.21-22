<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="MainPage.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.MainPage" meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <br />
    <br />
    <br />
    <center>
        <asp:Label ID="lblRecentUploads" runat="server" meta:resourcekey="lblRecentUploads"></asp:Label>
        <br />
    </center>
    <div class="gv">
            <asp:ListView ID="lvRecentUploads" runat="server">
                <ItemTemplate>                           
                    <a href="User/ImageDetails.aspx?imgId=<%# Eval("ImgId")%>">
                        <img width="400" height="400" src="data:image/jpg;base64,<%# Convert.ToBase64String((byte[])Eval("UploadedImage"))%>"/>
                    </a>
                </ItemTemplate>
        </asp:ListView>
    </div>
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>