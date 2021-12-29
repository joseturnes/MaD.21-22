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
    
    <div class="container">
        <div class="row">
            <center>
            <div class="col-md-4 col-md-offset-4">
                Imagen agregada:
                <br />
                <asp:Image ID="imagePreview" Width="400" ImageUrl="https://icons.iconarchive.com/icons/papirus-team/papirus-apps/512/upload-pictures-icon.png" runat="server" />
                <br />
                <br />
                Archivo:
                <asp:FileUpload ID="fuploadImage" accept=".jpg" runat="server" CssClass="form-control"/>
                <br />
                <br />
                <asp:DropDownList class="form-select" data-width="fit" ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                    <asp:ListItem Selected="True">Select a Category</asp:ListItem>
                    <asp:ListItem>Retrato</asp:ListItem>
                    <asp:ListItem>Paisaje Nocturno</asp:ListItem>
                    <asp:ListItem>Paisaje</asp:ListItem>
                    <asp:ListItem>Ciudades</asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
                Title:
                <asp:TextBox class="form-control" ID="txtTitle" runat="server"></asp:TextBox>
                <br />
                Descripcion:
                <asp:TextBox class="form-control" ID="txtDescription" runat="server"></asp:TextBox>
                <br />
                Tags:
                <asp:TextBox class="form-control" ID="txtTags" runat="server"></asp:TextBox>
                <br />
                F:
                <asp:TextBox class="form-control" type="number" ID="txtF" runat="server"></asp:TextBox>
                <br />
                T:
                <asp:TextBox class="form-control" type="number" ID="txtT" runat="server"></asp:TextBox>
                <br />
                ISO:
                <asp:TextBox class="form-control" ID="txtISO" runat="server"></asp:TextBox>
                <br />
                WB:
                <asp:TextBox class="form-control" ID="txtWB" runat="server"></asp:TextBox>
                <br />
                <asp:Button ID="btnUpload" runat="server" Text="Upload Image" CssClass="btn btn-success" OnClick="btnUpload_Click" />
                <asp:Button ID="btnReturn" runat="server" Text="Return to Profile" CssClass="btn btn-success" OnClick="btnPerfil_Click" />
            </div>
        </div>
    </div>
        
  </form>
</asp:Content>

