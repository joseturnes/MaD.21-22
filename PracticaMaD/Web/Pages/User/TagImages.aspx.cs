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
using Es.Udc.DotNet.PracticaMaD.Model.TagService;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{

    public partial class TagService : SpecificCulturePage
    {
        private ObjectDataSource pbpDataSource = new ObjectDataSource();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                pbpDataSource.ObjectCreating += this.PbpDataSource_ObjectCreating;

                pbpDataSource.TypeName =
                        Settings.Default.ObjectDS_Tag_Service;

                pbpDataSource.EnablePaging = true;

                pbpDataSource.SelectMethod =
                    Settings.Default.ObjectDS_Tag_Image_SelectMethod;

                Int64 tagId = Convert.ToInt64(Request.Params.Get("ID"));

                pbpDataSource.SelectParameters.Add("tagId", DbType.Int64, tagId.ToString());

                pbpDataSource.SelectCountMethod =
                    Settings.Default.ObjectDS_Tag_Images_CountMethod;
                pbpDataSource.StartRowIndexParameterName =
                    Settings.Default.ObjectDS_User_StartIndexParameter;
                pbpDataSource.MaximumRowsParameterName =
                    Settings.Default.ObjectDS_User_CountParameter;

                gvTagImages.AllowPaging = true;
                gvTagImages.PageSize = Settings.Default.PracticaMaD_defaultCount;

                gvTagImages.DataSource=pbpDataSource;
                gvTagImages.DataBind();
            }
            catch (TargetInvocationException)
            {
                lblInvalidUser.Visible = true;
            }
        }



        protected void gvFollowsPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTagImages.PageIndex = e.NewPageIndex;
            gvTagImages.DataBind();
        }

        protected void PbpDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ITagService tagService = iocManager.Resolve<ITagService>();

            e.ObjectInstance = tagService;
        }

    }
}



