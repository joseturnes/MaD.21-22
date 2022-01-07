using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{



    public partial class UploadImage : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            lclMenuExplanation.Text = lclMenuExplanation.Text;
            lblDescription.Visible = false;
            lblF.Visible = false;
            lblISO.Visible = false;
            lblT.Visible = false;
            lblTags.Visible = false;
            lblTittle.Visible = false;
            lblWB.Visible = false;
        }


        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //Obtener datos de la imagen
            int Tamanio = fuploadImage.PostedFile.ContentLength;
            byte[] OriginalImage = new byte[Tamanio];
            fuploadImage.PostedFile.InputStream.Read(OriginalImage, 0, Tamanio);
            Bitmap OriginalImageBinary = new Bitmap(fuploadImage.PostedFile.InputStream);


            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IImageUploadService imageUploadService = iocManager.Resolve<IImageUploadService>();
            //Insertar en la base de datos

            Int64 userId = SessionManager.GetUserId(Context);

            string auxF;
            string auxT;
            string auxISO;
            string auxWB;

            if (txtF.Text.Equals(""))
            {
                auxF = "0";
            }
            else
            {
                auxF = txtF.Text;
            }

            if (txtT.Text.Equals(""))
            {
                auxT = "0";
            }
            else
            {
                auxT = txtT.Text;
            }

            if (txtISO.Text.Equals(""))
            {
                auxISO = "0";
            }
            else
            {
                auxISO = txtISO.Text;
            }

            if (txtWB.Text.Equals(""))
            {
                auxWB = "0";
            }
            else
            {
                auxWB = txtWB.Text;
            }

            ImageUploadDetails details = new ImageUploadDetails(txtTitle.Text, OriginalImage, userId, txtDescription.Text, DateTime.Now, Convert.ToInt64(auxF), Convert.ToInt64(auxT), auxISO, auxWB, 0);

            String[] tags = null;

            if (!txtTags.Text.Equals(""))
            {
                tags = txtTags.Text.Split(',');
            }


            imageUploadService.UploadImage(details, tags.ToList(), DropDownList1.SelectedValue);

            string ImagenDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(OriginalImage);
            imagePreview.ImageUrl = ImagenDataURL64;

            btnUpload.Visible = false;
            fuploadImage.Visible = false;

            DropDownList1.Visible = false;

            txtTitle.Visible = false;
            txtDescription.Visible = false;
            txtTags.Visible = false;
            txtF.Visible = false;
            txtT.Visible = false;
            txtISO.Visible = false;
            txtWB.Visible = false;

            lblFile.Visible = false;
            lblDescription.Visible = false;
            lblF.Visible = false;
            lblISO.Visible = false;
            lblT.Visible = false;
            lblTags.Visible = false;
            lblTittle.Visible = false;
            lblWB.Visible = false;
        }



        protected void btnPerfil_Click(object sender, EventArgs e)
        {
            String url = String.Format("./Perfil.aspx?userId={0}", SessionManager.GetUserId(Context));
            Response.Redirect(Response.ApplyAppPathModifier(url));
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}