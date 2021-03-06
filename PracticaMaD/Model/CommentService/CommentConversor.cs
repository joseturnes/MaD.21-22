using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    public class CommentConversor
    {
        public static List<CommentDto> ToCommentDtos(List<Comment> comments)
        {
            List<CommentDto> result = new List<CommentDto>();

            for (int i = 0; i < comments.Count; i++)
            {
                result.Add(new CommentDto(comments[i].commentId, comments[i].content, comments[i].usrId, "userName", comments[i].imgId, comments[i].comDate));
            }

            return result;
        }
    }
}
