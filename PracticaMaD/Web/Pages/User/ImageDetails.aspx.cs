using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService;
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
            if (!IsPostBack)
            {
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IImageUploadService imageUploadService = iocManager.Resolve<IImageUploadService>();

                long imgId = Convert.ToInt64(Request.Params.Get("imgId"));

                ImageUpload image = imageUploadService.findImage(imgId);
                String commentsUrl = String.Format("./CommentDetails.aspx?imgId={0}", imgId);

                Image1.ImageUrl = "data:image/jpg;base64," + Convert.ToBase64String(image.uploadedImage);
                lablTitle.Text = "<h2>"+image.title+"<h2/>";
                lablLikes.Text = "<h5> Likes: " + image.likes + "<h5/>";
                labldescription.Text = "<h2> Description: " + image.descriptions + "<h2/>";
                long numberOfComments = imageUploadService.CountComments(imgId);
                if (numberOfComments==0)
                {
                    CommentsLink.Visible = false;
                }
                else {
                    CommentsLink.Visible = true;
                }
                CommentsLink.Text = "Coments : " + numberOfComments.ToString();
                CommentsLink.NavigateUrl = commentsUrl;
                




            }
        }
    }
}