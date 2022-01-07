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
using System.Collections.Generic;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class EditTags : SpecificCulturePage
    {
        IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lblEditTags.Visible = true;

                lclMenuExplanation.Text = lclMenuExplanation.Text;

                IImageUploadService imageService = iocManager.Resolve<IImageUploadService>();

                Int64 imgId = Convert.ToInt64(Request.Params.Get("imgId"));

                var tags = imageService.FindImageTags(imgId, 0, 100);
                string result = "";

                foreach (var tag in tags)
                {
                    result = result + "," + tag.tagname;
                }

                var array = result.Split(',');

                var array2 = array.Skip(1);

                result = string.Join(",", array2);

                txtTags.Text = result;
            }
        }

        protected void BtnEditClick(object sender, EventArgs e)
        {
            //IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ITagService tagService = iocManager.Resolve<ITagService>();
            //Insertar en la base de datos

            Int64 imgId = Convert.ToInt64(Request.Params.Get("imgId"));

          
            String [] tags = null;

          
            tags = txtTags.Text.Split(',');
           
            tagService.UpdateTags(imgId, tags.ToList());           

            String url = String.Format("./ImageDetails.aspx?imgId={0}",imgId);
            Response.Redirect(Response.ApplyAppPathModifier(url));
        }
    }
}