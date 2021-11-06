using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Model.TagService.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagService
{
    public interface ITagService
    {
        [Inject]
        ITagDao TagDao { set; }

        /// <exception cref="AlreadyCreatedException"/>
        long CreateTag(string name);


    }
}
