using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService
{
    [Serializable()]
    public class ImageUploadDto
    {
        public byte[] uploadedImage { get; set; }

        public string title { get; set; }

        public string descriptions { get; set; }

        public System.DateTime uploadDate { get; set; }
       
        public long likes { get; set; }

        


        public ImageUploadDto(byte [] uploadedImage, string title, string descriptions, System.DateTime uploadDate,
             long likes)
        {
            this.uploadedImage = uploadedImage;
            this.title = title;
            this.descriptions = descriptions;
            this.uploadDate = uploadDate;
            this.likes = likes;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageUploadDetails"/>
        /// class.
        /// </summary>
        /// <param name="title">The image title.</param>
        /// <param name="descriptions">The image descriptions</param>
        /// <param name="uploadDate">Image upload date.</param>
        /// <param name="f">Focal of the image</param>
        /// <param name="t">Exposition time</param>
        /// <param name="wb">WhiteBalance of the image</param>
        /// <param name="category">The category of the image</param>
        /// 
        public override bool Equals(object obj)
        {
            ImageUploadDto target = (ImageUploadDto)obj;

            return (this.title == target.title)
                && (this.descriptions == target.descriptions)
                && (this.uploadedImage == target.uploadedImage)
                && (this.uploadDate == target.uploadDate)
                & (this.likes == target.likes);
        }

        // The GetHashCode method is used in hashing algorithms and data 
        // structures such as a hash table. In order to ensure that it works 
        // properly, we suppose that the title does not change.        
        public override int GetHashCode()
        {
            return this.title.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the 
        /// current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current 
        /// <see cref="T:System.Object"></see>.
        /// </returns>
        public override String ToString()
        {
            String strImageUploadDto;

            strImageUploadDto =
                "[ title = " + title + " | " +
                "uploadedImage = " + uploadedImage + " | " +
                "descriptions = " + descriptions + " | " +
                "uploadDate = " + uploadDate + " | " +
                "likes = " + likes + " ]";


            return strImageUploadDto;
        }


    }
}
