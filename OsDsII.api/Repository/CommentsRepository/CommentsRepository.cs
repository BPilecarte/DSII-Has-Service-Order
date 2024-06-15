using OsDsII.Api.Data;
using OsDsII.Api.Models;

namespace OsDsII.Api.Repository.CommentsRepository
{
    public sealed class CommentsRepository : ICommentsRepository
    {
        private readonly DataContext _dataContext;

        public CommentsRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddCommentAsync(Comment comment)
        {
            await _dataContext.Comments.AddAsync(comment);
            await _dataContext.SaveChangesAsync();
        }
    }
}
