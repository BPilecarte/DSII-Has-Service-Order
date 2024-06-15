using OsDsII.Api.Dtos.Comments;
using OsDsII.Api.Models;
using OsDsII.Api.Dtos.ServiceOrders;

namespace OsDsII.Api.Services.Comments
{
    public interface ICommentsService
    {
        public Task<Comment> AddCommentAsync(int serviceOrderId, CommentDto comment);
    }
}
