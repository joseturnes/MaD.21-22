using System;

using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web
{

    public partial class PracticaMaD : System.Web.UI.MasterPage
    {

        public static readonly String USER_SESSION_ATTRIBUTE = "userSession";

        protected void Page_Load(object sender, EventArgs e)
        {
            lblTitle.NavigateUrl = "~/Pages/MainPage.aspx";

            if (!SessionManager.IsUserAuthenticated(Context))
            {

                if (lblDash2 != null)
                    lblDash2.Visible = false;
                if (lnkUpdate != null)
                    lnkUpdate.Visible = false;
                if (lblDash3 != null)
                    lblDash3.Visible = false;
                if (lblDash4 != null)
                    lblDash4.Visible = false;
                if (lnkLogout != null)
                    lnkLogout.Visible = false;
                if (lnkPerfil != null)
                    lnkPerfil.Visible = false;

            }
            else
            {
                if (lblWelcome != null)                   
                    lblWelcome.Text =
                        GetLocalResourceObject("lblWelcome.Hello.Text").ToString()
                        + " " + SessionManager.GetUserSession(Context).FirstName;
                if (lblDash1 != null)
                    lblDash1.Visible = false;
                if (lnkAuthenticate != null)
                    lnkAuthenticate.Visible = false;
            }
        }
    }
}
