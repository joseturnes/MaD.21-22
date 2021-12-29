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
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{

    public partial class PerfilCargado : SpecificCulturePage
    {
        private ObjectDataSource pbpDataSource = new ObjectDataSource();
        protected void Page_Load(object sender, EventArgs e)
        {
            Int64 userId = Convert.ToInt64(Request.Params.Get("ID"));
            Int64 pageOwner = SessionManager.GetUserId(Context);


            if (!(SessionManager.GetUserId(Context) == Convert.ToInt64(Request.Params.Get("ID"))))
            {
                btnUploadImage.Visible = false;
            }
            if (SessionManager.GetUserId(Context) == Convert.ToInt64(Request.Params.Get("ID")))
            {
                FollowButton.Visible = false;
            }
            
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();

            if ((SessionManager.GetUserId(Context) != Convert.ToInt64(Request.Params.Get("ID"))) && !userService.isFollowed(userId,pageOwner))
            {
                FollowButton.Visible = true;
                FollowButton.Text = "Already Followed";
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
                Int64 userId = Convert.ToInt64(Request.Params.Get("ID"));

                String url = String.Format("./Follows.aspx?userId={0}", userId);
                Response.Redirect(Response.ApplyAppPathModifier(url));

            }
        }

        protected void BtnSearchFollowersClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                /* Get data. */
                Int64 userId = Convert.ToInt64(Request.Params.Get("ID"));

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

        protected void BtnFollowClick(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();
            Int64 userId = SessionManager.GetUserId(Context);
            Int64 ID = Convert.ToInt64(Request.Params.Get("ID"));
            string login1 = userService.findUserNameById(userId);
            string login2 = userService.findUserNameById(ID);
            userService.follow(login1, login2);
        }

        protected void ImageClick(object sender, EventArgs e)
        {

            String url = String.Format("./MainPage.aspx");
            Response.Redirect(Response.ApplyAppPathModifier(url));
        }

    }
}



