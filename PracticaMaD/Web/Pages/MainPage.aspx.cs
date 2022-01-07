using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using System;
using System.Data;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages
{
    public partial class MainPage : SpecificCulturePage
    {
        private ObjectDataSource pbpDataSource = new ObjectDataSource();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                pbpDataSource.ObjectCreating += this.PbpDataSource_ObjectCreating;

                pbpDataSource.TypeName =
                        Settings.Default.ObjectDS_Image_Service;

                pbpDataSource.EnablePaging = false;

                pbpDataSource.SelectMethod =
                    Settings.Default.ObjectDS_Recent_Image_SelectMethod;

                pbpDataSource.SelectCountMethod =
                    Settings.Default.ObjectDS_Recent_Images_CountMethod;

                lvRecentUploads.DataSource = pbpDataSource;
                lvRecentUploads.DataBind();

                if (lvRecentUploads.Items.Count <= 0)
                    lblRecentUploads.Visible = false;
            }
            catch (TargetInvocationException)
            {
            }

            
        }

        protected void PbpDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IImageUploadService userService = iocManager.Resolve<IImageUploadService>();

            e.ObjectInstance = userService;
        }

    }
}