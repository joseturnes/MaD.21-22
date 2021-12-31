using System;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using System.Data;
using System.Reflection;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService;
using System.Web;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{

    public partial class Perfil : SpecificCulturePage
    {
        private ObjectDataSource pbpDataSource = new ObjectDataSource();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                pbpDataSource.ObjectCreating += this.PbpDataSource_ObjectCreating;

                pbpDataSource.TypeName =
                        Settings.Default.ObjectDS_Image_Service;

                pbpDataSource.EnablePaging = true;

                pbpDataSource.SelectMethod =
                    Settings.Default.ObjectDS_Image_SelectMethod;

                Int64 userId = SessionManager.GetUserId(Context);

                pbpDataSource.SelectParameters.Add("userId", DbType.Int64, userId.ToString());

                pbpDataSource.SelectCountMethod =
                    Settings.Default.ObjectDS_Images_CountMethod;
                pbpDataSource.StartRowIndexParameterName =
                    Settings.Default.ObjectDS_User_StartIndexParameter;
                pbpDataSource.MaximumRowsParameterName =
                    Settings.Default.ObjectDS_User_CountParameter;

                gvUploads.AllowPaging = true;
                gvUploads.PageSize = Settings.Default.PracticaMaD_defaultCount;

                gvUploads.DataSource=pbpDataSource;
                gvUploads.DataBind();
            }
            catch (TargetInvocationException)
            {
                lblInvalidUser.Visible = true;
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



