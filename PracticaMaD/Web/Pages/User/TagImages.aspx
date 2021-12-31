<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    Codebehind="TagImages.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.TagService"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">
    - <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
        <div>
            <p>
            <asp:Label ID="lblInvalidUser" meta:resourcekey="lblInvalidUser" runat="server" Visible="false"></asp:Label>
            </p>
            <div class="gv">
                <center>
                    <asp:GridView ID="gvTagImages" runat="server"
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
                            <asp:BoundField DataField="title" HeaderText="Title" />
                        </Columns>
                        </asp:GridView>
                </center>
            </div>
        </div>
    
    
</asp:Content>
