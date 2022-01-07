using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageUploadDao
{
    /// <summary>
    /// VO Class which contains the image details
    /// </summary>
    [Serializable()]
    public class ImageUploadDetails
    {

        public string title { get; set; }

        public byte[] uploadedImage { get; set; }

        public long usrId { get; set; }

        public string descriptions { get; set; }

        public System.DateTime uploadDate { get; set; }

        public float f { get; set; }

        public float t { get; set; }

        public string iso { get; set; }

        public long likes { get; set; }

        public string wb { get; set; }


        public ImageUploadDetails(string title, byte[] image, long usrId, string descriptions, System.DateTime uploadDate,
            float f, float t, string iso, string wb, long likes)
        {
            this.title = title;
            this.uploadedImage = image;
            this.usrId = usrId;
            this.descriptions = descriptions;
            this.uploadDate = uploadDate;
            this.f = f;
            this.t = t;
            this.iso = iso;
            this.wb = wb;
            this.likes = likes;
        }

        public ImageUploadDetails(string title, byte[] image, long usrId, string descriptions)
        {
            this.title = title;
            this.uploadedImage = image;
            this.usrId = usrId;
            this.descriptions = descriptions;
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
            ImageUploadDetails target = (ImageUploadDetails)obj;

            return (this.title == target.title)
                && (this.uploadedImage == target.uploadedImage)
                && (this.descriptions == target.descriptions)
                && (this.uploadDate == target.uploadDate)
                && (this.f == target.f)
                && (this.t == target.t)
                && (this.iso == target.iso)
                && (this.wb == target.wb)
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
            String strImageUploadDetails;

            strImageUploadDetails =
                "[ title = " + title + " | " +
                "descriptions = " + descriptions + " | " +
                "uploadDate = " + uploadDate + " | " +
                "f = " + f + " | " +
                "t = " + t + " | " +
                "iso = " + iso + " | " +
                "likes = " + likes + " | " +
                "wb = " + wb + " ]";


            return strImageUploadDetails;
        }


    }
}
