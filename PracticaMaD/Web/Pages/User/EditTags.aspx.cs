using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Web.Security;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService;
using Es.Udc.DotNet.ModelUtil.IoC;
using System.Web;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Model.TagService;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class EditTags : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lclMenuExplanation.Text = "Edit Tags";
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IImageUploadService imageService = iocManager.Resolve<IImageUploadService>();

            Int64 imgId = Convert.ToInt64(Request.Params.Get("imgId"));

            var tags = imageService.FindImageTags(imgId, 0, 10);
            string result = "";

            foreach (var tag in tags)
            {
                result = result + "," + tag.tagname;
            }

            txtTags.Text = result;
        }

        protected void BtnCommentClick(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ITagService imageUploadService = iocManager.Resolve<ITagService>();
            //Insertar en la base de datos

            Int64 userId = SessionManager.GetUserId(Context);
            Int64 imgId = Convert.ToInt64(Request.Params.Get("imgId"));

            
            

            String url = String.Format("./PerfilCargado.aspx?ID={0}",userId);
            Response.Redirect(Response.ApplyAppPathModifier(url));

        }

    }
}