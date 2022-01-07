using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using System;
using System.Data;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{

    public partial class RenderSearch : SpecificCulturePage
    {
        private ObjectDataSource pbpDataSource = new ObjectDataSource();
        protected void Page_Load(object sender, EventArgs e)
        {
            string keywords = Request.Params.Get("keywords");
            string category = Request.Params.Get("category");

            try
            {
                // ObjectCreating is executed before ObjectDataSource creates
                // an instance of the type used as DataSource (UserService).
                // We need to intercept this call to replace the standard creation
                // procedure (a new UserService() sentence) to use the Unity
                // Container that allows to complete the dependences (accountDao,...)
                pbpDataSource.ObjectCreating += this.PbpDataSource_ObjectCreating;

                pbpDataSource.TypeName =
                        Settings.Default.ObjectDS_Image_Service;

                pbpDataSource.EnablePaging = true;

                pbpDataSource.SelectMethod =
                    Settings.Default.ObjectDS_Search_keywords_SelectMethod;

                pbpDataSource.SelectParameters.Add("keywords", DbType.String, keywords);

                pbpDataSource.SelectParameters.Add("categoryId", DbType.Int64, category);

                pbpDataSource.StartRowIndexParameterName =
                    Settings.Default.ObjectDS_User_StartIndexParameter;

                pbpDataSource.SelectCountMethod =
                    Settings.Default.ObjectDS_Search_Keywords_CountMethod;

                pbpDataSource.MaximumRowsParameterName =
                    Settings.Default.ObjectDS_User_CountParameter;

                gvSearch.AllowPaging = true;
                gvSearch.PageSize = Settings.Default.PracticaMaD_defaultCount;

                gvSearch.DataSource = pbpDataSource;
                gvSearch.DataBind();
            }
            catch (TargetInvocationException)
            {

            }

        }



        protected void gvRenderPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSearch.PageIndex = e.NewPageIndex;
            gvSearch.DataBind();
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



