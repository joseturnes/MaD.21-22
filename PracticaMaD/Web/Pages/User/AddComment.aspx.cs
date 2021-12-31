using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Web.Security;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService;
using Es.Udc.DotNet.ModelUtil.IoC;
using System.Web;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class AddComment : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lclMenuExplanation.Text = "Add Comment";
        }

        protected void BtnCommentClick(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICommentService commentService = iocManager.Resolve<ICommentService>();
            //Insertar en la base de datos

            Int64 userId = SessionManager.GetUserId(Context);
            Int64 imgId = Convert.ToInt64(Request.Params.Get("imgId"));

            commentService.AddComment(imgId,txtContent.Text,userId);

            String url = String.Format("./ImageDetails.aspx?imgId={0}", imgId);
            Response.Redirect(Response.ApplyAppPathModifier(url));

        }
    }
}