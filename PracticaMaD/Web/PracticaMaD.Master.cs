using System;
using System.Data;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService;
using Es.Udc.DotNet.PracticaMaD.Model.TagService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;

namespace Es.Udc.DotNet.PracticaMaD.Web
{

    public partial class PracticaMaD : System.Web.UI.MasterPage
    {

        public static readonly String USER_SESSION_ATTRIBUTE = "userSession";
        private ObjectDataSource pbpDataSource = new ObjectDataSource();


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
            

            try
            {
                // ObjectCreating is executed before ObjectDataSource creates
                // an instance of the type used as DataSource (UserService).
                // We need to intercept this call to replace the standard creation
                // procedure (a new UserService() sentence) to use the Unity
                // Container that allows to complete the dependences (accountDao,...)
                pbpDataSource.ObjectCreating += this.PbpDataSource_ObjectCreating;

                pbpDataSource.TypeName =
                     Settings.Default.ObjectDS_Tag_Service;

                pbpDataSource.EnablePaging = true;

                pbpDataSource.SelectMethod =
                    Settings.Default.ObjectDS_Tag_SelectMethod;

                pbpDataSource.StartRowIndexParameterName =
                    Settings.Default.ObjectDS_User_StartIndexParameter;

                pbpDataSource.SelectCountMethod =
                    Settings.Default.ObjectDS_Tag_CountMethod;
                
                pbpDataSource.MaximumRowsParameterName =
                    Settings.Default.ObjectDS_User_CountParameter;

                gvTags.AllowPaging = false;
                gvTags.PageSize = Settings.Default.PracticaMaD_defaultCount;

                gvTags.DataSource = pbpDataSource;
                gvTags.DataBind();

                var rows = gvTags.Rows;

                for (int i = 0; i < rows.Count; i++)
                {
                    rows[i].Font.Size = 20 - 2 * i;
                }
            }
            catch (TargetInvocationException)
            {
                
            }
           
            
        }

        protected void gvTagsPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTags.PageIndex = e.NewPageIndex;
            gvTags.DataBind();
        }

        protected void PbpDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ITagService tagService = iocManager.Resolve<ITagService>();

            e.ObjectInstance = tagService;
        }

        protected void searchKeywords_Click(object sender, EventArgs e)
        {
            int category = 0;
            if (DropDownList1.SelectedValue.Equals("Retrato"))
                category = 1;
            if (DropDownList1.SelectedValue.Equals("Paisaje Nocturno"))
                category = 2;
            if (DropDownList1.SelectedValue.Equals("Paisaje"))
                category = 3;
            if (DropDownList1.SelectedValue.Equals("Ciudades"))
                category = 4;

            if (DropDownList1.SelectedValue.Equals("Select a Category"))
            {
                string keywords = txtKeywords.Text;
                string url = "~/Pages/User/RenderSearch.aspx?keywords=" + keywords + "?category=" + "0";
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }
            else
            {
                string keywords = txtKeywords.Text;
                string url = "~/Pages/User/RenderSearch.aspx?keywords=" + keywords + "&category=" + category.ToString();
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }
        }
    }


}

