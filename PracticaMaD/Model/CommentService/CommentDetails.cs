using System;


namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{

    /// <summary>
    /// VO Class which contains the user details
    /// </summary>
    [Serializable()]
    public class CommentDetails
    {
        #region Properties Region

        public String content { get; private set; }

        public long userId { get; private set; }

        public long pubId { get; private set; }

        public DateTime comDate { get; private set; }


        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CommetDetails"/>
        /// class.
        /// </summary>
        /// <param name="content">The user's comment.</param>
        /// <param name="userId">The comment user's id</param>
        /// <param name="pubId">The publications that the comment references .</param>
        /// <param name="comDate">The date of the comment</param>

        public CommentDetails(String content, long userId,
            long pubId, DateTime comDate)
        {
            this.content = content;
            this.userId = userId;
            this.pubId = pubId;
            this.comDate = comDate;
        }

        public override bool Equals(object obj)
        {

            CommentDetails target = (CommentDetails)obj;

            return (this.content == target.content)
                  && (this.userId == userId)
                  && (this.pubId == target.pubId)
                  && (this.comDate == target.comDate);
        }

        // The GetHashCode method is used in hashing algorithms and data 
        // structures such as a hash table. In order to ensure that it works 
        // properly, we suppose that the FirstName does not change.        
        public override int GetHashCode()
        {
            return this.content.GetHashCode();
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
            String strCommentDetails;

            strCommentDetails =
                "[ content = " + content + " | " +
                "userId = " + userId.ToString() + " | " +
                "pubId = " + pubId.ToString() + " | " +
                "comDate = " + comDate.ToString() + " ]";


            return strCommentDetails;
        }
    }
}
