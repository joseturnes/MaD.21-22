<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="Register.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.Register"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">
    -
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <div id="form">

            <div class="field">
                <asp:HyperLink ID="lnkUserExists" runat="server" meta:resourcekey="lnkUserExists" NavigateUrl="~/Pages/UserExists.aspx"></asp:HyperLink>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclUserName" runat="server" meta:resourcekey="lclUserName" />
                </span><span
                        class="entry">
                        <asp:TextBox class="form-control" ID="txtLogin" runat="server" Width="100px" Columns="16"
                            meta:resourcekey="txtLoginResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtLogin"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvUserNameResource1"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblLoginError" runat="server" ForeColor="Red" Style="position: relative"
                            Visible="False" meta:resourcekey="lblLoginError"></asp:Label></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclPassword" runat="server" meta:resourcekey="lclPassword" /></span><span
                        class="entry">
                        <asp:TextBox class="form-control" TextMode="Password" ID="txtPassword" runat="server"
                            Width="100px" Columns="16" meta:resourcekey="txtPasswordResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvPasswordResource1"></asp:RequiredFieldValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclRetypePassword" runat="server" meta:resourcekey="lclRetypePassword" /></span><span
                        class="entry">
                        <asp:TextBox class="form-control" TextMode="Password" ID="txtRetypePassword" runat="server" Width="100px"
                            Columns="16" meta:resourcekey="txtRetypePasswordResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvRetypePassword" runat="server" ControlToValidate="txtRetypePassword"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvRetypePasswordResource1"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cvPasswordCheck" runat="server" ControlToCompare="txtPassword"
                            ControlToValidate="txtRetypePassword" meta:resourcekey="cvPasswordCheck"></asp:CompareValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclFirstName" runat="server" meta:resourcekey="lclFirstName" /></span><span
                        class="entry">
                        <asp:TextBox class="form-control" ID="txtFirstName" runat="server" Width="100px"
                            Columns="16" meta:resourcekey="txtFirstNameResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvFirstNameResource1"></asp:RequiredFieldValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclSurname" runat="server" meta:resourcekey="lclSurname" /></span><span
                        class="entry">
                        <asp:TextBox class="form-control" ID="txtSurname" runat="server" Width="100px" Columns="16"
                            meta:resourcekey="txtSurnameResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvSurname" runat="server" ControlToValidate="txtSurname"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvSurnameResource1"></asp:RequiredFieldValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclEmail" runat="server" meta:resourcekey="lclEmail" /></span><span
                        class="entry">
                        <asp:TextBox class="form-control" ID="txtEmail" runat="server" Width="100px" Columns="16"
                            meta:resourcekey="txtEmailResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvEmailResource1"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                            Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            meta:resourcekey="revEmail"></asp:RegularExpressionValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclLanguage" runat="server" meta:resourcekey="lclLanguage" /></span><span
                        class="entry">
                        <asp:DropDownList class="form-select" ID="comboLanguage" runat="server" AutoPostBack="True"
                            Width="100px" meta:resourcekey="comboLanguageResource1"
                            OnSelectedIndexChanged="ComboLanguageSelectedIndexChanged">
                        </asp:DropDownList></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCountry" runat="server" meta:resourcekey="lclCountry" /></span><span
                        class="entry">
                        <asp:DropDownList class="form-select" ID="comboCountry" runat="server" Width="100px"
                            meta:resourcekey="comboCountryResource1">
                        </asp:DropDownList></span>
            </div>
            <div class="button">
                <asp:Button class="btn btn-info" ID="btnRegister" runat="server" OnClick="BtnRegisterClick" meta:resourcekey="btnRegister" />
            </div>
    </div>
</asp:Content>