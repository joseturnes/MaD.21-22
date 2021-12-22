using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    public class CommentConversor
    {
        public static List<CommentDto> toCommentDtos (List<Comment> comments)
        {
            List<CommentDto> result = new List<CommentDto>();

            for (int i = 0; i < comments.Count; i++)
            {
                result.Add(new CommentDto(comments[i].content, comments[i].usrId, comments[i].imgId, comments[i].comDate));
            }

            return result;
        }
    }
}
