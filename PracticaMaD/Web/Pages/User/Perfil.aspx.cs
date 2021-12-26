using System;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{

    public partial class Perfil : SpecificCulturePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnSearchFollowsClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                    /* Get data. */
                    Int64 userId = SessionManager.GetUserId(Context);
                   
                        String url = String.Format("./Follows.aspx?userId={0}", userId);
                        Response.Redirect(Response.ApplyAppPathModifier(url));
                    
                }
            }
        }
    }

