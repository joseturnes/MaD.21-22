using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class ImageDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lclMenuExplanation.Text = "Image Details";

            if (SessionManager.IsUserAuthenticated(Context))
            {
                long imgId = Convert.ToInt64(Request.Params.Get("imgId"));
                Int64 userId = SessionManager.GetUserId(Context);

                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IImageUploadService imageUploadService = iocManager.Resolve<IImageUploadService>();
                ImageUpload image = imageUploadService.findImage(imgId);

                if (imageUploadService.isLiked(imgId, image.usrId))
                {
                    likeButton.Text = "💔";
                }

                if (!(image.usrId == userId))
                {
                    btnDelete.Visible = false;
                }

                if (!IsPostBack)
                {

                    String commentsUrl = String.Format("./CommentDetails.aspx?imgId={0}", imgId);

                    Image1.ImageUrl = "data:image/jpg;base64," + Convert.ToBase64String(image.uploadedImage);
                    lablTitle.Text = "<h2>" + image.title + "<h2/>";
                    lablLikes.Text = "<h5> Likes: " + image.likes + "<h5/>";
                    labldescription.Text = "<h2> Description: " + image.descriptions + "<h2/>";
                    long numberOfComments = imageUploadService.CountComments(imgId);
                    if (numberOfComments == 0)
                    {
                        CommentsLink.Visible = false;
                    }
                    else
                    {
                        CommentsLink.Visible = true;
                    }
                    CommentsLink.Text = "Coments : " + numberOfComments.ToString();
                    CommentsLink.NavigateUrl = commentsUrl;

                }
            }
            else
            {
                String url = String.Format("./Authentication.aspx");
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }
        }

        protected void BtnDeleteClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                /* Get data. */
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IImageUploadService imageService = iocManager.Resolve<IImageUploadService>();
                Int64 userId = SessionManager.GetUserId(Context);
                Int64 imgId = Convert.ToInt64(Request.Params.Get("imgId"));
                imageService.RemoveImage(imgId);

                String url = String.Format("./Perfil.aspx?userId={0}", userId);
                Response.Redirect(Response.ApplyAppPathModifier(url));

            }
        }

        protected void BtnAddComment(object sender, EventArgs e)
        {
            Int64 imgId = Convert.ToInt64(Request.Params.Get("imgId"));
            String url = String.Format("./AddComment.aspx?imgId={0}", imgId);
            Response.Redirect(Response.ApplyAppPathModifier(url));
        }

        protected void BtnLikeClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                /* Get data. */
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IImageUploadService imageService = iocManager.Resolve<IImageUploadService>();
                Int64 userId = SessionManager.GetUserId(Context);
                Int64 imgId = Convert.ToInt64(Request.Params.Get("imgId"));
                if (!imageService.isLiked(imgId, userId))
                {
                    imageService.LikedImage(imgId, userId);
                    Response.Redirect(Request.RawUrl);
                }
                else
                {
                    imageService.UnlikeImage(imgId, userId);
                    Response.Redirect(Request.RawUrl);
                }
                

            }
        }

    }
}