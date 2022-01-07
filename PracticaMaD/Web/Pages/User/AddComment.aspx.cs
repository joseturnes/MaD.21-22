using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Web;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class AddComment : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lclMenuExplanation.Text = lclMenuExplanation.Text;
        }

        protected void BtnCommentClick(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICommentService commentService = iocManager.Resolve<ICommentService>();
            //Insertar en la base de datos

            Int64 userId = SessionManager.GetUserId(Context);
            Int64 imgId = Convert.ToInt64(Request.Params.Get("imgId"));

            commentService.AddComment(imgId, txtContent.Text, userId);

            String url = String.Format("./ImageDetails.aspx?imgId={0}", imgId);
            Response.Redirect(Response.ApplyAppPathModifier(url));

        }
    }
}