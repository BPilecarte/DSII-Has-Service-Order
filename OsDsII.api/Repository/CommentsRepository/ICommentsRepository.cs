using OsDsII.Api.Models;

namespace OsDsII.Api.Repository.CommentsRepository
{
    public interface ICommentsRepository
    {
        public Task AddCommentAsync(Comment comment);
    }
}
