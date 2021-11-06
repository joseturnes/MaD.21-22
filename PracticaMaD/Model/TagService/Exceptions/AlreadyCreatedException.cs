using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagService.Exceptions
{
    /// <summary>
    /// Public <c>ModelException</c> which captures the error 
    /// with the passwords of the users.
    /// </summary>
    [Serializable]
    public class AlreadyCreatedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="AlreadyCreatedException"/> class.
        /// </summary>
        /// <param name="loginName"><c>loginName</c> that causes the error.</param>
        public AlreadyCreatedException(String name)
            : base("Already created tag with name  => name = " + name)
        {
            this.name = name;
        }

        /// <summary>
        /// Stores the User login name of the exception
        /// </summary>
        /// <value>The name of the login.</value>
        public String name { get; private set; }
    }
}
