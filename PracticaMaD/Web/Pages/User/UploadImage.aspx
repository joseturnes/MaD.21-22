<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="UploadImage.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.UploadImage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    
    <div class="container">
        <div class="row">
            <center>
            <div class="col-md-4 col-md-offset-4">
                
                <asp:Label ID="lblAddedImage" runat="server" meta:resourcekey="lblAddedImage"></asp:Label>
                <br />
                <asp:Image ID="imagePreview" Width="400" ImageUrl="https://icons.iconarchive.com/icons/papirus-team/papirus-apps/512/upload-pictures-icon.png" runat="server" />
                <br />
                <br />
                <asp:Label ID="lblFile" runat="server" meta:resourcekey="lblFile"></asp:Label>
                <asp:FileUpload ID="fuploadImage" accept=".jpg" runat="server" CssClass="form-control" required/>
                <br />
                <br />
                <asp:DropDownList class="form-select" data-width="fit" ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" required>
                    <asp:ListItem Selected="True">Select a Category</asp:ListItem>
                    <asp:ListItem>Retrato</asp:ListItem>
                    <asp:ListItem>Paisaje Nocturno</asp:ListItem>
                    <asp:ListItem>Paisaje</asp:ListItem>
                    <asp:ListItem>Ciudades</asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
                <asp:Label ID="lblTittle" runat="server" meta:resourcekey="lblTittle"></asp:Label>
                <asp:TextBox class="form-control" ID="txtTitle" runat="server" placeholder="Title" required></asp:TextBox>
                <br />
                <asp:Label ID="lblDescription" runat="server" meta:resourcekey="lblDescription"></asp:Label>
                <asp:TextBox class="form-control" ID="txtDescription" runat="server" placeholder="Description" required></asp:TextBox>
                <br />
                <asp:Label ID="lblTags" runat="server" meta:resourcekey="lblTags"></asp:Label>
                <asp:TextBox class="form-control" ID="txtTags" runat="server" placeholder="Tags"></asp:TextBox>
                <br />
               <asp:Label ID="lblF" runat="server" meta:resourcekey="lblF"></asp:Label>
                <asp:TextBox class="form-control" type="number" ID="txtF" runat="server" placeholder="F"></asp:TextBox>
                <br />
                <asp:Label ID="lblT" runat="server" meta:resourcekey="lblT"></asp:Label>
                <asp:TextBox class="form-control" type="number" ID="txtT" runat="server"  placeholder="T"></asp:TextBox>
                <br />
                <asp:Label ID="lblISO" runat="server" meta:resourcekey="lblISO"></asp:Label>
                <asp:TextBox class="form-control" ID="txtISO" runat="server" placeholder="ISO"></asp:TextBox>
                <br />
                <asp:Label ID="lblWB" runat="server" meta:resourcekey="lblWB"></asp:Label>
                <asp:TextBox class="form-control" ID="txtWB" runat="server" placeholder="WB"></asp:TextBox>
                <br />
                <asp:Button ID="btnUpload" runat="server" Text="Upload Image" CssClass="btn btn-success" OnClick="btnUpload_Click" />
                <asp:Button ID="btnReturn" runat="server" Text="Return to Profile" CssClass="btn btn-success" OnClick="btnPerfil_Click" />
            </div>
          </center>
        </div>
    </div>
</asp:Content>

