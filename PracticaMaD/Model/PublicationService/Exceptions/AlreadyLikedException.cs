using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.PublicationService.Exceptions
{
    [Serializable]
    public class AlreadyLikedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="IncorrectPasswordException"/> class.
        /// </summary>
        /// <param name="loginName"><c>loginName</c> that causes the error.</param>
        public AlreadyLikedException(long pubId)
            : base("AlreadyLikedException => pubId = " + pubId)
        {
            this.pubId = pubId;
        }

        /// <summary>
        /// Stores the User login name of the exception
        /// </summary>
        /// <value>The name of the login.</value>
        public long pubId { get; private set; }

    }
}
