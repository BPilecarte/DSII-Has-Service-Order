using AutoMapper;
using OsDsII.Api.Dtos.Comments;
using OsDsII.Api.Models;
using OsDsII.Api.Repository.CommentsRepository;
using OsDsII.Api.Repository.ServiceOrderRepository;
using OsDsII.Api.Exceptions;

namespace OsDsII.Api.Services.Comments
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IServiceOrderRepository _serviceOrderRepository;
        private readonly IMapper _mapper;

        public CommentsService(ICommentsRepository commentsRepository, IMapper mapper, IServiceOrderRepository serviceOrderRepository)
        {
            _commentsRepository = commentsRepository;
            _serviceOrderRepository = serviceOrderRepository;
            _mapper = mapper;
        }

        public async Task<Comment> AddCommentAsync(int serviceOrderId, CommentDto comment)
        {

            var commentMapped = _mapper.Map<Comment>(comment);
            var os = await _serviceOrderRepository.GetByIdAsync(serviceOrderId);

            if (os == null)
            {
                throw new NotFoundException("Ordem de serviço não encontrada.");
            }

            Comment commentExists = HandleCommentObject(os.Id, commentMapped.Description);

            return commentExists;
        }

        private Comment HandleCommentObject(int id, string description)
        {
            return new Comment
            {
                Description = description,
                ServiceOrderId = id
            };
        }
    }
}
