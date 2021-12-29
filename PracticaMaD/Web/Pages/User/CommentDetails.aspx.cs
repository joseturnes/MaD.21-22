using System;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using System.Data;
using System.Reflection;
using Es.Udc.DotNet.ModelUtil.IoC;
using System.Web;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService;
using Es.Udc.DotNet.PracticaMaD.Model;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class CommentDetails : SpecificCulturePage
    {
        private ObjectDataSource pbpDataSource = new ObjectDataSource();
        protected void Page_Load(object sender, EventArgs e)
        {
            long imgId = Convert.ToInt64(Request.Params.Get("imgId"));

            try
            {
                pbpDataSource.ObjectCreating += this.PbpDataSource_ObjectCreating;

                pbpDataSource.TypeName = Settings.Default.ObjectDS_Image_Service;

                pbpDataSource.EnablePaging = true;

                pbpDataSource.SelectMethod = Settings.Default.ObjectDS_Comments_SelectMethod;

                pbpDataSource.SelectParameters.Add("imgId", DbType.Int64, imgId.ToString());

                pbpDataSource.SelectCountMethod = Settings.Default.ObjectDS_Comments_CountMethod;
                pbpDataSource.StartRowIndexParameterName =
                    Settings.Default.ObjectDS_User_StartIndexParameter;
                pbpDataSource.MaximumRowsParameterName =
                    Settings.Default.ObjectDS_User_CountParameter;

                gvComments.AllowPaging = true;
                gvComments.PageSize = Settings.Default.PracticaMaD_defaultCount;

                gvComments.DataSource = pbpDataSource;
                gvComments.DataBind();
            }
            catch (TargetInvocationException)
            {
                lblInvalidUser.Visible = true;
            }

            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IImageUploadService imageUploadService = iocManager.Resolve<IImageUploadService>();

            ImageUpload image = imageUploadService.findImage(imgId);

            Image1.ImageUrl = "data:image/jpg;base64," + Convert.ToBase64String(image.uploadedImage);
            lablTitle.Text = "<h2>" + image.title + "<h2/>";

            
        }

        protected void gvCommentsPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvComments.PageIndex = e.NewPageIndex;
            gvComments.DataBind();
        }

        protected void PbpDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IImageUploadService imageService = iocManager.Resolve<IImageUploadService>();

            e.ObjectInstance = imageService;
        }
    }
}