using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Web;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class EditComment : SpecificCulturePage
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
            Int64 comId = Convert.ToInt64(Request.Params.Get("comId"));

            if (!txtContent.Text.Equals(""))
            {
                commentService.UpdateComment(comId, txtContent.Text);
            }

            String url = String.Format("./PerfilCargado.aspx?ID={0}", userId);
            Response.Redirect(Response.ApplyAppPathModifier(url));

        }

        protected void BtnDeleteClick(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICommentService commentService = iocManager.Resolve<ICommentService>();
            //Insertar en la base de datos

            Int64 userId = SessionManager.GetUserId(Context);
            Int64 comId = Convert.ToInt64(Request.Params.Get("comId"));

            commentService.RemoveComment(comId);

            String url = String.Format("./PerfilCargado.aspx?ID={0}", userId);
            Response.Redirect(Response.ApplyAppPathModifier(url));

        }
    }
}