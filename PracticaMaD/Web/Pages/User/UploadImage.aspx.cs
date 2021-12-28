using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService;
using Es.Udc.DotNet.ModelUtil.IoC;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System.Data;
using System.ComponentModel;
using System.Web.UI.HtmlControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    


    public partial class UploadImage : System.Web.UI.Page
    {
     

        private object imgPreview;

        protected void Page_Load(object sender, EventArgs e)
        {
            ConsultarImagenes();
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

            ImageUploadDetails details = new ImageUploadDetails(txtTitle.Text, OriginalImage, userId, txtDescription.Text,DateTime.Now,Convert.ToInt64(txtF.Text), Convert.ToInt64(txtT.Text),txtISO.Text,txtWB.Text,0) ;

            List<String> tags = new List<string>();

            imageUploadService.UploadImage(details, tags, DropDownList1.SelectedValue);

            string ImagenDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(OriginalImage);
            imagePreview.ImageUrl = ImagenDataURL64;

            ConsultarImagenes();
        }



        protected void ConsultarImagenes()
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IImageUploadService imageUploadService = iocManager.Resolve<IImageUploadService>();
            //Insertar en la base de datos

            Int64 userId = SessionManager.GetUserId(Context);
            List<ImageUploadDto> data = imageUploadService.recentUploads(userId, 0, 3);

            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(ImageUploadDto));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (ImageUploadDto item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);

            }

                DataTable ImagenesBD = table;

            Repeater1.DataSource = ImagenesBD;
            Repeater1.DataBind();

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}