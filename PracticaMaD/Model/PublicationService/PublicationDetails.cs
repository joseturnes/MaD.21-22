using System;


namespace Es.Udc.DotNet.PracticaMaD.Model.PublicationService
{

    /// <summary>
    /// VO Class which contains the user details
    /// </summary>
    [Serializable()]
    public class PublicationDetails
    {
        #region Properties Region

        public float imgId { get; private set; }

        public long userId { get; private set; }

        public long likes { get; private set; }

        public DateTime pubDate { get; private set; }


        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicationDetails"/>
        /// class.
        /// </summary>
        /// <param name="imgId">The image id.</param>
        /// <param name="userId">The publication owner user id</param>
        /// <param name="likes">The number of likes .</param>
        /// <param name="pubDate">The date of the publication</param>

        public PublicationDetails(float imgId, long userId,
            long likes, DateTime pubDate)
        {
            this.imgId = imgId;
            this.userId = userId;
            this.likes = likes;
            this.pubDate = pubDate;
        }

        public override bool Equals(object obj)
        {

            PublicationDetails target = (PublicationDetails)obj;

            return (this.imgId == target.imgId)
                  && (this.userId == userId)
                  && (this.likes == target.likes)
                  && (this.pubDate == target.pubDate);
        }

        // The GetHashCode method is used in hashing algorithms and data 
        // structures such as a hash table. In order to ensure that it works 
        // properly, we suppose that the imgId does not change.        
        public override int GetHashCode()
        {
            return this.imgId.GetHashCode();
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
            String strPublicationDetails;

            strPublicationDetails =
                "[ imgId = " + imgId.ToString() + " | " +
                "userId = " + userId.ToString() + " | " +
                "likes = " + likes.ToString() + " | " +
                "pubDate = " + pubDate.ToString() + " ]";


            return strPublicationDetails;
        }
    }
}