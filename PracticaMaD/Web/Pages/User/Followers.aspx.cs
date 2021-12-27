using System;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using System.Data;
using System.Reflection;
using System.Web;
using Es.Udc.DotNet.ModelUtil.IoC;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{

    public partial class Followers : SpecificCulturePage
    {

        private ObjectDataSource pbpDataSource = new ObjectDataSource();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // ObjectCreating is executed before ObjectDataSource creates
                // an instance of the type used as DataSource (UserService).
                // We need to intercept this call to replace the standard creation
                // procedure (a new UserService() sentence) to use the Unity
                // Container that allows to complete the dependences (accountDao,...)
                pbpDataSource.ObjectCreating += this.PbpDataSource_ObjectCreating;

                pbpDataSource.TypeName =
                     Settings.Default.ObjectDS_User_Service;

                pbpDataSource.EnablePaging = true;

                pbpDataSource.SelectMethod =
                    Settings.Default.ObjectDS_User_Followers_SelectMethod;

                /* Get Account Identifier */
                long userID = Convert.ToInt64(Request.Params.Get("userId"));


                pbpDataSource.SelectParameters.Add("userId", DbType.Int64, userID.ToString());

                pbpDataSource.SelectCountMethod =
                    Settings.Default.ObjectDS_Followers_CountMethod;
                pbpDataSource.StartRowIndexParameterName =
                    Settings.Default.ObjectDS_User_StartIndexParameter;
                pbpDataSource.MaximumRowsParameterName =
                    Settings.Default.ObjectDS_User_CountParameter;

                gvFollowers.AllowPaging = true;
                gvFollowers.PageSize = Settings.Default.PracticaMaD_defaultCount;

                gvFollowers.DataSource = pbpDataSource;
                gvFollowers.DataBind();
            }
            catch (TargetInvocationException)
            {
                lblInvalidUser.Visible = true;
            }
        }

        protected void gvFollowsPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFollowers.PageIndex = e.NewPageIndex;
            gvFollowers.DataBind();
        }

        protected void PbpDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();

            e.ObjectInstance = userService;
        }
    }
}