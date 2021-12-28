<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="CommentDetails.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.CommentDetails" meta:resourcekey="Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">
    - <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
        <div>
        <asp:Label class="display-1" ID="lablTitle" runat="server"></asp:Label>
        <br />
        <asp:Image ID="Image1" runat="server" width="200" height="200" />
        <br />
        <div>
            <p>
            <asp:Label ID="lblInvalidUser" meta:resourcekey="lblInvalidUser" runat="server" Visible="false"></asp:Label>
            </p>
            <div class="row">
                <div class="col-sm">
                    <center>
                        <form runat="server">
                            <asp:GridView ID="gvComments" runat="server" CssClass="userFollows" Visible="true"
                                    AutoGenerateColumns="False"
                                    OnPageIndexChanging="gvCommentsPageIndexChanging"
                                    ShowHeaderWhenEmpty="True">
                                <Columns>
                                    <asp:HyperLinkField
                                        DataNavigateUrlFields="UserId"
                                        DataNavigateUrlFormatString="userProfile.aspx?ID={0}"
                                        DataTextField="UserName"
                                        HeaderText="User Name"
                                        SortExpression="UserName" />
                                    <asp:BoundField DataField="Content" HeaderText="Comment" />
                                    <asp:BoundField DataField="ComDate" HeaderText="Publication Date" />
                                </Columns>
                            </asp:GridView>
                        </form>
                    </center>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
