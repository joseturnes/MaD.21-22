<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="UploadImage.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.UploadImage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <form id="form1" runat="server">
    <center>
    <div class="container">
        <div class="row">
            <div class="col-md-4 col-md-offset-4">
                Imagen agregada:
                <br />
                <asp:Image ID="imagePreview" Width="200" ImageUrl="https://icons.iconarchive.com/icons/papirus-team/papirus-apps/512/upload-pictures-icon.png" runat="server" />
                <br />
                <br />
                Archivo:
                <asp:FileUpload ID="fuploadImage" accept=".jpg" runat="server" CssClass="form-control"/>
                <br />
                <br />
                Titulo de imagen:
                <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                <br />
                <asp:Button ID="btnUpload" runat="server" Text="Button" CssClass="btn btn-success" OnClick="btnUpload_Click" />
            </div>
        </div>

        <div class="row">
            <asp:Repeater ID="Repeater1" runat="server"></asp:Repeater>
            <ItemTemplate>
                <div class="col-md-4">
                    <asp:Label ID="lblSubject" runat="server" Text='<%#Eval("title") %>' Font-Bold="true"/> 
                </div>
            </ItemTemplate>
        </div>
    </div>
        </center>
  </form>
</asp:Content>
