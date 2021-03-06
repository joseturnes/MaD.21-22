using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using System;
using System.Data;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{

    public partial class Perfil : SpecificCulturePage
    {
        private ObjectDataSource pbpDataSource = new ObjectDataSource();
        protected void Page_Load(object sender, EventArgs e)
        {
            Int64 userId = SessionManager.GetUserId(Context);

            if (SessionManager.IsUserAuthenticated(Context))
            {
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IUserService userService = iocManager.Resolve<IUserService>();

                int numFollows = userService.GetNumberOfFollows(userId);
                int numFollowers = userService.GetNumberOfFollowers(userId);
                if (!Page.IsPostBack)
                {
                    btnFollows.Text = btnFollows.Text + " (" + numFollows.ToString() + ") ";
                    btnFollowers.Text = btnFollowers.Text + " (" + numFollowers.ToString() + ") ";
                }

                try
                {
                    pbpDataSource.ObjectCreating += this.PbpDataSource_ObjectCreating;

                    pbpDataSource.TypeName =
                            Settings.Default.ObjectDS_Image_Service;

                    pbpDataSource.EnablePaging = true;

                    pbpDataSource.SelectMethod =
                        Settings.Default.ObjectDS_Image_SelectMethod;


                    pbpDataSource.SelectParameters.Add("userId", DbType.Int64, userId.ToString());

                    pbpDataSource.SelectCountMethod =
                        Settings.Default.ObjectDS_Images_CountMethod;
                    pbpDataSource.StartRowIndexParameterName =
                        Settings.Default.ObjectDS_User_StartIndexParameter;
                    pbpDataSource.MaximumRowsParameterName =
                        Settings.Default.ObjectDS_User_CountParameter;

                    gvUploads.AllowPaging = true;
                    gvUploads.PageSize = Settings.Default.PracticaMaD_defaultCount;

                    gvUploads.DataSource = pbpDataSource;
                    gvUploads.DataBind();
                }
                catch (TargetInvocationException)
                {
                    lblInvalidUser.Visible = true;
                }
            }
            else
            {
                String url = String.Format("./Authentication.aspx");
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }
        }



        protected void gvFollowsPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUploads.PageIndex = e.NewPageIndex;
            gvUploads.DataBind();
        }

        protected void PbpDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IImageUploadService userService = iocManager.Resolve<IImageUploadService>();

            e.ObjectInstance = userService;
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

        protected void BtnSearchFollowersClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                /* Get data. */
                Int64 userId = SessionManager.GetUserId(Context);

                String url = String.Format("./Followers.aspx?userId={0}", userId);
                Response.Redirect(Response.ApplyAppPathModifier(url));

            }
        }

        protected void BtnUploadImageClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                /* Get data. */
                Int64 userId = SessionManager.GetUserId(Context);

                String url = String.Format("./UploadImage.aspx?userId={0}", userId);
                Response.Redirect(Response.ApplyAppPathModifier(url));

            }
        }

        protected void gvUploads_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ImageClick(object sender, EventArgs e)
        {
            String url = String.Format("./MainPage.aspx");
            Response.Redirect(Response.ApplyAppPathModifier(url));
        }

    }
}



