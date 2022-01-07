using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Web.Security;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class Authentication : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblPasswordError.Visible = false;
            lblLoginError.Visible = false;
        }

        /// <summary>
        /// Handles the Click event of the btnLogin control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance
        /// containing the event data.</param>
        protected void BtnLoginClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                Int64 imgId = Convert.ToInt64(Request.Params.Get("ID"));
                if (imgId == 0)
                {
                    try
                    {
                        SessionManager.Login(Context, txtLogin.Text,
                            txtPassword.Text, checkRememberPassword.Checked);

                        FormsAuthentication.
                            RedirectFromLoginPage(txtLogin.Text,
                                checkRememberPassword.Checked);

                        Int64 userId = SessionManager.GetUserId(Context);

                        String url = String.Format("./Perfil.aspx?userId={0}", userId);
                        Response.Redirect(Response.ApplyAppPathModifier(url));
                    }
                    catch (InstanceNotFoundException)
                    {
                        lblLoginError.Visible = true;
                    }
                    catch (IncorrectPasswordException)
                    {
                        lblPasswordError.Visible = true;
                    }
                }
                else
                {
                    try
                    {
                        SessionManager.Login(Context, txtLogin.Text,
                            txtPassword.Text, checkRememberPassword.Checked);

                        FormsAuthentication.
                            RedirectFromLoginPage(txtLogin.Text,
                                checkRememberPassword.Checked);

                        Int64 userId = SessionManager.GetUserId(Context);

                        String url = String.Format("./ImageDetails.aspx?imgId={0}", imgId);
                        Response.Redirect(Response.ApplyAppPathModifier(url));
                    }
                    catch (InstanceNotFoundException)
                    {
                        lblLoginError.Visible = true;
                    }
                    catch (IncorrectPasswordException)
                    {
                        lblPasswordError.Visible = true;
                    }

                }
            }
        }
    }
}