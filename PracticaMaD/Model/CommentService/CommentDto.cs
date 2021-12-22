using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    [Serializable()]
    public class CommentDto
    {
        #region Properties Region

        public String content { get; private set; }

        public long userId { get; private set; }

        public long imgId { get; private set; }

        public DateTime comDate { get; private set; }


        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentDetails"/>
        /// class.
        /// </summary>
        /// <param name="content">The user's comment.</param>
        /// <param name="userId">The comment user's id</param>
        /// <param name="imgId">The publications that the comment references .</param>
        /// <param name="comDate">The date of the comment</param>

        public CommentDto(String content, long userId,
            long imgId, DateTime comDate)
        {
            this.content = content;
            this.userId = userId;
            this.imgId = imgId;
            this.comDate = comDate;
        }

        public override bool Equals(object obj)
        {

            CommentDto target = (CommentDto)obj;

            return (this.content == target.content)
                  && (this.userId == userId)
                  && (this.imgId == target.imgId)
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
            String strCommentDto;

            strCommentDto =
                "[ content = " + content + " | " +
                "userId = " + userId.ToString() + " | " +
                "pubId = " + imgId.ToString() + " | " +
                "comDate = " + comDate.ToString() + " ]";


            return strCommentDto;
        }
    }
}
